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
        private readonly ResponseHandler responseHandler;

        // GET: ApprovalController
        public ApprovalController(LeaveClientService leaveClientService, AdvanceClientService advanceClientService, ExpenseClientService expenseClientService, ApprovalClientService approvalClientService, ResponseHandler responseHandler)
        {
            this.leaveClientService = leaveClientService;
            this.advanceClientService = advanceClientService;
            this.expenseClientService = expenseClientService;
            this.approvalClientService = approvalClientService;
            this.responseHandler = responseHandler;
        }
        public async Task<ActionResult> Index()
        {

            return View();
        }

        public async Task<ActionResult> GetLeaves()
        {
            var temp = leaveClientService.GetLeaves(User.FindFirstValue("company")).Result.Where(x => x.ApprovalStatus.ToLower().Contains("waiting")).ToList();
            return View(temp);
        }

        public async Task<ActionResult> GetAdvances()
        {
            var temp = advanceClientService.GetAdvances(User.FindFirstValue("company")).Result.Data.Where(x => x.ApprovalStatus.ToLower().Contains("waiting")).ToList();
            return View(temp);
        }

        public async Task<ActionResult> GetExpenses()
        {
            var temp = expenseClientService.GetExpenses(User.FindFirstValue("company")).Result.Data.Where(x => x.ApprovalStatus.ToLower().Contains("waiting")).ToList();
            return View(temp);
        }
        public async Task<ActionResult> Approve(string id, string data, bool isApproved)
        {
            if (data == "leave")
                return (ActionResult)responseHandler.HandleResponse(await approvalClientService.Approve(id, data, isApproved), "GetLeaves", "GetLeaves", this);
            else if (data == "advance")
                return (ActionResult)responseHandler.HandleResponse(await approvalClientService.Approve(id, data, isApproved), "GetExpense", "GetExpense", this);
            else
                return (ActionResult)responseHandler.HandleResponse(await approvalClientService.Approve(id, data, isApproved), "GetExpenses", "GetExpenses", this);
        }

    }
}
