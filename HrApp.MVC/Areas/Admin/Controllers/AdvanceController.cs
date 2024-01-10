using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.ClientServices;
using HrApp.MVC.Models.Advance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HrApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            ViewBag.AdvanceTypes = await _advanceClientService.GetAdvanceTypes();
            ViewBag.Currencies = await _commonClientService.GetCurrencies();
            var temp = await _advanceClientService.GetAdvances();
            ViewBag.Advances = temp.Data;

            return View();
        }
    }
}
