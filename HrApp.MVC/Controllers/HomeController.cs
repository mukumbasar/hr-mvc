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

        public HomeController(INotyfService notifyService,LoginClientService loginClientService)
        {
            _notifyService = notifyService;
            this.loginClientService = loginClientService;
        }

        public async Task<IActionResult> Index()
        {
            using HttpClient client = new HttpClient();

            var response = await client.GetAsync("https://ank14hr.azurewebsites.net/api/User/AppUserHome/2e1b2611-f6cf-451d-9836-49f28b390f76");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;

                var userHomeModel = JsonConvert.DeserializeObject<AppUserHomeViewModel>(responseData);

                _notifyService.Success($"Welcome {userHomeModel.Name + (!String.IsNullOrEmpty(userHomeModel.SecondName) ? userHomeModel.SecondName : "")}!");

                return View(userHomeModel);
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