using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EducationBackendFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVm homeVm = new HomeVm
            {
                Sliders = _db.Sliders.ToList(),
                NoticeBoards = _db.NoticeBoards.ToList(),
                NoticeRightInfos = _db.NoticeRightInfos.ToList(),
                WhyUs = _db.WhyUs.FirstOrDefault(),
                UpComingEvents = _db.UpComingEvents.ToList(),
                Testimonial=_db.Testimonials.FirstOrDefault()
                
            };
            var controller = Request.Path.Value;
            return View(homeVm);
        }
    }
}
