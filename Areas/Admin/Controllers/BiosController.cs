using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Extentions;
using EducationBackendFinal.Helpers;
using EducationBackendFinal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EducationBackendFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BiosController : Controller
    {

        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        public BiosController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }


        public IActionResult Index()
        {
            Bio bio = _db.Bios.FirstOrDefault();
            return View(bio);
        }


        public IActionResult Update(int?id)
        {
            if (id == null) return NotFound();
            Bio bio =  _db.Bios.FirstOrDefault(p=>p.Id==id);
            if (bio == null) return NotFound();
            return View(bio);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Bio bio)
        {
            if (id == null) return NotFound();
            Bio dbBio = await _db.Bios.FindAsync(id);
            if (dbBio == null) return NotFound();
            if (bio.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }

                if (!bio.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }

                if (bio.Photo.MaxLength(2000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 2mg ola biler");
                    return View();
                }

               
                string path = Path.Combine("img", "logo");
                Helper.DeleteImage(_env.WebRootPath, path, dbBio.Logo);

                string fileName = await bio.Photo.SaveImg(_env.WebRootPath, path);
                dbBio.Logo = fileName;

            }
            dbBio.Number = bio.Number;
            dbBio.Facebook = bio.Facebook;
            dbBio.Vcontact = bio.Vcontact;
            dbBio.Twitter = bio.Twitter;
            dbBio.Pinterest = bio.Pinterest;
           

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
