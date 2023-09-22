using Microsoft.AspNetCore.Mvc;

namespace PhonebookV3.Controllers
{
    public class UsersController : Controller
    {
        [Route("/users/login", Name = "login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


    }
}
