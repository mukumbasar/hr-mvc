using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.ClientServices;
using HrApp.MVC.Models.Advance;
using HrApp.MVC.Models.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HrApp.MVC.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        public INotyfService _notifyService { get; }
        public 

        public ExpenseController(INotyfService notifyService, ExpenseClientService expenseClientService)
        {
            _notifyService = notifyService;
        }

        public async Task<IActionResult> Index()
        {
            var expenseTypes = 
            //var currencies = CurrencyViewModel'a uygun bir şekilde api'dan çekilecek.

            List<SelectListItem> expenseTypeItems = new List<SelectListItem>();
            List<SelectListItem> currencyItems = new List<SelectListItem>();

            //foreach (var item in expenseTypes)
            //{
            //    expenseTypeItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
            //};

            //foreach (var item in currencies)
            //{
            //    currencyItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
            //};

            var expenseTypesList = new SelectList(expenseTypeItems);
            var currencyList = new SelectList(currencyItems);

            ViewBag.ExpenseTypes = expenseTypesList.Items;
            ViewBag.Currencies = currencyList.Items;
            //ViewBag.Expenses = apidan expenseler istenecek. dönütün çıktısı, ReadExpenseViewModel'a uygun olacak yani değişkenler uyuşacak.

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseViewModel createExpenseViewModel)
        {
            //vm'yi gönder ve result'tan bağımsız olarak aşağıdaki gibi yönlendir.
            _notifyService.Success("Success!");
            _notifyService.Error("Error!");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateExpenseViewModel updateExpenseViewModel)
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

            var response = await client.GetAsync("https://ank14hr.azurewebsites.net/api/Expense/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;

                var vm = JsonConvert.DeserializeObject<UpdateExpenseViewModel>(responseData);

                return PartialView("_ExpensePartialView", vm);
            }

            _notifyService.Error("Error!");

            return View();
        }
    }
}
