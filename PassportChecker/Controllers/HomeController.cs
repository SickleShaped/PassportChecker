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

    public ReaderService readerService; ///..............................................................

    public HomeController(IPassportService passportService)
    {
        readerService = new ReaderService();
        _passportService = passportService;
    }

    [SwaggerIgnore]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("GetInactivePassports")]
    public async Task<List<Passport>> GetInactivePassports()
    {
        return await _passportService.GetInactivePassports();
    }

    [HttpGet("GetChangesByDate")]
    public async Task<List<Change>> GetChangesByDate(DateTime date)
    {
        return await _passportService.GetChangesByDate(date);
    }

    [HttpGet("GetChangeByPassport")]
    public async Task<List<Change>> GetChangeByPassport(int series, int number)
    {
        return await _passportService.GetChangeByPassport(series, number);
    }

    [HttpPost("FAKEREADER")]
    public async void GetDataFromSource()
    {
         readerService.GetDataFromSource();
    }
}
