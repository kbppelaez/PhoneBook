using PhonebookV3.Core.DataTransferObjects;
using System.ComponentModel.DataAnnotations;

namespace PhonebookV3.Data
{
    public class Contact
    {
        /* Constructors */
        public Contact() { }

        public Contact(ContactData data)
        {
            this.Id = data.Id;
            this.FirstName = data.FirstName;
            this.LastName = data.LastName;
            this.Email = data.Email;
            this.PhoneNumber = data.PhoneNumber;
            this.Notes = data.Notes;
        }

        /* Properties */
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string FirstName {  get; set; } = string.Empty;

        [StringLength(128)]
        public string LastName { get; set; }

        [StringLength(512)]
        public string Email { get; set; }

        [StringLength(64)]
        public string PhoneNumber { get; set; }

        public string Notes { get; set; }

    }
}
