using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcutePediatricsOrientation.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcutePediatricsOrientation.Controllers
{
    public class EditPackageController : Controller
    {
        private readonly AcutePediatricsContext _context;

        public EditPackageController(AcutePediatricsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}