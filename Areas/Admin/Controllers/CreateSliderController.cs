using System;
using System.Collections.Generic;
using System.IO;
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

namespace EducationBackendFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CreateSliderController : Controller
    {

        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        public CreateSliderController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Sliders.ToList());
        }
        
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _db.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        public IActionResult Create()
        {
            if (_db.Sliders.Count() >= 5)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create(Slider slider)
        {
           
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }

            if (slider.Photo.MaxLength(2000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                return View();
            }

            if (_db.Sliders.Count() >= 5)
            {
                return RedirectToAction(nameof(Index));
            }
            string path = Path.Combine("img", "slider");
            string fileName = await slider.Photo.SaveImg(_env.WebRootPath, path);
            
            Slider newslider = new Slider();
            newslider.Description= slider.Description;          
            newslider.Title = slider.Title;
            newslider.Image = fileName;
            
            await _db.Sliders.AddAsync(newslider);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _db.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _db.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            string path = Path.Combine("img", "slider");
            Helper.DeleteImage(_env.WebRootPath, path, slider.Image);

            _db.Sliders.Remove(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _db.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, CreateSliderVM createSliderVM)
        {
            if (id == null) return NotFound();
            Slider dbSlider = await _db.Sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();
            if (createSliderVM.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }

                if (!createSliderVM.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }

                if (createSliderVM.Photo.MaxLength(200))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                    return View();
                }


                string path = Path.Combine("img", "slider");
                Helper.DeleteImage(_env.WebRootPath, path, dbSlider.Image);
             

                string fileName = await createSliderVM.Photo.SaveImg(_env.WebRootPath, path);
                dbSlider.Image = fileName;

            }
            dbSlider.Description = createSliderVM.Description;
            dbSlider.Title = createSliderVM.Title;
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }



}
