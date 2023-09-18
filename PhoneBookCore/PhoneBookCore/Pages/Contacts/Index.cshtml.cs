using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhoneBookCore.Pages.Contacts
{
    public class IndexModel : PageModel
    {
        List<ContactInfo> contacts = new List<ContactInfo>();

        public void OnGet()
        {

        }
    }

    public class ContactInfo
    {
        public string Id;
        public string FirstName;
        public string LastName;
        public string EmailAdd;
        public string PhoneNumber;
        public string Notes;

    }
}
