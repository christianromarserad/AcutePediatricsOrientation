using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcutePediatricsOrientation.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using AcutePediatricsOrientation.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AcutePediatricsOrientation.Controllers
{
    public class AccountController : Controller
    {
        private readonly AcutePediatricsContext _context;

        public AccountController(AcutePediatricsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Account account)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Account.SingleOrDefault(a => a.Username == account.Username && a.Password == account.Password);

                if(user == null)
                {
                    ModelState.AddModelError("", "username or password is invalid");
                    return View();
                }
                else
                {
                    // Create the identity from the user info
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.Name, account.Username));
                    if(user.RoleId == (int) ProjectEnum.Role.Educator)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Educator"));
                    }

                    // Authenticate using the identity
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = false });

                    return RedirectToAction("Index", "Package");
                }
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Register(Account account)
        { 
            return View();
        }
    }
}
