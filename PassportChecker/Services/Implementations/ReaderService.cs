using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using PassportChecker.Models;
using PassportChecker.Models.DbModels;
using PassportChecker.Services.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;

namespace PassportChecker.Services.Implementations;

public class ReaderService : IReaderService
{
    private readonly ApiDbContext _dbContext;
    private readonly IMapper _mapper;

    public ReaderService(ApiDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;

    }

    public async Task GetDataFromSource()
    {
        var sourcePassports = GetPassports();
        var dbPassports = await _dbContext.Passports.ToListAsync();

        var needToDelete = dbPassports.Except(sourcePassports); //только те, которые есть в базе, но уже нет в сурс-файле
        var needToAdd = sourcePassports.Except(dbPassports); //только те, которое есть в сурс файле, но пока нет в базе

        foreach (var Delete in needToDelete)
        {
            _dbContext.Passports.Remove(Delete);
            await _dbContext.PassportChanges.AddAsync(new ChangeModel()
            {
                Series = Delete.Series,
                Number = Delete.Number,
                Id = new Guid(),
                IsActive = true,
                Date = (DateTime.Now.Year - 2000) * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day
            });
        }

        foreach (var Add in needToAdd)
        {
            await _dbContext.Passports.AddAsync(new PassportModel()
            {
                Series = Add.Series,
                Number = Add.Number
            });
            await _dbContext.PassportChanges.AddAsync(new ChangeModel()
            {
                Series = Add.Series,
                Number = Add.Number,
                Id = new Guid(),
                IsActive = false,
                Date = (DateTime.Now.Year - 2000) * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day
            });
        }

        await _dbContext.SaveChangesAsync();
    }

    private List<PassportModel> GetPassports()
    {
        List<PassportModel> passports = new List<PassportModel>();
        using (TextFieldParser tfp = new TextFieldParser(@"C:\\PassportChecker\Data.csv"))
        {

            tfp.TextFieldType = FieldType.Delimited;
            tfp.SetDelimiters(",");
            tfp.ReadFields();
            while (!tfp.EndOfData)
            {
                PassportModel passport = new PassportModel();
                string[] fields = tfp.ReadFields();

                bool seriesSuccess = Int32.TryParse(fields[0], out int series);
                bool numberSuccess = Int32.TryParse(fields[1], out int number);
                bool formatSuccess = seriesSuccess && numberSuccess && series>=1000 && fields[0].Length == 4 && fields[1].Length == 6;

                if (formatSuccess)
                {
                    passport.Series = series;
                    passport.Number = number;
                    passports.Add(passport);
                }
            }
        }
        return passports;
    }
}
