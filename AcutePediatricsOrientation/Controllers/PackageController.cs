using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public IActionResult Index()
        {
            var currentUsername = User.Claims.Single(c => c.Type == ClaimTypes.Name).Value;

            var categories = _context.Category.Select(c => new CategoryViewModel
            {
                Name = c.Name,
                Topics = c.Topics.Select(t => new TopicViewModel
                {
                    Name = t.Name,
                    Signature = t.Signatures.Where(s => s.User.Username == currentUsername)
                                            .Select(s => new SignatureViewModel() {
                                                Username = s.User.Username,
                                                Date = s.Date
                                            }).SingleOrDefault(),
                    Documents = t.Documents.Select(d => new DocumentsViewModel
                    {
                        Id = d.Id,
                        Name = d.Name
                    })
                })
            });
            return View(new PackageViewModel { Categories = categories.ToList() });
        }

        public IActionResult ViewDocument(int id)
        {
            var document = _context.Document.SingleOrDefault(d => d.Id == id);

            if(document != null)
            {
                var documentViewModel = new DocumentsViewModel {
                    DocumentTypeId = document.DocumentTypeId,
                    Name = document.Name,
                    Path = document.Path
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