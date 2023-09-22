using Microsoft.AspNetCore.Mvc;
using PhonebookV3.Models;
using PhonebookV3.Core;

namespace PhonebookV3.Controllers
{
    public class UsersController : Controller
    {
        public readonly IUsersService _usersServices;
        public readonly ILogger<UsersController> _logger;

        public UsersController(IUsersService users, ILogger<UsersController> logger)
        {
            _usersServices = users;
            _logger = logger;
        }

        [Route("/users/login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserViewModel());
        }

        [Route("/users/login")]
        [HttpPost]
        public async Task<IActionResult> VerifyLogin([FromForm] UserViewModel account)
        {
            await account.Verify();
            if(account.success)
            {
                return RedirectToAction("Index");
            }
            return View("Login", account);
        }


    }
}
