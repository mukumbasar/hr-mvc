using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.ClientServices;
using HrApp.MVC.Models;
using HrApp.MVC.Models.Personnel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace HrApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public INotyfService _notifyService { get; }
        private readonly LoginClientService loginClientService;
        private readonly PersonelClientService personelClientService;
        private readonly EmailClientService emailClientService;
        private readonly ResponseHandler responseHandler;

        public HomeController(INotyfService notifyService, LoginClientService loginClientService, PersonelClientService personelClientService, ResponseHandler responseHandler, EmailClientService emailClientService)
        {
            _notifyService = notifyService;
            this.loginClientService = loginClientService;
            this.personelClientService = personelClientService;
            this.responseHandler = responseHandler;
            this.emailClientService = emailClientService;
        }
        [Authorize]
        public async Task<IActionResult> Index() =>
            responseHandler.HandleResponse(await personelClientService.GetAppUserHomeViewModelAsync(User.FindFirstValue("nameid")), "Index", "Index", this);

        public IActionResult Login() =>
            View();


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel) =>
            responseHandler.HandleResponse(await loginClientService.LoginAsync(loginViewModel, HttpContext), "Index", "Index", this);

        [HttpGet]
        public async Task<IActionResult> Logout() =>
            responseHandler.HandleResponse(await loginClientService.LogoutAsync(HttpContext), "Index", "Logout", this);

        [HttpGet]
        public IActionResult SendPasswordEmail() => View();

        [HttpPost]
        public async Task<IActionResult> SendPasswordEmail(ForgetPassViewModel forgetPassViewModel) => responseHandler.HandleResponse(await emailClientService.SendEmail(forgetPassViewModel, ModelState), "Login", "Index", this);


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}