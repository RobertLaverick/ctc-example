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
    public class DeparturesController : Controller
    {
        private readonly ILogger<DeparturesController> _logger;

        public DeparturesController(ILogger<DeparturesController> logger)
        {
            _logger = logger;
        }
    }
}
