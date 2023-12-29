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

            List<SelectListItem> advanceTypeItems = new List<SelectListItem>();
            List<SelectListItem> currencyItems = new List<SelectListItem>();

            foreach (var item in advanceTypes)
            {
                advanceTypeItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
            };

            foreach (var item in currencies)
            {
                currencyItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
            };

            var advanceTypesList = new SelectList(advanceTypeItems);
            var currencyList = new SelectList(currencyItems);

            ViewBag.AdvanceTypes = advanceTypesList.Items;
            ViewBag.Currencies = currencyList.Items;
            ViewBag.Advances = await _advanceClientService.GetAdvances();

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

            List<SelectListItem> advanceTypeItems = new List<SelectListItem>();
            List<SelectListItem> currencyItems = new List<SelectListItem>();

            foreach (var item in advanceTypes)
            {
                advanceTypeItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
            };

            foreach (var item in currencies)
            {
                currencyItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
            };

            var advanceTypesList = new SelectList(advanceTypeItems);
            var currencyList = new SelectList(currencyItems);

            ViewBag.AdvanceTypes = advanceTypesList.Items;
            ViewBag.Currencies = currencyList.Items;

            if (result.IsSuccess) return PartialView("_AdvancePartialView", result.Data);

            _notifyService.Error("Error");

            return View("Index");
        }
    }
}
