using System.ComponentModel.DataAnnotations;

namespace PhonebookV2.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = String.Empty;

        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Email Address")]
        [RegularExpression(
            @"[_a-zA-Z0-9.]+@([a-zA-Z]{2,}[.])+[a-zA-Z]{2,}",       //alphanumeric and _ . for username, with at least one subdomain and at least one TLD
            ErrorMessage = "Please input a valid email address.")]  //      at least 2 letters per domain
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        [Display(Name = "Name")]
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
