using PhonebookV3.Core;
using PhonebookV3.Core.DataTransferObjects;

namespace PhonebookV3.Models
{
    public class ContactDetailsViewModel
    {
        // CONSTRUCTORS
        public ContactDetailsViewModel() { }

        public ContactDetailsViewModel(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        public IContactsService _contactsService;

        // PROPERTIES
        public ContactData Details { get; set; }
        public bool exists { get; set; }
        public bool success { get; set; }
        public bool fromAdd { get; set; }
        public bool fromEdit { get; set; }
        public string errorMsg { get; set; }

        // METHODS
        public void AddService(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }
        public async Task AddToPhonebook()
        {
            string Msg = await _contactsService.InsertContact(this.Details);

            this.success = Msg.Equals("OK") ? true : false;
            if(!this.success)
            {
                this.errorMsg = Msg;
            }
        }

        public async Task Find(int id)
        {
            this.Details = await _contactsService.Search(id);
            this.exists = this.Details == null ? false : true;
        }

        public async Task SaveChanges()
        {
            string Msg = await _contactsService.UpdateContact(this.Details);
            this.success = Msg.Equals("OK") ? true : false;
            if(!this.success)
            {
                this.errorMsg = Msg;
            }
        }
    }
}
