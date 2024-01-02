using Microsoft.AspNetCore.Mvc;

namespace HrApp.MVC.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
