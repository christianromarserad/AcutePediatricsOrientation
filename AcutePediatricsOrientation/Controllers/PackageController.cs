﻿using System;
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
                    Id = t.Id,
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

        public IActionResult SignTopic(int id)
        {
            var currentUsername = User.Claims.Single(c => c.Type == ClaimTypes.Name).Value;
            var topic = _context.Topic.SingleOrDefault(d => d.Id == id);
            var user = _context.Account.SingleOrDefault(a => a.Username == currentUsername);

            if (topic != null && user != null)
            {
                var newSignature = new Signature
                {
                    Date = DateTime.Now,
                    TopicId = topic.Id,
                    UserId = user.Id
                };
                _context.Signature.Add(newSignature);
                _context.SaveChanges();

                return Json(new { Success = true, Username = user.Username, Date = newSignature.Date});
            }
            else
            {
                return Json(new { Success = false });
            }
        }
    }
}