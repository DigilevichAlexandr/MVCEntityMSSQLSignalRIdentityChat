using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCEntityMSSQLSignalR.BLL.DTO;
using MVCEntityMSSQLSignalR.BLL.Services;
using MVCEntityMSSQLSignalR.DAL.Entities;
using System;
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
        private readonly IAccountService _accountService;

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="context">Context for working with database</param>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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
                if (await _accountService.Login(model).ConfigureAwait(false))
                {
                    await Authenticate(model.Email).ConfigureAwait(false);

                    return RedirectToAction("Index", "Home");
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
                if (await _accountService.Register(model).ConfigureAwait(false))
                {
                    await Authenticate(model.Email).ConfigureAwait(false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong name or password");
                }
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
                new ClaimsPrincipal(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// Method for logout from the site
        /// </summary>
        /// <returns>Representation of Login page</returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme)
                .ConfigureAwait(false);

            return RedirectToAction("Login", "Account");
        }
    }
}
