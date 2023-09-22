using System.ComponentModel.DataAnnotations;

namespace PhonebookV3.Data
{
    public class User
    {
        /* Constructors */
        public User() { }

        /* Properties */
        public int Id { get; set; }

        [Required]
        [StringLength(512)]
        public string Email { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }
    }
}
