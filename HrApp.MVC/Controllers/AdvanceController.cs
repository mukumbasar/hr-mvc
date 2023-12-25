using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.Models.Advance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HrApp.MVC.Controllers
{
    public class AdvanceController : Controller
    {
        public INotyfService _notifyService { get; }

        public AdvanceController(INotyfService notifyService)
        {
            _notifyService = notifyService;
        }

        public async Task<IActionResult> Index()
        {
            //var advanceTypes = AdvanceTypeViewModel'a uygun bir şekilde api'dan çekilecek.
            //var currencies = CurrencyViewModel'a uygun bir şekilde api'dan çekilecek.

            List<SelectListItem> advanceTypeItems = new List<SelectListItem>();
            List<SelectListItem> currencyItems = new List<SelectListItem>();

            //foreach (var item in advanceTypes)
            //{
            //    advanceTypeItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
            //};

            //foreach (var item in currencies)
            //{
            //    currencyItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
            //};

            var advanceTypesList = new SelectList(advanceTypeItems);
            var currencyList = new SelectList(currencyItems);

            ViewBag.AdvanceTypes = advanceTypesList.Items;
            ViewBag.Currencies = currencyList.Items;
            //ViewBag.Advances = apidan expenseler istenecek. dönütün çıktısı, ReadAdvancesViewModel'a uygun olacak yani değişkenler uyuşacak.

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvanceViewModel createAdvanceViewModel)
        {
            //vm'yi gönder ve result'tan bağımsız olarak aşağıdaki gibi yönlendir.
            _notifyService.Success("Success!");
            _notifyService.Error("Error!");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAdvanceViewModel updateAdvanceViewModel)
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

            var response = await client.GetAsync("https://ank14hr.azurewebsites.net/api/Advance/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;

                var vm = JsonConvert.DeserializeObject<UpdateAdvanceViewModel>(responseData);

                return PartialView("_AdvancePartialView", vm);
            }

            _notifyService.Error("Error!");

            return View(); 
        }
    }
}
