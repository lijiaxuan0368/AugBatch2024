using ApplicationCore.Models;
using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        // code: Loose coupled / decoupling -> constructor injection -> startup.cs ConfigureService register
        // join : inner / outer -> left, right, full; cross join ; self join 
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet] // Display 
        // ctrl + shift + f9
        // localhost:5000/account/register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]  // Create User in DB
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            // save the info to database by calling user service register method
            var user = await _userService.RegisterUser(model);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel model)
        {
            var user = await _userService.ValidateUser(model.Email, model.Password);

            // Cookie based Authentication

            // Create the claims that we are going to store in cookie
            // System.Security.Claims;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()!)
            };

            // Create identity object that will use above claims
            // Microsoft.AspNetCore.Authentication.Cookies;
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Create the cookie
            // HttpContext
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity));
            // Expiration Time -> Startup.cs

            // redirect to home page
            return LocalRedirect("~/");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
