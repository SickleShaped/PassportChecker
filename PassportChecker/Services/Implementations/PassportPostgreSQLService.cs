﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using PassportChecker.Models;
using PassportChecker.Services.Interfaces;
using System.Collections.Generic;

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

    public async Task<List<Change>> GetChangesByDate(int date, int page)
    {
        var changes = await _dbContext.Changes.Where(c => date == c.Date).AsNoTracking().ProjectTo<Change>(_mapper.ConfigurationProvider).ToListAsync();
        List<Change> changesPage = new List<Change>();
        if (changes.Count - page * 1000 >= 0) changes.RemoveRange(0, page * 1000);
        else return changesPage;

        if(changes.Count >=1000) changesPage = changes.GetRange(0, 1000);
        else changesPage = changes.GetRange(0, changes.Count);

        return changesPage;
    }

    public async Task<List<Change>> GetChangeByPassport(int series, int number, int page)
    {
        var changes = await _dbContext.Changes.Where(c => c.Series == series && c.Number == number).AsNoTracking().ProjectTo<Change>(_mapper.ConfigurationProvider).ToListAsync();
        List<Change> changesPage = new List<Change>();
        if (changes.Count - page * 1000 >= 0) changes.RemoveRange(0, page * 1000);
        else return changesPage;

        if (changes.Count >= 1000) changesPage = changes.GetRange(0, 1000);
        else changesPage = changes.GetRange(0, changes.Count);

        return changesPage;
    }
}
