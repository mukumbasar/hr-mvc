using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.ClientServices;
using HrApp.MVC.Helpers;
using HrApp.MVC.Models.Advance;
using HrApp.MVC.Models.Expense;
using HrApp.MVC.Validator;
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
        private readonly ResponseHandler responseHandler;

        public ExpenseController(INotyfService notifyService, ExpenseClientService expenseClientService, CommonClientService commonClientService, ResponseHandler responseHandler)
        {
            _notifyService = notifyService;
            _expenseClientService = expenseClientService;
            _commonClientService = commonClientService;
            this.responseHandler = responseHandler;
        }

        public async Task<IActionResult> Index()
        {
            var expenseTypes = await _expenseClientService.GetExpenseTypes();

            var currencies = await _commonClientService.GetCurrencies();
            ViewBag.ExpenseTypes = currencies;
            ViewBag.Currencies = expenseTypes;
            ViewBag.Expenses = _expenseClientService.GetExpenses().Result.Data;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseViewModel createExpenseViewModel)
        {
            createExpenseViewModel.Document = await ImageConversions.ConvertToByteArrayAsync(createExpenseViewModel.File);
            return responseHandler.HandleResponse(await _expenseClientService.CreateExpense(createExpenseViewModel, ModelState), "Index", "Index", this);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateExpenseViewModel updateExpenseViewModel)
        {

            return responseHandler.HandleResponse(await _expenseClientService.UpdateExpense(updateExpenseViewModel, ModelState), "Index", "Index", this);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return responseHandler.HandleResponse(await _expenseClientService.DeleteExpense(id), "Index", "Index", this);
        }

        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            var expenseTypes = await _expenseClientService.GetExpenseTypes();

            var currencies = await _commonClientService.GetCurrencies();

            ViewBag.ExpenseTypes = expenseTypes;
            ViewBag.Currencies = currencies;

            var result = await _expenseClientService.GetExpense(id);
            return responseHandler.HandleResponse(await _expenseClientService.GetExpense(id), "_ExpensePartialView", "Index", this);
        }
    }
}
