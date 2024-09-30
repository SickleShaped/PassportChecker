using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using PassportChecker.Models;
using PassportChecker.Models.DbModels;
using PassportChecker.Services.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;

namespace PassportChecker.Services.Implementations;

public class DataUpdaterService : IDataUpdaterService
{
    private readonly ApiDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public DataUpdaterService(ApiDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task GetDataFromSource()
    {
        var path = @"C:\\PassportChecker\Data.csv";
        var sourcePassports = FileReader.GetPassports(path);
        var dbPassports = await _dbContext.Passports.AsNoTracking().ToListAsync();

        var needToDelete = DataConfigurator.GetFirstExceptSecond(dbPassports, sourcePassports);
        var needToAdd = DataConfigurator.GetFirstExceptSecond(sourcePassports, dbPassports);
        var changes = DataConfigurator.GetChanges(needToDelete, needToAdd);

        

        while (changes.Count > 0)
        {
            int number = 0;
            if (changes.Count >= 1000000) number = 1000000;
            else number = changes.Count;

            var x = changes.GetRange(0, number);
             _dbContext.Changes.AddRange(x);
            await _dbContext.BulkSaveChangesAsync();
            changes.RemoveRange(0, number);
        }

        int i = 0;

        
        while (needToDelete.Count > 0)
        {
            int number = 0;
            if (needToDelete.Count >= 100000) number = 100000;
            else number = needToDelete.Count;

            var x = needToDelete.GetRange(0, number);
            _dbContext.Passports.RemoveRange(x);
            await _dbContext.BulkSaveChangesAsync()
            needToDelete.RemoveRange(0, number);
        }
        
        while (needToAdd.Count > 0)
        {
            int number = 0;
            if (needToAdd.Count >= 100000) number = 100000;
            else number = needToAdd.Count;

            var x = needToAdd.GetRange(0, number);
            await _dbContext.Passports.AddRangeAsync(x);
            await _dbContext.BulkSaveChangesAsync()
            needToAdd.RemoveRange(0, number);
        }
        
    }

    
}
