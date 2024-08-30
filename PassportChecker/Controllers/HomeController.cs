using Microsoft.AspNetCore.Mvc;
using PassportChecker.Models;
using PassportChecker.Services.Implementations;
using System.Diagnostics;

namespace PassportChecker.Controllers
{
    public class HomeController : Controller
    {
        private readonly PassportService _passportService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public void GetInactivePassports()
        {

        }

        public void GetAllChangesByDate()
        {

        }

        public void GetActivityBySeriesNumber()
        {

        }
    }
}
