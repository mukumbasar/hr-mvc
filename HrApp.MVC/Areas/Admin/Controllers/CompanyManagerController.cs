using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.Areas.Admin.Models.Personnel;
using HrApp.MVC.Validator;
using Microsoft.AspNetCore.Mvc;

namespace HrApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyManagerController : Controller
    {
        private readonly PersonelClientService personelClientService;
        private readonly ResponseHandler responseHandler;

        public INotyfService _notifyService { get; }
        public CompanyManagerController(INotyfService notifyService, AppUserUpdateViewModelValidator updateValidator, PersonelClientService personelClientService, ResponseHandler responseHandler)
        {
            _notifyService = notifyService;
            this.personelClientService = personelClientService;
            this.responseHandler = responseHandler;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var temp = await personelClientService.GetAllAdmin();
            ViewBag.CompanyManagers = temp.Data.Where(x => x.IsActive).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AppUserAddViewModel userViewModel) =>
             responseHandler.HandleResponse(await personelClientService.AddAppUserAddViewModelAsync(userViewModel, ModelState, User.IsInRole("WebsiteManager")), "Index", "Add", this);
    }
}
