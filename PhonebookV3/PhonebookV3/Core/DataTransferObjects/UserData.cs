using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PhonebookV3.Core.DataTransferObjects
{
    public class UserData
    {
        public int Id { get; set; }

        [Required]
        [StringLength(512)]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 8, ErrorMessage = "Password must have at least 8 characters.")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
