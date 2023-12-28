using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.Models.Advance;
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

        public LeaveController(INotyfService notifyService)
        {
            _notifyService = notifyService;
        }

        public async Task<IActionResult> Index()
        {
            //var leaveTypes = leaveTypeViewModel'a uygun bir şekilde api'dan çekilecek.

            List<SelectListItem> leaveTypeItems = new List<SelectListItem>();

            //foreach (var item in leaveTypes)
            //{
            //    leaevTypeItems.Add(new SelectListItem(item.Name+"-"+item.Amount, item.Id.ToString()));
            //};

            var leaveTypesList = new SelectList(leaveTypeItems);

            ViewBag.leaveTypes = leaveTypesList.Items;
            //ViewBag.Leaves = apidan expenseler istenecek. dönütün çıktısı, ReadLeaveViewModel'a uygun olacak yani değişkenler uyuşacak.

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveViewModel createLeaveViewModel)
        {
            //vm'yi gönder ve result'tan bağımsız olarak aşağıdaki gibi yönlendir.
            _notifyService.Success("Success!");
            _notifyService.Error("Error!");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateLeaveViewModel updateLeaveViewModel)
        {
            //vm'yi gönder ve result'tan bağımsız olarak aşağıdaki gibi yönlendir.
            _notifyService.Success("Success!");
            _notifyService.Error("Error!");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //id'yi gönder ve result'tan bağımsız olarak aşağıdaki gibi yönlendir.
            _notifyService.Success("Success!");
            _notifyService.Error("Error!");
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
