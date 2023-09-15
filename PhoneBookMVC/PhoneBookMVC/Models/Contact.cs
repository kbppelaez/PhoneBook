using System.ComponentModel.DataAnnotations;

namespace PhoneBookMVC.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Email Address")]
        [RegularExpression(@"[_a-zA-Z0-9.]+@([a-zA-Z]{2,}[.])+[a-zA-Z]{2,}")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Notes")]
        public string Notes { get; set; } = string.Empty;

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
