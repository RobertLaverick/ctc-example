using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleApp.Clients;
using ExampleApp.Models;
using ExampleApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace ExampleApp.Controllers
{
    public class ArrivalsController : Controller
    {
        private readonly ILogger<ArrivalsController> _logger;

        public ArrivalsController(ILogger<ArrivalsController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> GetUserArrivals()
        {
            var token = HttpContext.Session.GetString("token");
            var client = new HmrcCtcClient("https://api.development.tax.service.gov.uk", token);
            var result = await client.GetUserArrivals();
            return View(result);
        }

        public IActionResult GetArrivalById()
        {
            return View();
        }

        public IActionResult GetArrivalMessages()
        {
            return View();
        }

        public IActionResult GetArrivalMessage()
        {
            return View();
        }

        public async Task<IActionResult> SendArrivalNotification()
        {
            var token = HttpContext.Session.GetString("token");
            var client = new HmrcCtcClient("https://api.development.tax.service.gov.uk", token);
            var result = await client.SendArrivalNotification();
            return Content(result);
        }

        [HttpGet]
        public IActionResult ResubmitArrivalNotification()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResubmitArrivalNotification(string arrivalId)
        {
            var token = HttpContext.Session.GetString("token");
            var client = new HmrcCtcClient("https://api.development.tax.service.gov.uk", token);
            var result = await client.ResubmitArrivalNotification(arrivalId);
            return Content(result);
        }
    }
}
