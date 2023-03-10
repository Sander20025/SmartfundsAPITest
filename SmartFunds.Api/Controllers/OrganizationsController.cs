using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartFunds.Model;
using SmartFunds.Services;

namespace SmartFunds.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly OrganizationService _organizationService;

        public OrganizationsController(OrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        // /api/organizations/1
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var organization = _organizationService.Get(id);
            return Ok(organization);
        }

        // /api/organizations
        [HttpGet]
        public IActionResult Find()
        {
            var organizations = _organizationService.Find();
            return Ok(organizations);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Organization organization)
        {
            return Ok();
        }
    }
}
