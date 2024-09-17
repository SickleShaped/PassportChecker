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
        var dbPassports = await _dbContext.Passports.AsNoTracking().ToListAsync();

        var needToDelete = dbPassports.Except(sourcePassports).ToList(); //только те, которые есть в базе, но уже нет в сурс-файле
        var needToAdd = sourcePassports.Except(dbPassports).ToList(); //только те, которое есть в сурс файле, но пока нет в базе

        int date = (DateTime.Now.Year - 2000-1) * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;

        List<ChangeModel> changes = new List<ChangeModel>();

        foreach (var Delete in needToDelete)
        {
            changes.Add(new ChangeModel()
            {
                Series = Delete.Series,
                Number = Delete.Number,
                Id = new Guid(),
                IsActive = true,
                Date = date
            });
        }
        foreach (var Add in needToAdd)
        {
            changes.Add(new ChangeModel()
            {
                Series = Add.Series,
                Number = Add.Number,
                Id = new Guid(),
                IsActive = false,
                Date = date
            });
        }
        
        while (needToDelete.Count > 0)
        {
            int number = 0;
            if (needToDelete.Count >= 25000) number = 25000;
            else number = needToDelete.Count;

            var x = needToDelete.GetRange(0, number);
            _dbContext.RemoveRange(x);
            _dbContext.SaveChangesAsync();
            needToDelete.RemoveRange(0, number);
        }
        while (changes.Count > 0)
        {
            int number = 0;
            if (changes.Count >= 25000) number = 25000;
            else number = changes.Count;

            var x = changes.GetRange(0, number);
            await _dbContext.Changes.AddRangeAsync(x);
            await _dbContext.SaveChangesAsync();
            changes.RemoveRange(0, number);  
        }
        while (needToAdd.Count > 0)
        {
            int number = 0;
            if (needToAdd.Count >= 25000) number = 25000;
            else number = needToAdd.Count;

            var x = needToAdd.GetRange(0, number);
            await _dbContext.Passports.AddRangeAsync(x);
            await _dbContext.SaveChangesAsync();
            needToAdd.RemoveRange(0, number);
        }
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
                bool formatSuccess = seriesSuccess && numberSuccess && series >= 1000 && fields[0].Length == 4 && fields[1].Length == 6;

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
