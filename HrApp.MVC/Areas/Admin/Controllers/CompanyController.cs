using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using HrApp.MVC.Areas.Admin.Models.Company;
using HrApp.MVC.ClientServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly CompanyClientService _companyClientService;
        private readonly ResponseHandler responseHandler;
        public INotyfService _notifyService { get; }

        public CompanyController(INotyfService notifyService, CompanyClientService companyClientService, ResponseHandler responseHandler)
        {
            _notifyService = notifyService;
            _companyClientService = companyClientService;
            this.responseHandler = responseHandler;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanyTypes()
        {
            var companyTypes = await _companyClientService.GetCompanyTypes();
            return Json(companyTypes);
        }

        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyClientService.GetCompanies();
            if (User.FindFirstValue("role") == "Admin")
                companies.Data = companies.Data.Where(x => x.Id == int.Parse(User.FindFirstValue("company"))).ToList();
            return Json(companies);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var temp = await _companyClientService.GetCompanyTypes();
            ViewBag.CompanyTypes = temp;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCompanyViewModel addCompanyViewModel) => responseHandler.HandleResponse(await _companyClientService.CreateCompany(addCompanyViewModel, ModelState), "List", "Add", this);


        public async Task<IActionResult> List()
        {
            var temp = await _companyClientService.GetCompanies();
            ViewBag.Companies = temp.Data.ToList();

            return View();
        }
    }
}
