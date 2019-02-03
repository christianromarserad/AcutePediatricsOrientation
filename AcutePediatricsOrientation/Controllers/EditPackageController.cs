using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcutePediatricsOrientation.Models;
using AcutePediatricsOrientation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcutePediatricsOrientation.Controllers
{
    [Authorize]
    public class EditPackageController : Controller
    {
        private readonly AcutePediatricsContext _context;

        public EditPackageController(AcutePediatricsContext context)
        {
            _context = context;
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

        public IActionResult DeleteCategory(int id)
        {
            if(_context.Category.Any(c => c.Id == id))
            {
                var topicIds = _context.Topic.Where(t => t.CategoryId == id).Select(t => t.Id);

                foreach(var topicId in topicIds)
                {
                    _context.Document.RemoveRange(_context.Document.Where(d => d.TopicId == topicId));
                }

                _context.Topic.RemoveRange(_context.Topic.Where(t => t.CategoryId == id));
                _context.Category.RemoveRange(_context.Category.Where(t => t.Id == id));

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {   // TODO HERE
                return View();
            }
        }

        [HttpGet]
        public IActionResult CreateTopic(int id)
        {
            return View(new Topic { CategoryId = id});
        }

        [HttpPost]
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
    }
}