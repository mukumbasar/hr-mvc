using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.ClientServices;
using HrApp.MVC.Models.Advance;
using HrApp.MVC.Models.Expense;
using HrApp.MVC.Models.Leave;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;

namespace HrApp.MVC.Controllers
{
    [Authorize]
    public class LeaveController : Controller
    {
        public INotyfService _notifyService { get; }
        private LeaveClientService _leaveClientService;
        private ResponseHandler _responseHandler;

        public LeaveController(INotyfService notifyService, LeaveClientService leaveClientService, ResponseHandler responseHandler)
        {
            _notifyService = notifyService;
            _leaveClientService = leaveClientService;
            _responseHandler = responseHandler;
        }

        public async Task<IActionResult> Index()
        {
            var leaveTypes = await _leaveClientService.GetLeaveTypes();
            var leaves = await _leaveClientService.GetLeaves();

            ViewBag.leaveTypes = leaveTypes;
            ViewBag.Leaves = leaves.Where(x => x.AppUserId == User.FindFirstValue("nameid"));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveViewModel createLeaveViewModel) =>
            _responseHandler.HandleResponse(await _leaveClientService.CreateLeave(createLeaveViewModel, ModelState), "Index", "Index", this);


        [HttpPost]
        public async Task<IActionResult> Update(UpdateLeaveViewModel updateLeaveViewModel) =>
            _responseHandler.HandleResponse(await _leaveClientService.UpdateLeave(updateLeaveViewModel), "Index", "Index", this);


        [HttpGet]
        public async Task<IActionResult> Delete(int id) =>
            _responseHandler.HandleResponse(await _leaveClientService.DeleteLeave(id), "Index", "Index", this);


        [HttpGet]
        public async Task<IActionResult> Read(int id) =>
            _responseHandler.HandleResponse(await _leaveClientService.GetLeave(id), "_LeavePartialView", "Index", this);

    }
}
