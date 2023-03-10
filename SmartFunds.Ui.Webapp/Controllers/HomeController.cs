using Microsoft.AspNetCore.Mvc;
using SmartFunds.Core;
using SmartFunds.Services;

namespace SmartFunds.Ui.Webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly OrganizationService _organizationService;


        public HomeController(OrganizationService organizationService)
        {
            _organizationService = organizationService;
        }
        public IActionResult Index()
        {
            var organizations = _organizationService.Find();
            return View(organizations);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}