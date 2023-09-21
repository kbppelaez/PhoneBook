using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhonebookV2.Core;
using PhonebookV2.Core.Application;
using PhonebookV2.Data;
using PhonebookV2.Models;
using System.Diagnostics;
using System.Transactions;

namespace PhonebookV2.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactsService _contactsService;
        private readonly ILogger<ContactsController> _logger;
        private readonly ContactsTable _contacts = new ContactsTable();

        public ContactsController(IContactsService contacts, ILogger<ContactsController> logger)
        {
            _contactsService = contacts;
            _logger = logger;
        }

        [Route("/")]
        [Route("/contacts")]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]ContactSearchQuery args)
        {
            var vm = new ContactsSearchViewModel(_contactsService);
            await vm.Initialize(args);
            
            return View(vm);
        }

        [Route("contacts/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> ViewContact(int id)
        {
            ContactsView result = await _contacts.Find(id);

            if (result.exists.HasValue && result.exists.Value)
            {
                return View(result);
            }
            else return NotFound();
        }

        [Route("contacts/{id:int}/edit")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditContact(int id)
        {
            ContactsView result = await _contacts.Find(id);

            if (result.exists.HasValue && result.exists.Value)
            {
                return View(result);
            }
            else return NotFound();
        }

        [Route("contacts/{id:int}/edit")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SaveChanges([FromForm] ContactsView c, int id)
        {
            if (c.ContactId != id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View("EditContact", c);
            }
            else
            {
                var result = await _contacts.SaveChanges(c);

                return View("ViewContact", result);
            }
        }

        [Route("contacts/{id:int}/delete")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteContact(int id)
        {
            ContactsView contact = await _contacts.Find(id);

            if (contact.exists.HasValue && contact.exists.Value)
            {
                return View(contact);
            }
            else return NotFound();
        }

        [Route("/contacts/{id:int}/delete")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteContactConfirmed([FromForm] int ContactId)
        {
            if (await _contacts.DeleteContact(ContactId))
            {
                return RedirectToAction(nameof(Index));
            }
            else return NotFound();
        }

        [Route("/contacts/create")]
        [HttpGet]
        [Authorize]
        public IActionResult NewContact()
        {
            return View();
        }

        [Route("/contacts/create")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateContact([FromForm] ContactsView c)
        {
            if (await _contacts.CreateContact(c))
            {
                c.addSuccess = true;
                return View("ViewContact", c);
            }
            else return NotFound();
        }

        [Route("/contacts/search/{term?}")]
        [HttpGet]
        public async Task<IActionResult> SearchContact(string term)
        {
            if (term == null)
            {
                return View();
            }
            else
            {
                SearchListView searchView = new SearchListView();
                searchView.term = term;
                searchView.Contacts = await _contacts.SearchContacts(term);
                return View(searchView);
            }
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