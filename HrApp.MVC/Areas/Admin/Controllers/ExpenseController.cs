using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.ClientServices;
using HrApp.MVC.Models.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HrApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            ViewBag.ExpenseTypes= await _expenseClientService.GetExpenseTypes();
            ViewBag.Currencies= await _commonClientService.GetCurrencies();
            var temp = await _expenseClientService.GetExpenses();
            ViewBag.Expenses = temp.Data;

            return View();
        }
    }
}
