using PhonebookV2.Core;

namespace PhonebookV2.Models
{
    public class ContactsSearchViewModel
    {
        private readonly IContactsService _contactsService;

        public ContactsSearchViewModel(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        public IEnumerable<ContactData> Contacts { get; set; }
        public ContactSearchQuery QueryArgs { get; set; }

        public async Task Initialize(ContactSearchQuery args)
        {
            QueryArgs = args;
            Contacts = await _contactsService.ListAll(args);
        }
    }
}
