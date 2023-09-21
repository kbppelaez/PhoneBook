using PhonebookV3.Core;
using PhonebookV3.Core.DataTransferObjects;

namespace PhonebookV3.Models
{
    public class ContactSearchViewModel
    {
        public readonly IContactsService _contactsService;

        public ContactListData[] Contacts { get; set; }
        public ContactSearchQueryData args { get; set; }

        public ContactSearchViewModel(IContactsService contactService)
        {
            _contactsService = contactService;
        }

        public async Task Initialize(ContactSearchQueryData queryData)
        {
            args = queryData;
            Contacts = await _contactsService.GetAll(queryData);
        }
    }
}
