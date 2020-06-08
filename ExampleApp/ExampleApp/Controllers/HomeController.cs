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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var clientId = "y24eoPeZbqPLlbNH8LenfIk1a61k";
            var scope = "common-transit-convention-traders";
            var target = $"https://api.development.tax.service.gov.uk/oauth/authorize?" +
                            $"response_type=code" +
                            $"&client_id={clientId}" +
                            $"&scope={scope}" +
                            $"&redirect_uri=https://localhost:5001/Home/CodeHook";

            return Redirect(target);
        }

        public async Task<IActionResult> CodeHook(string code)
        {
            var authClient = new AuthClient();
            var tokenDetails = await authClient.GetAuthToken(code);

            HttpContext.Session.SetString("token", tokenDetails.access_token);
            return RedirectToAction("Welcome");
        }

        public IActionResult UseToken(string token)
        {
            HttpContext.Session.SetString("token", token);
            return RedirectToAction("Welcome");
        }

        public async Task<IActionResult> Welcome()
        {
            return View();
        }
    }
}
