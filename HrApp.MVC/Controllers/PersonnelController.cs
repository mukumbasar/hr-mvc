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
        private readonly PersonelClientService personelClientService;
        private readonly ResponseHandler responseHandler;

        public INotyfService _notifyService { get; }
        public PersonnelController(INotyfService notifyService, AppUserUpdateViewModelValidator updateValidator, PersonelClientService personelClientService, ResponseHandler responseHandler)
        {
            _notifyService = notifyService;
            this.personelClientService = personelClientService;
            this.responseHandler = responseHandler;
        }
        public async Task<IActionResult> Details(string id) =>
            responseHandler.HandleResponse(await personelClientService.GetAppUserDetailViewModelAsync("2e1b2611-f6cf-451d-9836-49f28b390f76"), "details", "index", this);

        [HttpGet]
        public async Task<IActionResult> UpdateAsync() =>
            responseHandler.HandleResponse(await personelClientService.GetAppUserUpdateAsync("2e1b2611-f6cf-451d-9836-49f28b390f76"), "update", "Index", this);


        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateViewModel userViewModel) =>
            responseHandler.HandleResponse(await personelClientService.UpdateAppUserUpdateViewModelAsync(userViewModel, ModelState), "details", "update", this);


    }
}
