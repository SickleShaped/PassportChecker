using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using PassportChecker.Models;
using PassportChecker.Services.Interfaces;

namespace PassportChecker.Services.Implementations;

public class PassportPostgreSQLService : IPassportService
{
    private readonly ApiDbContext _dbContext;
    private readonly IMapper _mapper;

    public PassportPostgreSQLService(ApiDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Passport> GetInactivePassports(int series, int number)
    {
        var passports = await _dbContext.Passports.Where(c => c.Series == series && c.Number == number).AsNoTracking().ProjectTo<Passport>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        return passports;
    }

    public async Task<List<Change>> GetChangesByDate(DateTime date)
    {
        var changes = await _dbContext.Changes.Where(c => (date.Year - 2000) * 10000 + date.Month * 100 + date.Day == c.Date).AsNoTracking().ProjectTo<Change>(_mapper.ConfigurationProvider).ToListAsync();
        return changes;

    }

    public async Task<List<Change>> GetChangeByPassport(int series, int number)
    {
        var changes = await _dbContext.Changes.Where(c => c.Series == series && c.Number == number).AsNoTracking().ProjectTo<Change>(_mapper.ConfigurationProvider).ToListAsync();
        return changes;

    }
}
