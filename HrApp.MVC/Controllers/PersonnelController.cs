using System.Text;
using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.Helpers;
using HrApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;

namespace HrApp.MVC.Controllers
{

    public class PersonnelController : Controller
    {
        public INotyfService _notifyService { get; }
        public PersonnelController(INotyfService notifyService)
        {
            _notifyService = notifyService;
        }
        public async Task<IActionResult> Details(string id)
        {
            using HttpClient client = new HttpClient();

            var response = await client.GetAsync("https://ank14hr.azurewebsites.net/api/User/AppUserDetail/2e1b2611-f6cf-451d-9836-49f28b390f76");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;

                var userDetailModel = JsonConvert.DeserializeObject<AppUserDetailViewModel>(responseData);

                _notifyService.Success($"Welcome {userDetailModel.Name + (!String.IsNullOrEmpty(userDetailModel.SecondName) ? userDetailModel.SecondName : "")}!");

                return View(userDetailModel);
            }

            _notifyService.Error("User home information acquistion error!");

            return View();
        }

        public async Task<IActionResult> UpdateAsync()
        {

            using HttpClient client = new HttpClient();

            var response = await client.GetFromJsonAsync<AppUserUpdateViewModel>("https://ank14hr.azurewebsites.net/api/User/AppUserDetail/2e1b2611-f6cf-451d-9836-49f28b390f76");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateViewModel userViewModel)
        {
            using HttpClient client = new HttpClient();

            var bytes = await ImageConversions.ConvertToByteArrayAsync(userViewModel.NewImage);

            userViewModel.UpdatedImage = bytes;

            var response = client.PutAsJsonAsync("https://localhost:7213/api/User/UpdateAppUser", userViewModel).Result;

            System.Console.WriteLine(response.ToString());

            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
