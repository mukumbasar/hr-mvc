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

        public HomeController(INotyfService notifyService)
        {
            _notifyService = notifyService;
        }

        public async Task<IActionResult> Index()
        {
            using HttpClient client = new HttpClient();

            var response = await client.GetAsync("https://localhost:7213/api/AppUserHome/{Id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;

                var userHomeModel = JsonConvert.DeserializeObject<PersonelHomeViewModel>(responseData);

                _notifyService.Success("This is a success notification.");

                return View(userHomeModel);
            }

            _notifyService.Error("This is an error notification.");

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}