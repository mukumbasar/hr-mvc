using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.ClientServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class LeaveController : Controller
    {
        public INotyfService _notifyService { get; }
        public LeaveClientService _leaveClientService;
        public ResponseHandler _responseHandler;

        public LeaveController(INotyfService notifyService, LeaveClientService leaveClientService, ResponseHandler responseHandler)
        {
            _notifyService = notifyService;
            _leaveClientService = leaveClientService;
            _responseHandler = responseHandler;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.LeaveType= await _leaveClientService.GetLeaveTypes();
            ViewBag.LeaveType = await _leaveClientService.GetLeaves();
            return View();
        }
    }
}
