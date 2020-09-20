using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Extentions;
using EducationBackendFinal.Helpers;
using EducationBackendFinal.Models;
using EducationBackendFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EducationBackendFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SpeakerController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;

        public SpeakerController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;

        }
        public IActionResult Index()
        {
            return View(_db.Speakers.Where(t => t.IsDeleted == false).ToList());
        }



        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpeakerCreateVM speakerCreateVM)
        {

           
            if (!ModelState.IsValid) return View();
            if (!speakerCreateVM.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }

            if (speakerCreateVM.Photo.MaxLength(2000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                return View();
            }

            Speaker newSpeaker = new Speaker
            {
                Name = speakerCreateVM.Name,
                Position = speakerCreateVM.Position,
                Image = await speakerCreateVM.Photo.SaveImg(_env.WebRootPath, "img/teacher"),
            };
            await _db.AddAsync(newSpeaker);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        public IActionResult Detail(int? id)
        {
            if (id == null) return View();
            Speaker dbspeaker = _db.Speakers.FirstOrDefault(p => p.Id == id);

            return View(dbspeaker);
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return View();
            Speaker speaker = _db.Speakers.FirstOrDefault(p => p.Id == id);
            if (speaker == null) return View();
            return View(speaker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SpeakerEditVM speakerEditVM)
        {

            if (!ModelState.IsValid) return View(ModelState);
            Speaker dbSpeaker = await _db.Speakers.FirstOrDefaultAsync(x => x.Id == speakerEditVM.Id);
            if (dbSpeaker == null) return NotFound();
            dbSpeaker.Name = speakerEditVM.Name;
            dbSpeaker.Position = speakerEditVM.Position;
            if (speakerEditVM.Photo != null)
            {
                Helper.DeleteImage(_env.WebRootPath, "img/teacher", dbSpeaker.Image);
                dbSpeaker.Image = await speakerEditVM.Photo.SaveImg(_env.WebRootPath, "img/teacher");

            }

            await _db.SaveChangesAsync();
            await Task.Delay(1000);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Speaker dbSpeaker = _db.Speakers.FirstOrDefault(s => s.Id == id);
            if (dbSpeaker == null) return NotFound();

            return View(dbSpeaker);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSpeaker(int? id)
        {
            if (id == null) return NotFound();
            Speaker dbSpeaker = _db.Speakers.FirstOrDefault(s => s.Id == id);
            if (dbSpeaker == null) return NotFound();
            dbSpeaker.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
