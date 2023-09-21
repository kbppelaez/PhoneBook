using Microsoft.EntityFrameworkCore;
using PhonebookV2.Data;
using PhonebookV2.Models;

namespace PhonebookV2.Core.Application
{
    public class ContactsService :IContactsService
    {
        private readonly PhonebookDbContext _db;

        public ContactsService(PhonebookDbContext db)
        {
            _db = db;
        }

        public async Task<List<ContactData>> ListAll(ContactSearchQuery args)
        {
            IQueryable<Contact> query = _db.Contact;

            if (!string.IsNullOrEmpty(args.Term))
            {
                query = query.Where(c => c.FirstName.Contains(args.Term) || c.LastName.Contains(args.Term));
            }

            query = query
                .OrderBy(c => c.FirstName);

            return await query
                .Select(
                    c => new ContactData
                    {
                        ContactId = c.ContactId,
                        FirstName = c.FirstName,
                        LastName = c.LastName
                    })
                .ToListAsync();
        }
    }
}
