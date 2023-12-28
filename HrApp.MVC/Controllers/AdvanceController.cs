using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.ClientServices;
using HrApp.MVC.Models.Advance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HrApp.MVC.Controllers
{
    public class AdvanceController : Controller
    {
        public INotyfService _notifyService { get; }
        private AdvanceClientService _advanceClientService;
        private CommonClientService _commonClientService;

        public AdvanceController(INotyfService notifyService, AdvanceClientService advanceClientService, CommonClientService commonClientService)
        {
            _notifyService = notifyService;
            _advanceClientService = advanceClientService;
            _commonClientService = commonClientService;
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
            var result = await _advanceClientService.CreateAdvance(createAdvanceViewModel);

            if(result.IsSuccess)
            {
                _notifyService.Success(result.Message);
            }
            
            _notifyService.Error(result.Message);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAdvanceViewModel updateAdvanceViewModel)
        {
            var result = await _advanceClientService.UpdateAdvance(updateAdvanceViewModel);

            if (result.IsSuccess)
            {
                _notifyService.Success(result.Message);
            }

            _notifyService.Error(result.Message);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _advanceClientService.DeleteAdvance(id);

            if (result.IsSuccess)
            {
                _notifyService.Success(result.Message);
            }

            _notifyService.Error(result.Message);

            return RedirectToAction("Index");
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

            _notifyService.Error(result.Message);

            return View("Index"); 
        }
    }
}
