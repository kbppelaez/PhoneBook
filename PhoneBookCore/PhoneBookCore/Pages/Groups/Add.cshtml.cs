using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhoneBookCore.Pages.Groups
{
    public class AddModel : PageModel
    {
        public String SearchString = string.Empty;
        public void OnGet()
        {
        }
    }

    public class  GroupInfo
    {
        public string GId = String.Empty;
        public string Name = string.Empty;
        public string? Description;
    }
}
