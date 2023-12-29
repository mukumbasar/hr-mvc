using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.ClientServices;
using HrApp.MVC.Models.Advance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HrApp.MVC.Controllers
{
    [Authorize]
    public class AdvanceController : Controller
    {
        public INotyfService _notifyService { get; }
        private AdvanceClientService _advanceClientService;
        private CommonClientService _commonClientService;
        private readonly ResponseHandler _responseHandler;

        public AdvanceController(INotyfService notifyService, AdvanceClientService advanceClientService, CommonClientService commonClientService, ResponseHandler responseHandler)
        {
            _notifyService = notifyService;
            _advanceClientService = advanceClientService;
            _commonClientService = commonClientService;
            _responseHandler = responseHandler;
        }

        public async Task<IActionResult> Index()
        {
            var advanceTypes = await _advanceClientService.GetAdvanceTypes();
            var currencies = await _commonClientService.GetCurrencies();

            ViewBag.AdvanceTypes = advanceTypes;
            ViewBag.Currencies = currencies;

            ViewBag.Advances = _advanceClientService.GetAdvances().Result.Data;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvanceViewModel createAdvanceViewModel)
        {
            return _responseHandler.HandleResponse(await _advanceClientService.CreateAdvance(createAdvanceViewModel, ModelState), "Index", "Index", this);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAdvanceViewModel updateAdvanceViewModel)
        {
            return _responseHandler.HandleResponse(await _advanceClientService.UpdateAdvance(updateAdvanceViewModel, ModelState), "Index", "Index", this);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return _responseHandler.HandleResponse(await _advanceClientService.DeleteAdvance(id), "Index", "Index", this);
        }

        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            var result = await _advanceClientService.GetAdvance(id);

            var advanceTypes = await _advanceClientService.GetAdvanceTypes();
            var currencies = await _commonClientService.GetCurrencies();

            ViewBag.AdvanceTypes = advanceTypes;
            ViewBag.Currencies = currencies;

            ViewBag.Advances = _advanceClientService.GetAdvances().Result.Data;

            if (result.IsSuccess) return PartialView("_AdvancePartialView", result.Data);

            _notifyService.Error("Error");

            return View("Index");
        }
    }
}
