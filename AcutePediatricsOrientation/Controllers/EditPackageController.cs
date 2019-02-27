﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AcutePediatricsOrientation.Enums;
using AcutePediatricsOrientation.Models;
using AcutePediatricsOrientation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AcutePediatricsOrientation.Controllers
{
    [Authorize]
    public class EditPackageController : Controller
    {
        private readonly AcutePediatricsContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EditPackageController(AcutePediatricsContext context, IHostingEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        public IActionResult Index()
        {
            var categories = _context.Category.Select(c => new CategoryViewModel {
                Id = c.Id,
                Name = c.Name,
                Topics = c.Topics.Select(t => new TopicViewModel {
                    Id = t.Id,
                    Name = t.Name,
                    Documents = t.Documents.Select(d => new DocumentsViewModel {
                        Id = d.Id,
                        Name = d.Name
                    })
                })
            });
            return View(new EditPackageViewModel { Categories = categories.ToList()});
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category category)
        {
            if (_context.Category.Any(c => c.Name == category.Name))
            {
                return View();
            }
            else
            {
                _context.Category.Add(new Category { Name = category.Name });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Category.SingleOrDefault(t => t.Id == id);
            if (category == null)
            {
                // TODO 
                return View("Error");
            }
            else
            {
                return View(category);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(Category category)
        {
            if(_context.Category.Any(c => c.Id == category.Id))
            {
                var topicIds = _context.Topic.Where(t => t.CategoryId == category.Id).Select(t => t.Id);

                foreach(var topicId in topicIds)
                {
                    _context.Document.RemoveRange(_context.Document.Where(d => d.TopicId == topicId));
                }

                _context.Topic.RemoveRange(_context.Topic.Where(t => t.CategoryId == category.Id));
                _context.Category.RemoveRange(_context.Category.Where(t => t.Id == category.Id));

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {   // TODO HERE
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _context.Category.SingleOrDefault(t => t.Id == id);
            if (category == null)
            {
                // TODO 
                return View("Error");
            }
            else
            {
                return View(category);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(Category category)
        {
            var oldCategory = _context.Category.SingleOrDefault(t => t.Id == category.Id);
            if (oldCategory == null)
            {
                // TODO 
                return View("Error");
            }
            else
            {
                oldCategory.Name = category.Name;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult CreateTopic(int id)
        {
            return View(new Topic { CategoryId = id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTopic(Topic topic)
        {
            if (ModelState.IsValid)
            {
                _context.Topic.Add(new Topic { CategoryId = topic.CategoryId, Name = topic.Name});
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult EditTopic(int id)
        {
            var topic = _context.Topic.SingleOrDefault(t => t.Id == id);
            if (topic == null)
            {
                // TODO 
                return View("Error");
            }
            else
            {
                return View(topic);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTopic(Topic topic)
        {
            var oldTopic = _context.Topic.SingleOrDefault(t => t.Id == topic.Id);
            if (oldTopic == null)
            {
                // TODO 
                return View("Error");
            }
            else
            {
                oldTopic.Name = topic.Name;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult DeleteTopic(int id)
        {
            var topic = _context.Topic.Include(t => t.Category).SingleOrDefault(t => t.Id == id);
            if (topic == null)
            {
                // TODO 
                return View("Error");
            }
            else
            {
                return View(topic);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTopic(Topic topic)
        {
            var topicToBeRemoved = _context.Topic.Include(t => t.Category).SingleOrDefault(t => t.Id == topic.Id);
            if (topicToBeRemoved == null)
            {
                // TODO 
                return View("Error");
            }
            else
            {
                _context.Topic.Remove(topicToBeRemoved);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult CreateDocument(int id)
        {
            var createDocumentViewModel = new CreateDocumentViewModel {
                TopicId = id,
                DocumentTypes = _context.DocumentType.Select(dt => new SelectListItem {
                    Text = dt.Name,
                    Value = dt.Id.ToString()
                })
            };

            return View(createDocumentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDocument(CreateDocumentViewModel document)
        {
            if(document.DocumentType == (int) ProjectEnum.DocumentType.PDF)
            {
                ModelState.Remove("Url");
            }
            else if(document.DocumentType == (int) ProjectEnum.DocumentType.Video)
            {
                ModelState.Remove("File");
            }

            if (ModelState.IsValid)
            {
                if(document.DocumentType == (int)ProjectEnum.DocumentType.PDF)
                {
                    var fileName = document.File.FileName;
                    var trainingFilesFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "trainingfiles");
                    var filePath = Path.Combine(trainingFilesFolderPath, fileName);
                    if (!_context.Document.Any(d => d.FilePath == filePath))
                    {
                        document.File.CopyTo(new FileStream(filePath, FileMode.Create));
                        _context.Document.Add(new Documents {
                            DocumentTypeId = document.DocumentType,
                            FilePath = Path.Combine("/trainingfiles/", fileName),
                            Name = document.Name,
                            TopicId = document.TopicId
                        });

                        _context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("File", "That file is already used in the package");
                    }
                }
                else if (document.DocumentType == (int)ProjectEnum.DocumentType.Video)
                {
                    _context.Document.Add(new Documents
                    {
                        DocumentTypeId = document.DocumentType,
                        FilePath = document.Url,
                        Name = document.Name,
                        TopicId = document.TopicId
                    });

                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            document.DocumentTypes = _context.DocumentType.Select(dt => new SelectListItem
            {
                Text = dt.Name,
                Value = dt.Id.ToString()
            });
            return View(document);
        }
    }
}