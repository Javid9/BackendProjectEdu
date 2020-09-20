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
    public class HomeBioController : Controller
    {

        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        public HomeBioController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }


        public IActionResult Index()
        {
            HomeBio homeBio = _db.HomeBios.FirstOrDefault();
            return View(homeBio);
        }


        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            HomeBio homeBio = _db.HomeBios.FirstOrDefault(p => p.Id == id);
            if (homeBio == null) return NotFound();
            return View(homeBio);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, HomeBio homeBio)
        {
            if (id == null) return NotFound();
            HomeBio dbHomeBio = await _db.HomeBios.FindAsync(id);
            if (dbHomeBio == null) return NotFound();
            if (homeBio.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }

                if (!homeBio.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }

                if (homeBio.Photo.MaxLength(2000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 2mg ola biler");
                    return View();
                }
                string path = Path.Combine("img", "logo");
                Helper.DeleteImage(_env.WebRootPath, path, dbHomeBio.Logo);

                string fileName = await homeBio.Photo.SaveImg(_env.WebRootPath, path);
                dbHomeBio.Logo = fileName;
            }
            dbHomeBio.Number = homeBio.Number;
            dbHomeBio.Facebook = homeBio.Facebook;
            dbHomeBio.Vcontact = homeBio.Vcontact;
            dbHomeBio.Twitter = homeBio.Twitter;
            dbHomeBio.Pinterest = homeBio.Pinterest;


            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
