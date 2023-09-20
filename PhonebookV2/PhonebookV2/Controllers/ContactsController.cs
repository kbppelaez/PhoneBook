using Microsoft.AspNetCore.Mvc;
using PhonebookV2.Models;
using System.Diagnostics;

namespace PhonebookV2.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ILogger<ContactsController> logger)
        {
            _logger = logger;
        }

        [Route("/")]
        [Route("/contacts")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ContactsTable contacts = new ContactsTable();
            var result = await contacts.ListAll();
            return View(result);
        }

        [Route("contacts/{id}")]
        public IActionResult ViewContact(int id)
        {
            ContactsTable contacts = new ContactsTable();
            ContactsView result = contacts.Find(id);

            if (result.exists)
            {
                return View(result);
            }
            else return View("error");
        }

        [Route("/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}