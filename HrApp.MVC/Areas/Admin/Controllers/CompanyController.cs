using Microsoft.AspNetCore.Mvc;

namespace HrApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Add()
        {
            return View();

        }
    }
}
