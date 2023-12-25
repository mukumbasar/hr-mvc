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
        private readonly PersonelClientService personelClientService;

        public INotyfService _notifyService { get; }

        public HomeController(INotyfService notifyService, PersonelClientService personelClientService)
        {
            _notifyService = notifyService;
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}