using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhoneBookCore.Pages.Contacts
{
    public class SearchModel : PageModel
    {
        public string SearchString = string.Empty;
        public void OnGet()
        {
            SearchString = "hi";
        }
    }
}
