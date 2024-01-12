using HrApp.MVC;
using HrApp.MVC.ClientServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public ActionResult GetLeaves()
        {
            var temp = leaveClientService.GetLeaves(User.FindFirstValue("company")).Result.Where(x => x.ApprovalStatus.ToLower().Contains("waiting")).ToList();
            return View(temp);
        }

        public ActionResult GetAdvances()
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
                return (ActionResult)responseHandler.HandleResponse(await approvalClientService.Approve(id, data, isApproved), "GetAdvances", "GetAdvances", this);
            else
                return (ActionResult)responseHandler.HandleResponse(await approvalClientService.Approve(id, data, isApproved), "GetExpenses", "GetExpenses", this);
        }

        [HttpGet]
        public async Task<IActionResult> GetReport(int id)
        {
            var temp = await expenseClientService.GetExpenseFile(id);

            // Remove the prefix if it exists
            string base64String = temp.Data.ConvertedFile;
            if (base64String.StartsWith("data:image/jpg;base64,"))
            {
                base64String = base64String.Substring("data:image/jpg;base64,".Length);
                byte[] imageBytes = Convert.FromBase64String(base64String);

                // Return the byte array as an image file
                return File(imageBytes, "image/jpeg");
            }

            try
            {
                int commaIndex = base64String.IndexOf(',');
                if (commaIndex != -1)
                {
                    // Remove the data type and encoding information (e.g., "data:image/jpg;base64,")
                    base64String = base64String.Substring(commaIndex + 1);
                }
                // Decode Base64 string to byte array
                byte[] fileBytes = Convert.FromBase64String(base64String);

                // Return the byte array as a PDF file
                return File(fileBytes, "application/pdf");
            }
            catch (FormatException ex)
            {
                // Handle the exception or log the error
                // ...
                return BadRequest("Invalid Base64 format");
            }
        }

    }
}
