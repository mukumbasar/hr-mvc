using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.ClientServices;
using HrApp.MVC.Helpers;
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
        public ExpenseClientService _expenseClientService { get; }
        private CommonClientService _commonClientService;

        public ExpenseController(INotyfService notifyService, ExpenseClientService expenseClientService, CommonClientService commonClientService)
        {
            _notifyService = notifyService;
            _expenseClientService = expenseClientService;
            _commonClientService = commonClientService;
        }

        public async Task<IActionResult> Index()
        {
            var expenseTypes = await _expenseClientService.GetExpenseTypes();

            var currencies = await _commonClientService.GetCurrencies();

            List<SelectListItem> expenseTypeItems = new List<SelectListItem>();
            List<SelectListItem> currencyItems = new List<SelectListItem>();

            foreach (var item in expenseTypes)
            {
                expenseTypeItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
            };

            foreach (var item in currencies)
            {
                currencyItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
            };

            var expenseTypesList = new SelectList(expenseTypeItems);
            var currencyList = new SelectList(currencyItems);

            ViewBag.ExpenseTypes = expenseTypesList.Items;
            ViewBag.Currencies = currencyList.Items;
            ViewBag.Expenses = await _expenseClientService.GetExpenses();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseViewModel createExpenseViewModel)
        {
            createExpenseViewModel.Document = await ImageConversions.ConvertToByteArrayAsync(createExpenseViewModel.File);

            var result = await _expenseClientService.CreateExpense(createExpenseViewModel);

            if(result.IsSuccess) 
            {
                _notifyService.Success(result.Message);
            }
            
            _notifyService.Error("Error");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateExpenseViewModel updateExpenseViewModel)
        {
            var result = await _expenseClientService.UpdateExpense(updateExpenseViewModel);

            if (result.IsSuccess)
            {
                _notifyService.Success(result.Message);
            }

            _notifyService.Error("Error");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _expenseClientService.DeleteExpense(id);

            if (result.IsSuccess)
            {
                _notifyService.Success(result.Message);
            }

            _notifyService.Error("Error");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            var expenseTypes = await _expenseClientService.GetExpenseTypes();

            var currencies = await _commonClientService.GetCurrencies();

            List<SelectListItem> expenseTypeItems = new List<SelectListItem>();
            List<SelectListItem> currencyItems = new List<SelectListItem>();

            foreach (var item in expenseTypes)
            {
                expenseTypeItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
            };

            foreach (var item in currencies)
            {
                currencyItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
            };

            var expenseTypesList = new SelectList(expenseTypeItems);
            var currencyList = new SelectList(currencyItems);

            ViewBag.ExpenseTypes = expenseTypesList.Items;
            ViewBag.Currencies = currencyList.Items;

            var result = await _expenseClientService.GetExpense(id);
            
            if (result.IsSuccess)
            {
                return PartialView("_ExpensePartialView", result.Data);
            }

            _notifyService.Error("Error!");

            return RedirectToAction("Index");
        }
    }
}
