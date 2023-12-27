using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.Models;
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
        private readonly ResponseHandler responseHandler;

        public HomeController(INotyfService notifyService, LoginClientService loginClientService, PersonelClientService personelClientService, ResponseHandler responseHandler)
        {
            _notifyService = notifyService;
            this.loginClientService = loginClientService;
            this.personelClientService = personelClientService;
            this.responseHandler = responseHandler;
        }
        [Authorize]
        public async Task<IActionResult> Index() =>
                responseHandler.HandleResponse(await personelClientService.GetAppUserHomeViewModelAsync(User.FindFirstValue("nameid")), "Index", "Index", this);

        public IActionResult Login() =>
            View();


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            var success = await loginClientService.LoginAsync(loginViewModel, HttpContext);

            if (success == false)
            {
                _notifyService.Error("User login error!");
                return View();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await LoginHelper.LogoutAsync(HttpContext);
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}