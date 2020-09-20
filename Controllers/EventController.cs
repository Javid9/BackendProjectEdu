using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Models;
using EducationBackendFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationBackendFinal.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _db;
        public EventController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<UpComingEvent> events = _db.UpComingEvents.Take(8).OrderByDescending(p=>p.Id).ToList();

            return View(events);
        }
        public IActionResult Detail(int? id)
        {




            UpComingEvent UpComingEvent = _db.UpComingEvents.Include(c => c.SpeakerEvents).ThenInclude(p => p.Speaker).FirstOrDefault(p=>p.Id==id);
               

            
            return View(UpComingEvent);
        }
    }
}
