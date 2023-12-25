using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace HrApp.MVC.Controllers
{
    public class HomeController : Controller
    {


        public INotyfService _notifyService { get; }
        private readonly LoginClientService loginClientService;
        private readonly PersonelClientService personelClientService;

        public HomeController(INotyfService notifyService,LoginClientService loginClientService,PersonelClientService personelClientService)
        {
            _notifyService = notifyService;
            this.loginClientService = loginClientService;
            this.personelClientService = personelClientService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await personelClientService.GetAppUserHomeViewModelAsync("2e1b2611-f6cf-451d-9836-49f28b390f76");

            if (items != null)
            {
                _notifyService.Success($"Welcome {items.Name + (!String.IsNullOrEmpty(items.SecondName) ? items.SecondName : "")}!");

                return View(items);
            }

            _notifyService.Error("User home information acquistion error!");

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            var success = await loginClientService.LoginAsync(loginViewModel);

            if (success == false)
            {
                _notifyService.Error("User login error!");
                return View();
            }

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}