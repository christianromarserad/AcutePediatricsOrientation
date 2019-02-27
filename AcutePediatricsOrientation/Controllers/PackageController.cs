using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcutePediatricsOrientation.Models;
using AcutePediatricsOrientation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AcutePediatricsOrientation.Controllers
{
    public class PackageController : Controller
    {
        private readonly AcutePediatricsContext _context;

        public PackageController(AcutePediatricsContext context)
        {
            _context = context;
        }
        public IActionResult ViewDocument(int id)
        {
            var document = _context.Document.SingleOrDefault(d => d.Id == id);

            if(document != null)
            {
                var documentViewModel = new DocumentsViewModel {
                    Name = document.Name,
                    Path = document.FilePath
                };
                return View(documentViewModel);
            }
            else
            {
                return View("Error");
            }
        }
    }
}