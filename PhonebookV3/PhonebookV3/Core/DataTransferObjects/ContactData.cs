using System.ComponentModel.DataAnnotations;

namespace PhonebookV3.Core.DataTransferObjects
{
    public class ContactData
    {
        public ContactData() { }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(128)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(512)]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [StringLength(64)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [StringLength(512)]
        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Name")]
        public string Name {
            get { 
                if (string.IsNullOrEmpty(LastName))
                {
                    return FirstName;
                }else return FirstName + " " + LastName;
            }
        }

    }
}
