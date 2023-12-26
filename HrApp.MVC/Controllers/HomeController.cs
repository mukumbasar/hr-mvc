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
        private readonly ResponseHandler responseHandler;

        public HomeController(INotyfService notifyService, LoginClientService loginClientService, PersonelClientService personelClientService, ResponseHandler responseHandler)
        {
            _notifyService = notifyService;
            this.loginClientService = loginClientService;
            this.personelClientService = personelClientService;
            this.responseHandler = responseHandler;
        }

        public async Task<IActionResult> Index() =>
            responseHandler.HandleResponse(await personelClientService.GetAppUserHomeViewModelAsync("2e1b2611-f6cf-451d-9836-49f28b390f76"), "Index", "Index", this);

        public IActionResult Login() =>
            View();


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