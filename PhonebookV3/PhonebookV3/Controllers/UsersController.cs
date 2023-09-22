using Microsoft.AspNetCore.Mvc;
using PhonebookV3.Models;
using PhonebookV3.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using PhonebookV3.Core.DataTransferObjects;
using System.Security.Claims;

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
                await PersistLogin(account.User);
                _logger.LogInformation("User {Email} logged in.", account.User.Email);
                account.User.Password = string.Empty;
                return RedirectToAction("Index");
            }

            account.User.Password = string.Empty;
            return View("Login", account);
        }

        public async Task PersistLogin(UserData account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Email)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }


    }
}
