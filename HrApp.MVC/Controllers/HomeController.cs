using HrApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HrApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static PersonelHomeViewModel _personelHomeViewModel = new()
        {
            Id = "1",
            Name = "Name1",
            SecondName = "SecondName1",
            Surname = "Surname1",
            SecondSurname = "SecondSurname1",
            Email = "Email1",
            MobileNumber = "MobileNumber1",
            Occupation = "Ocuppation1",
            Department = "Department1"
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View(_personelHomeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}