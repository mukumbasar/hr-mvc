using System.Security.Claims;
using HrApp.MVC;
using HrApp.MVC.ClientServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Area("Admin")]
    [Authorize]
    public class ApprovalController : Controller
    {
        private readonly LeaveClientService leaveClientService;
        private readonly AdvanceClientService advanceClientService;
        private readonly ExpenseClientService expenseClientService;
        private readonly ApprovalClientService approvalClientService;

        // GET: ApprovalController
        public ApprovalController(LeaveClientService leaveClientService, AdvanceClientService advanceClientService, ExpenseClientService expenseClientService, ApprovalClientService approvalClientService)
        {
            this.leaveClientService = leaveClientService;
            this.advanceClientService = advanceClientService;
            this.expenseClientService = expenseClientService;
            this.approvalClientService = approvalClientService;
        }
        public async Task<ActionResult> IndexAsync()
        {
            ViewBag.Leave = leaveClientService.GetLeaves(User.FindFirstValue("company")).Result.Where(x => x.ApprovalStatus.ToLower().Contains("waiting")).ToList();
            ViewBag.Advances = advanceClientService.GetAdvances(User.FindFirstValue("company")).Result.Data.Where(x => x.ApprovalStatus.ToLower().Contains("waiting")).ToList();
            ViewBag.Expenses = expenseClientService.GetExpenses(User.FindFirstValue("company")).Result.Data.Where(x => x.ApprovalStatus.ToLower().Contains("waiting")).ToList();
            return View();
        }
        public async Task<ActionResult> Approve(string id, string data, bool isApproved)
        {
            await approvalClientService.Approve(id, data, isApproved);
            return View();
        }

    }
}
