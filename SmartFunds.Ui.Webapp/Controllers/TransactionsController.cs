using Microsoft.AspNetCore.Mvc;
using SmartFunds.Model;
using SmartFunds.Services;

namespace SmartFunds.Ui.Webapp.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly OrganizationService _organizationService;

        public TransactionsController(
            TransactionService transactionService, 
            OrganizationService organizationService)
        {
            _transactionService = transactionService;
            _organizationService = organizationService;
        }

        public IActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                var organization = _organizationService.Get(id.Value);
                if (organization is null)
                {
                    return RedirectToAction("Index", "Organization");
                }

                ViewData["Organization"] = organization;
            }

            var transactions = _transactionService.Find(id);

            return View(transactions);
        }

        [HttpGet]
        public IActionResult Create(int? organizationId)
        {
            if (organizationId.HasValue)
            {
                var transaction = new Transaction
                {
                    OrganizationId = organizationId.Value,
                    Owner = string.Empty,
                    Remarks = string.Empty
                };
                return View(transaction);
            }

            var organizations = _organizationService.Find();
            ViewBag.Organizations = organizations;
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return View(transaction);
            }

            _transactionService.Create(transaction);

            return RedirectToAction("Index", new{Id = transaction.OrganizationId});
        }

        [HttpPost("[controller]/Delete/{id:int?}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var transaction = _transactionService.Get(id);

            if (transaction is null)
            {
                return RedirectToAction("Index", "Organization");
            }

            _transactionService.Delete(id);

            return RedirectToAction("Index", new{Id = transaction.OrganizationId});
        }
    }
}
