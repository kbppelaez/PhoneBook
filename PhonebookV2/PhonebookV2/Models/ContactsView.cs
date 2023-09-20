using System.ComponentModel.DataAnnotations;

namespace PhonebookV2.Models
{
    public class ContactsView
    {
        public bool exists;
        public int ContactId { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Notes { get; set; }
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(LastName))
                {
                    return FirstName;
                }
                else return FirstName + " " + LastName;
            }
        }

        public ContactsView() { 
        }

        public ContactsView(bool exists)
        {
            this.exists = exists;
        }

        public ContactsView(Contact c)
        {
            if (c == null)
            {
                this.exists = false;
            }
            else
            {
                this.exists = true;
                this.ContactId = c.ContactId;
                this.FirstName = c.FirstName;
                this.LastName = c.LastName;
                this.Email = c.Email;
                this.PhoneNumber = c.PhoneNumber;
                this.Notes = c.Notes;
            }
        }
    }

    public class ListContactsView
    {
        public int ContactId;
        public string FirstName = string.Empty;
        public string? LastName;
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(LastName))
                {
                    return FirstName;
                }
                else
                {
                    return FirstName + " " + LastName;
                }
            }
        }
    }
}
