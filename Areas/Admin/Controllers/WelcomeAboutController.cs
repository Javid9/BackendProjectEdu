using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Extentions;
using EducationBackendFinal.Helpers;
using EducationBackendFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EducationBackendFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WelcomeAboutController : Controller
    {

        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        public WelcomeAboutController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            WelcomeAbout welcomeAbout = _db.WelcomeAbouts.FirstOrDefault();
            return View(welcomeAbout);
        }



        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            WelcomeAbout welcomeAbout = await _db.WelcomeAbouts.FindAsync(id);
            if (welcomeAbout == null) return NotFound();
            return View(welcomeAbout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, WelcomeAbout welcomeAbout)
        {
            if (id == null) return NotFound();
            WelcomeAbout dbwelcomeAbout = await _db.WelcomeAbouts.FindAsync(id);
            if (dbwelcomeAbout == null) return NotFound();
            if (welcomeAbout.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }

                if (!welcomeAbout.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }

                if (welcomeAbout.Photo.MaxLength(200))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                    return View();
                }

                string path = Path.Combine("img", "about");
                Helper.DeleteImage(_env.WebRootPath, path, dbwelcomeAbout.Image);
                string fileName = await welcomeAbout.Photo.SaveImg(_env.WebRootPath, path);
                dbwelcomeAbout.Image = fileName;

            }
            dbwelcomeAbout.Description = welcomeAbout.Description;
            dbwelcomeAbout.Title = welcomeAbout.Title;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
