using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Models;
using EducationBackendFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EducationBackendFinal.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _db;
        public AboutController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            AboutVm aboutVm = new AboutVm
            {
                WelcomeAbout = _db.WelcomeAbouts.FirstOrDefault(),
                Testimonial = _db.Testimonials.FirstOrDefault(),
                NoticeBoards = _db.NoticeBoards.ToList(),
                VideoTour = _db.VideoTours.FirstOrDefault()
              

            };
            return View(aboutVm);
        }
    }
}
