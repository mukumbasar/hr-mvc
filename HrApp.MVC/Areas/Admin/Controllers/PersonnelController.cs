using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.Areas.Admin.Models.Personnel;
using HrApp.MVC.ClientServices;
using HrApp.MVC.Models.Personnel;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HrApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        [HttpGet]
        public async Task<IActionResult> ActiveList()
        {
            var temp = await personelClientService.GetAppUserAsync();
            ViewBag.Personnels = temp.Data.Where(x => x.IsActive).ToList();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> PassiveList()
        {
            var temp = await personelClientService.GetAppUserAsync();
            ViewBag.Personnels = temp.Data.Where(x => !x.IsActive).ToList();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Listx(string userId) =>
            responseHandler.HandleResponse(await personelClientService.GetAppUserAsync(userId), "List", "List", this);

        [HttpGet]
        public async Task<IActionResult> Add() => View();
        [HttpPost]
        public async Task<IActionResult> Add(AppUserAddViewModel userViewModel) =>
             responseHandler.HandleResponse(await personelClientService.AddAppUserAddViewModelAsync(userViewModel, ModelState), "ActiveList", "Add", this);
    }
}
