using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCEntityMSSQLSignalR.Data;
using MVCEntityMSSQLSignalR.Helpers;
using MVCEntityMSSQLSignalR.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.Controllers
{
    /// <summary>
    /// Controller for authentication/authorization
    /// </summary>
    public class AccountController : Controller
    {
        private readonly ApplicationContext db;

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="context">Context for working with database</param>
        public AccountController(ApplicationContext context)
        {
            db = context;
        }

        /// <summary>
        /// Get login page method
        /// </summary>
        /// <returns>Login form representation</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Login post method for registered users
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns back the login model</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user != null)
                {
                    string hashedPassword = HashHelper.HashWithSalt(model.Password, user.Salt);

                    if (hashedPassword == user.HashedPassword)
                    {
                        await Authenticate(model.Email);

                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Wrong name or password");
            }
            return View(model);
        }

        /// <summary>
        /// Get registration form method
        /// </summary>
        /// <returns>Registration form representation</returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Registration Post method
        /// </summary>
        /// <param name="model">Registration form model</param>
        /// <returns>Received registration form model</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    (string hashedPassword, string salt) = HashHelper.Hash(model.Password);

                    db.Users.Add(new User
                    {
                        Email = model.Email,
                        HashedPassword = hashedPassword,
                        Salt = salt
                    });

                    await db.SaveChangesAsync();
                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Wrong name or password");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
        }

        /// <summary>
        /// Method for logout from the site
        /// </summary>
        /// <returns>Representation of Login page</returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
