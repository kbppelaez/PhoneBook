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
            List<ListContactsView> result = await contacts.ListAll();
            return View(result);
        }

        [Route("contacts/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> ViewContact(int id)
        {
            ContactsTable contacts = new ContactsTable();
            ContactsView result = await contacts.Find(id);

            if (result.exists.HasValue && result.exists.Value)
            {
                return View(result);
            }
            else return NotFound();
        }

        [Route("contacts/{id:int}/edit")]
        [HttpGet]
        public async Task<IActionResult> EditContact(int id)
        {
            ContactsTable contacts = new ContactsTable();
            ContactsView result = await contacts.Find(id);

            if (result.exists.HasValue && result.exists.Value)
            {
                return View(result);
            }
            else return NotFound();
        }

        [Route("contacts/{id:int}/edit")]
        [HttpPost]
        public async Task<IActionResult> SaveChanges([FromForm] ContactsView c, int id)
        {
            if (c.ContactId != id)
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return View("EditContact", c);
            }
            else
            {
                ContactsTable contactsTable = new ContactsTable();
                var result = await contactsTable.SaveChanges(c);

                return View("ViewContact", result);
            }
        }

        [Route("contacts/{id:int}/delete")]
        [HttpGet]
        public async Task<IActionResult> DeleteContact(int id)
        {
            ContactsTable contactsTable = new ContactsTable();
            ContactsView contact = await contactsTable.Find(id);

            if (contact.exists.HasValue && contact.exists.Value)
            {
                return View(contact);
            }
            else return NotFound();
        }

        [Route("/contacts/{id:int}/delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteContactConfirmed([FromForm] int ContactId)
        {
            ContactsTable contactsTable = new ContactsTable();
            if(await contactsTable.DeleteContact(ContactId))
            {
                return RedirectToAction(nameof(Index));
            }
            else return NotFound();
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