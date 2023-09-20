using PhonebookV2.Models;
using Microsoft.EntityFrameworkCore;
using PhonebookV2.Data;

namespace PhonebookV2.Models
{
    public class ContactsTable
    {
        public string errorMsg = string.Empty;
        public bool? isSuccess;

        public ContactsTable() {
        }

        public async Task<List<ListContactsView>> ListAll()
        {
            List<ListContactsView> result;
            using (var _context = new ContactsContext())
            {
                var query = from c in _context.Contact
                            select c;

                query = query.OrderBy(c => c.FirstName);
                result = await query.Select(
                                        c => new ListContactsView {
                                            ContactId = c.ContactId,
                                            FirstName = c.FirstName,
                                            LastName = c.LastName
                                        })
                                    .ToListAsync();
            }

            return result;
        }

        public ContactsView Find(int id)
        {
            ContactsView? result;

            using(var _context = new ContactsContext())
            {
                var query = from c in _context.Contact
                            select c;

                query = query.Where(c => c.ContactId == id);
                query = query.DefaultIfEmpty();
                result = query?.Select(c => new ContactsView {
                                                ContactId = c.ContactId,
                                                FirstName = c.FirstName,
                                                LastName = c.LastName,
                                                Email = c.Email,
                                                PhoneNumber = c.PhoneNumber,
                                                Notes = c.Notes
                                      })
                               .First();

                if(result == null)
                {
                    result = new ContactsView(false);
                }else result.exists = true;
            }

            return result;
        }
    }
}
