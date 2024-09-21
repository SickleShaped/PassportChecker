using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PassportChecker.Models;
using PassportChecker.Services.Implementations;
using PassportChecker.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.Diagnostics;

namespace PassportChecker.Controllers;

[ApiController]
[Route("/")]
public class HomeController : Controller
{
    private readonly IPassportService _passportService;
    private readonly IDataUpdaterService _readerService;
    public HomeController(IPassportService passportService, IDataUpdaterService readerService)
    {
        _readerService = readerService;
        _passportService = passportService;
    }

    [SwaggerIgnore]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("GetInactivePassports")]
    public async Task<Passport> GetInactivePassports(int series, int number)
    {
        return await _passportService.GetInactivePassports(series, number);
    }

    [HttpGet("GetChangesByDate")]
    public async Task<List<Change>> GetChangesByDate(int date, int page = 0)
    {
        return await _passportService.GetChangesByDate(date, page);
    }

    [HttpGet("GetChangeByPassport")]
    public async Task<List<Change>> GetChangeByPassport(int series, int number, int page = 0)
    {
        return await _passportService.GetChangeByPassport(series, number, page);
    }

    [HttpPost("AutoReader")]
    public async Task GetDataFromSource()
    {
        await _readerService.GetDataFromSource();
    }
}
