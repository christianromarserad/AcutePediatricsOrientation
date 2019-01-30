using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcutePediatricsOrientation.Models;
using AcutePediatricsOrientation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var categories = _context.Category.Select(c => new EditPackageViewModelCategory {
                Id = c.Id,
                Name = c.Name,
                Topics = c.Topics.Select(t => new EditPackageViewModelTopic {
                    Id = t.Id,
                    Name = t.Name,
                    Documents = t.Documents.Select(d => new EditPackageViewModelDocuments {
                        Name = d.Name
                    }).ToList()
                }).ToList()
            });
            return View(new EditPackageViewModel { Categories = categories.ToList()});
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(string name)
        {
            if (ModelState.IsValid)
            {
                _context.Category.Add(new Category { Name = name });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}