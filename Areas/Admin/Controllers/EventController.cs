using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Extentions;
using EducationBackendFinal.Models;
using EducationBackendFinal.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationBackendFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventController : Controller
    {

        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        public EventController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            
           
            return View(_db.UpComingEvents.Include(e=>e.SpeakerEvents).ThenInclude(c=>c.Speaker).ToList());
            
           

            //List<UpComingEvent> events = _db.UpComingEvents.Include(e => e.SpeakerEvents);


            
        }
        public IActionResult Create()
        {
            ViewBag.Speaker = _db.Speakers.ToList();
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Create([FromForm] UpComingEventCreateVM upComingEventCreateVM)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!upComingEventCreateVM.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return BadRequest(ModelState);
            }

            if (upComingEventCreateVM.Photo.MaxLength(2000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                return BadRequest(ModelState);
            }
            string path = Path.Combine("img", "event");
            UpComingEvent upComingEvent = new UpComingEvent
            {
                Title = upComingEventCreateVM.Title,
                Image = await upComingEventCreateVM.Photo.SaveImg(_env.WebRootPath, path),
                Month = upComingEventCreateVM.Month,
                Day = upComingEventCreateVM.Day,
                Location = upComingEventCreateVM.Location,
                StartTime = upComingEventCreateVM.StartTime,
                EndTime = upComingEventCreateVM.EndTime,
                Description = upComingEventCreateVM.Description
            };

           
           
            await _db.UpComingEvents.AddAsync(upComingEvent);
            await _db.SaveChangesAsync();
            foreach (var speakerId in upComingEventCreateVM.SpeakerEventsId)
            {
                var speaker = _db.Speakers.Include(p=>p.SpeakerEvents).ThenInclude(p=>p.UpComingEvent).FirstOrDefault(p=>p.Id==speakerId);
                foreach (var se in speaker.SpeakerEvents)
                {
                    if (upComingEvent.StartTime>se.UpComingEvent.StartTime&& upComingEvent.EndTime < se.UpComingEvent.EndTime)
                    {
                        ModelState.AddModelError("", "Busy");
                        return BadRequest(ModelState);

                    }

                }
                SpeakerEvent speakerEvent = new SpeakerEvent
                {
                    SpeakerId=speakerId,
                    UpComingEventId=upComingEvent.Id

                };
                _db.SpeakerEvents.Add(speakerEvent);
                await _db.SaveChangesAsync();
            }

            return Ok($"{upComingEvent.Id} li element yaradildi");


        }
    }
}
