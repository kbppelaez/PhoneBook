using Microsoft.EntityFrameworkCore;
using PhonebookV3.Core.DataTransferObjects;
using PhonebookV3.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PhonebookV3.Core.Application
{
    public class ContactsService : IContactsService
    {
        private readonly PhonebookDbContext _db;

        public ContactsService(PhonebookDbContext db)
        {
            _db = db;
        }

        public async Task<int> CountAll(ContactSearchQueryData queryData)
        {
            IQueryable<Contact> query = _db.Contact;

            if(!string.IsNullOrEmpty(queryData.Term))
                query = SearchTerm(query, queryData.Term);

            return await query.CountAsync();
        }
        public async Task<ContactListData[]> GetAll(ContactSearchQueryData queryData)
        {
            IQueryable<Contact> query = _db.Contact;

            if (!string.IsNullOrEmpty(queryData.Term))
                query = SearchTerm(query, queryData.Term);

            query = query.OrderBy(c => c.FirstName);
            query = SkipContacts(query, queryData.Page, queryData.PageSize);

            return await query
                .Select(
                    c => new ContactListData { 
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName
                    })
                .ToArrayAsync();
        }

        public async Task<string> InsertContact(ContactData newcontact)
        {
            try
            {
                _db.Add(new Contact(newcontact));
                if (await _db.SaveChangesAsync() == 1) return "OK";
                else return "Database error.";
            }catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteContact(int id)
        {
            try
            {
                var contact = await _db.Contact.FindAsync(id);
                if (contact != null)
                    _db.Contact.Remove(contact);

                if (await _db.SaveChangesAsync() == 1) return "OK";
                else return "Database error.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<ContactData> Search(int id)
        {
            try
            {
                IQueryable<Contact> query = _db.Contact;
                query = query.Where(c => c.Id == id);
                query = query.DefaultIfEmpty();
                
                return await query
                    .Select(c => new ContactData
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber,
                        Notes = c.Notes,
                    }).FirstAsync();
            }catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> UpdateContact(ContactData contact)
        {
            try
            {
                _db.Update(new Contact(contact));
                if (await _db.SaveChangesAsync() == 1)
                    return "OK";
                else return "Database Error.";
            }catch (Exception ex) {
                return ex.Message;
            }
        }

        // HELPER FUNCTIONS
        public IQueryable<Contact> SearchTerm(IQueryable<Contact> query, string term)
        {
            term = term.Trim().ToLower();
            query = query.Where(
                c => c.FirstName.ToLower().StartsWith(term)
                    || (c.LastName != null && c.LastName.ToLower().StartsWith(term))
                    || (c.Email != null && c.Email.ToLower().StartsWith(term))
                    || (c.PhoneNumber != null && c.PhoneNumber.StartsWith(term))
                    || (c.Notes != null && c.Notes.ToLower().StartsWith(term))
            );
            return query;
        }

        public IQueryable<Contact> SkipContacts(IQueryable<Contact> query, int currentIndex, int pageSize)
        {
            return query.Skip(currentIndex * pageSize)
                         .Take(pageSize);
        }
    }
}
