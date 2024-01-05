using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;

namespace HrApp.MVC.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        private readonly TelemetryClient _telemetryClient;

        public ErrorController()
        {
            
        }

        [Route("500")]
        public IActionResult Exception()
        { 
            return View();
        }

        [Route("404")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
