using PhonebookV3.Core.DataTransferObjects;
using System.ComponentModel.DataAnnotations;

namespace PhonebookV3.Data
{
    public class User
    {
        /* Constructors */
        public User() { }

        public User(UserData userData) { 
            this.Id = userData.Id;
            this.Email = userData.Email;
        }

        /* Properties */
        public int Id { get; set; }

        [Required]
        [StringLength(512)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
