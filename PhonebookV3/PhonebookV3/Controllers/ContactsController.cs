using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhonebookV3.Core;
using PhonebookV3.Core.DataTransferObjects;
using PhonebookV3.Data;
using PhonebookV3.Models;
using System.Diagnostics;

namespace PhonebookV3.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactsService _contactsService;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(IContactsService contacts, ILogger<ContactsController> logger)
        {
            _contactsService = contacts;
            _logger = logger;
        }

        // INDEX
        [Route("/")]
        [Route("/contacts", Name = "index")]
        public async Task<IActionResult> Index([FromQuery] ContactSearchQueryData args)
        {
            var vm = new ContactSearchViewModel(_contactsService);
            await vm.Initialize(args);
            return View(vm);
        }

        // CREATE
        [Route("/contacts/create")]
        [HttpGet]
        public IActionResult NewContact()
        {
            return View(new ContactDetailsViewModel());
        }

        [Route("/contacts/create")]
        [HttpPost]
        public async Task<IActionResult> SaveChanges([FromForm] ContactDetailsViewModel newcontact)
        {
            newcontact.AddService(_contactsService);
            await newcontact.AddToPhonebook();
            newcontact.fromAdd = true;
            return View("ViewContact", newcontact);
        }

        // VIEW
        [Route("/contacts/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> ViewContact(int id)
        {
            var vm = new ContactDetailsViewModel(_contactsService);

            await vm.Find(id);

            if (vm.exists)
            {
                return View(vm);
            }
            else return NotFound();
        }

        // EDIT
        [Route("contacts/{id:int}/edit")]
        [HttpGet]
        public async Task<IActionResult> EditContact(int id)
        {
            var vm = new ContactDetailsViewModel(_contactsService);

            await vm.Find(id);

            if (vm.exists)
            {
                return View(vm);
            }
            else return NotFound();
        }

        [Route("contacts/{id:int}/edit")]
        [HttpPost]
        public async Task<IActionResult> SaveChanges([FromForm] ContactDetailsViewModel c, int id)
        {
            if (c.Details.Id != id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View("EditContact", c);
            }
            else
            {
                c.AddService(_contactsService);
                await c.SaveChanges();
                c.fromEdit = true;

                return View("ViewContact", c);
            }
        }

        // DELETE
        [Route("contacts/{id:int}/delete")]
        [HttpGet]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var vm = new ContactDetailsViewModel(_contactsService);

            await vm.Find(id);

            if (vm.exists)
            {
                return View(vm);
            }
            else return NotFound();
        }

        [Route("/contacts/{id:int}/delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteContactConfirmed([FromForm] int id)
        {
            string result = await _contactsService.DeleteContact(id);
            if (result.Equals("OK"))
            {
                return RedirectToAction(nameof(Index));
            }
            else return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}