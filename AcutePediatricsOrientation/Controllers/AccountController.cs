using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcutePediatricsOrientation.Models;

namespace AcutePediatricsOrientation.Controllers
{
    public class AccountController : Controller
    {
        private readonly AcutePediatricsContext _context;

        public AccountController(AcutePediatricsContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
