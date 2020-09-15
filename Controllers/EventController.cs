using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Models;
using Microsoft.AspNetCore.Mvc;

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
            List<UpComingEvent> events = _db.UpComingEvents.ToList();

            return View(events);
        }
        public IActionResult Detail(int? id)
        {
            UpComingEvent upComingEvent = _db.UpComingEvents.Find(id);
            return View(upComingEvent);
        }
    }
}
