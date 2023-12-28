using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.ClientServices;
using HrApp.MVC.Models.Advance;
using HrApp.MVC.Models.Expense;
using HrApp.MVC.Models.Leave;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HrApp.MVC.Controllers
{
    [Authorize]
    public class LeaveController : Controller
    {
        public INotyfService _notifyService { get; }
        private LeaveClientService _leaveClientService;

        public LeaveController(INotyfService notifyService, LeaveClientService leaveClientService)
        {
            _notifyService = notifyService;
            _leaveClientService = leaveClientService;
        }

        public async Task<IActionResult> Index()
        {
            var leaveTypes = await _leaveClientService.GetLeaveTypes();

            List<SelectListItem> leaveTypeItems = new List<SelectListItem>();

            foreach (var item in leaveTypes)
            {
                leaveTypeItems.Add(new SelectListItem(item.Name + "-" + item.Amount, item.Id.ToString()));
            };

            var leaveTypesList = new SelectList(leaveTypeItems);

            ViewBag.leaveTypes = leaveTypesList.Items;
            ViewBag.Leaves = await _leaveClientService.GetLeaves();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveViewModel createLeaveViewModel)
        {
            var result = await _leaveClientService.CreateLeave(createLeaveViewModel);

            if (result.IsSuccess)
            {
                _notifyService.Success(result.Message);
            }

            _notifyService.Error(result.Message);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateLeaveViewModel updateLeaveViewModel)
        {
            var result = await _leaveClientService.UpdateLeave(updateLeaveViewModel);

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
            var result = await _leaveClientService.DeleteLeave(id);

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
            using HttpClient client = new HttpClient();

            var response = await client.GetAsync("https://ank14hr.azurewebsites.net/api/Leave/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;

                var vm = JsonConvert.DeserializeObject<UpdateLeaveViewModel>(responseData);

                return PartialView("_LeavePartialView", vm);
            }

            _notifyService.Error("Error!");

            return View();
        }
    }
}
