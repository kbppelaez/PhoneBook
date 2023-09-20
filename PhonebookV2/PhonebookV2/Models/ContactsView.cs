using System.ComponentModel.DataAnnotations;

namespace PhonebookV2.Models
{
    public class ContactsView
    {
        public bool? exists; //if created instance has valid data
        public bool? editSuccess; //if saving edits is successful
        public bool? addSuccess; //if creating new contact is successful
        public string errorMsg = string.Empty;
        public int ContactId { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = String.Empty;
        
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        
        [Display(Name = "Email Address")]
        [EmailAddress]
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

    public class SearchListView
    {
        public string? term;
        public IEnumerable<ListContactsView>? Contacts;
    }
}
