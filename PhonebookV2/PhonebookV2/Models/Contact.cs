using System.ComponentModel.DataAnnotations;

namespace PhonebookV2.Models
{
    public class Contact
    {
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

        public Contact(ContactsView cview)
        {
            ContactId = cview.ContactId;
            FirstName = cview.FirstName;
            LastName = cview.LastName;
            Email = cview.Email;
            PhoneNumber = cview.PhoneNumber;
            Notes = cview.Notes;
        }

        public Contact() { }
    }
}
