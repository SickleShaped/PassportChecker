using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using PassportChecker.Models;
using PassportChecker.Services.Interfaces;

namespace PassportChecker.Services.Implementations;

public class PassportPostgreSQLService:IPassportService
{
    private readonly ApiDbContext _dbContext;
    private readonly IMapper _mapper;

    public PassportPostgreSQLService(ApiDbContext dbContext, IMapper mapper)
    {
        //_dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<Passport>> GetInactivePassports()
    {
        var passports = await _dbContext.Passports.AsNoTracking().ProjectTo<Passport>(_mapper.ConfigurationProvider).ToListAsync();
        return passports;
    }

    public async Task<List<Change>> GetChangesByDate(DateTime date)
    {
        //читать из файла который мой
        var changes = await _dbContext.Changes.Where(c=>c.Date.Day == date.Day && c.Date.Month == date.Month && c.Date.Year == date.Year).AsNoTracking().ProjectTo<Change>(_mapper.ConfigurationProvider).ToListAsync();
        return changes;
        
    }

    public async Task<List<Change>> GetChangeByPassport(int series, int number)
    {
        var changes = await _dbContext.Changes.Where(c=>c.Series == series && c.Number == number).AsNoTracking().ProjectTo<Change>(_mapper.ConfigurationProvider).ToListAsync();
        return changes;
        //читать из файла который мой
        
    }
}
