using HrApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HrApp.MVC.Controllers
{
   
    public class PersonelController : Controller
    {

        PersonelDetailViewModel user = new PersonelDetailViewModel() { 
            Id = "1", 
            Name = "Name1", 
            Surname = "Surname1", 
            Email = "Email1", 
            Address = "Address1", 
            BirthPlace = "BirthPlace1", 
            BirthYear = DateTime.Now, 
            Department = "Department1",
            StartDate = DateTime.Now, 
            TurkishIdentificationNumber = "12345678910",
            IsActive = true,
            CompanyName = "Koç",
            Occupation = "Paspasçı",
            MobileNumber = "053255323232"
        };


        public IActionResult Index()
        {
            
            return View();
        }


        public IActionResult Details()
        {
            return View(user);
        }

    }
}
