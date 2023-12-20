using HrApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HrApp.MVC.Controllers
{
   
    public class PersonelController : Controller
    {
        

        public IActionResult Index()
        {
            
            return View();
        }

    }
}
