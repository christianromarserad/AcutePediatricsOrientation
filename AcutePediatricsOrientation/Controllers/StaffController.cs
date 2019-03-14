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
    public class StaffController : Controller
    {
        private readonly AcutePediatricsContext _context;

        public StaffController(AcutePediatricsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var staffListViewModel = new StaffListViewModel();
            var stafSignaturefList = _context.Signature.ToList();
            var totalTopics = (double)_context.Topic.Count();
            staffListViewModel.Users = _context.Account.Select(a => 
                new StaffViewModel
                {
                    UserId = a.Id,
                    UserName = a.Username,
                    Progress = (((double)stafSignaturefList.Where(sl => sl.UserId == a.Id).Count() / totalTopics) * 100.0)
                }
            ).ToList();

            return View(staffListViewModel);
        }
    }
}