using System.Security.Claims;
using System.Text;
using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.Helpers;
using HrApp.MVC.Models;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;

namespace HrApp.MVC.Controllers
{

    public class PersonnelController : Controller
    {
        private readonly AppUserUpdateViewModelValidator updateValidator;
        private readonly PersonelClientService personelClientService;

        public INotyfService _notifyService { get; }
        public PersonnelController(INotyfService notifyService, AppUserUpdateViewModelValidator updateValidator, PersonelClientService personelClientService)
        {
            _notifyService = notifyService;
            this.updateValidator = updateValidator;
            this.personelClientService = personelClientService;
        }
        public async Task<IActionResult> Details(string id)
        {
            id = "2e1b2611-f6cf-451d-9836-49f28b390f76";
            var items = await personelClientService.GetAppUserDetailViewModelAsync(id);

            if (items != null)
            {
                _notifyService.Success($"Welcome {items.Name + (!String.IsNullOrEmpty(items.SecondName) ? items.SecondName : "")}!");

                return View(items);
            }
            _notifyService.Error("User home information acquistion error!");

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateAsync()
        {

            var response = await personelClientService.GetAppUserUpdateAsync("2e1b2611-f6cf-451d-9836-49f28b390f76");
            if (response != null)
            {
                return View(response);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateViewModel userViewModel)
        {

            if (updateValidator.ValidateAsync(userViewModel).Result.IsValid)
            {
                var result = await personelClientService.UpdateAppUserUpdateViewModelAsync(userViewModel);
                if (!result)
                {

                    _notifyService.Error("User update error!");
                    return View(userViewModel);
                }
                return RedirectToAction("Index", "Home");

            }
            PostValidationErrors.AddToModelState(updateValidator.ValidateAsync(userViewModel).Result, ModelState);
            _notifyService.Error("User update error!");

            return View(userViewModel);
        }

    }
}
