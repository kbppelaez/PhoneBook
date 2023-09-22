using PhonebookV3.Core;
using PhonebookV3.Core.DataTransferObjects;

namespace PhonebookV3.Models
{
    public class ContactSearchViewModel
    {
        public readonly IContactsService _contactsService;

        public ContactListData[] Contacts { get; set; }
        public ContactSearchQueryData args { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public ContactSearchViewModel(IContactsService contactService)
        {
            _contactsService = contactService;
        }

        public async Task Initialize(ContactSearchQueryData queryData)
        {
            args = queryData;
            var count = await _contactsService.CountAll(args);
            Contacts = await _contactsService.GetAll(queryData);

            InitializePageTracker(count);
        }

        public void InitializePageTracker(int count)
        {
            TotalPages = (int)Math.Ceiling(count/ (double) args.PageSize);
            HasNextPage = (args.Page == TotalPages - 1) ? false : true;
            HasPreviousPage = (args.Page == 0) ? false : true;
        }
    }
}
