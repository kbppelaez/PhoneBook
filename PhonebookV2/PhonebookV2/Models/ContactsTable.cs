using PhonebookV2.Models;
using Microsoft.EntityFrameworkCore;
using PhonebookV2.Data;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PhonebookV2.Controllers;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

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

        public async Task<ContactsView> Find(int id)
        {
            ContactsView? result;

            try
            {
                using (var _context = new ContactsContext())
                {
                    var query = from c in _context.Contact
                                select c;

                    query = query.Where(c => c.ContactId == id);
                    query = query.DefaultIfEmpty();
                    result = await query.Select(c => new ContactsView
                                    {
                                        ContactId = c.ContactId,
                                        FirstName = c.FirstName,
                                        LastName = c.LastName,
                                        Email = c.Email,
                                        PhoneNumber = c.PhoneNumber,
                                        Notes = c.Notes
                                    })
                                   .FirstAsync();

                    if (result == null)
                    {
                        result = new ContactsView(false);
                    }
                    else result.exists = true;
                }
            }catch (Exception ex)
            {
                result = new ContactsView(false);
                Console.Write(ex.Message);
            }
            return result;
        }

        public async Task<ContactsView> SaveChanges(ContactsView contact)
        {
            try
            {
                using(var _context = new ContactsContext())
                {
                    _context.Update(new Contact(contact));
                    await _context.SaveChangesAsync();
                    contact.editSuccess = true;
                }
            }
            catch (Exception ex)
            {
                contact = await Find(contact.ContactId);
                contact.editSuccess = false;
                contact.errorMsg = ex.Message;
            }

            return contact;
        }

        public async Task<bool> DeleteContact(int ContactId)
        {
            bool result = false;

            using(var _context = new ContactsContext())
            {
                if(_context.Contact != null)
                {
                    var contact = await _context.Contact.FindAsync(ContactId);
                    if(contact != null)
                    {
                        _context.Contact.Remove(contact);
                    }
                    int affected = await _context.SaveChangesAsync();
                    if (affected == 1) result = true;
                }
            }

            return result;
        }

        public async Task<bool> CreateContact(ContactsView contactview)
        {
            bool result = false;
            using(var _context = new ContactsContext()){
                if(_context.Contact != null)
                {
                    Contact contact = new Contact(contactview);
                    if(contact != null)
                    {
                        _context.Contact.Add(contact);
                    }
                    int affected = await _context.SaveChangesAsync();
                    if(affected == 1) result = true;
                }
            }
            return result;
        }

        public async Task<List<ListContactsView>> SearchContacts(string term)
        {
            List<ListContactsView> result = new List<ListContactsView>();

            using(var _context = new ContactsContext())
            {
                if(_context.Contact != null)
                {
                    var query = from c in _context.Contact
                                select c;

                    query = query.Where(c => c.FirstName.ToLower().StartsWith(term.ToLower())
                                        || (c.LastName != null && c.LastName.ToLower().StartsWith(term.ToLower()))
                                        || (c.Email != null && c.Email.ToLower().StartsWith(term.ToLower()))
                                        || (c.PhoneNumber != null && c.PhoneNumber.ToLower().StartsWith(term.ToLower()))
                                        || (c.Notes != null && c.Notes.ToLower().StartsWith(term.ToLower()))
                                        );
                    query = query.OrderBy(c => c.FirstName);
                    
                    result = await query.Select(
                                            c => new ListContactsView
                                            {
                                                ContactId = c.ContactId,
                                                FirstName = c.FirstName,
                                                LastName = c.LastName
                                            })
                                        .ToListAsync();
                }
            }

            return result;
        }
    }
}
