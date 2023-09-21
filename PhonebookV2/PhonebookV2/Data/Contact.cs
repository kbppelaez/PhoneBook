using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhonebookV2.Models;

namespace PhonebookV2.Data
{
    public class Contact
    {
        public Contact() { }

        public Contact(ContactsView cview)
        {
            ContactId = cview.ContactId;
            FirstName = cview.FirstName;
            LastName = cview.LastName;
            Email = cview.Email;
            PhoneNumber = cview.PhoneNumber;
            Notes = cview.Notes;
        }

        public int ContactId { get; set; }

        [Required]
        [StringLength(128)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(128)]
        public string LastName { get; set; }

        [Required]
        [StringLength(512)]
        public string Email { get; set; }

        [StringLength(64)]
        public string PhoneNumber { get; set; }

        public string Notes { get; set; }

        [NotMapped]
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
    }
}
