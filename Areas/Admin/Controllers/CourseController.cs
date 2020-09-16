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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationBackendFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {

        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        public CourseController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {

            return View(_db.Courses.Where(c=>c.IsDeleted==false).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM courseCreateVM)
        {
            if (!ModelState.IsValid) return NotFound();

            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!courseCreateVM.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }

            if (courseCreateVM.Photo.MaxLength(2000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                return View();
            }

            
            string path = Path.Combine("img", "course");
            string fileName = await courseCreateVM.Photo.SaveImg(_env.WebRootPath, path);

            Course newcourse = new Course
            {
                Title = courseCreateVM.Title,
                Image = fileName,
                Description = courseCreateVM.Description,
                StartTime = courseCreateVM.StartTime,
                Duration = courseCreateVM.Duration,
                ClassDuration = courseCreateVM.ClassDuration,
                SkilLevel = courseCreateVM.SkilLevel,
                Language = courseCreateVM.Language,
                StudentsCount = courseCreateVM.StudentsCount,
                Assesments = courseCreateVM.Assesments,
                
            };

            await _db.Courses.AddAsync(newcourse);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Course course = await _db.Courses.FindAsync(id);

            return View(course);
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Course course = await _db.Courses.FindAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CourseEditVM courseEditVM)
        {
            if (!ModelState.IsValid) return View();
            Course dbCourse = _db.Courses.Where(c=>c.IsDeleted==false).FirstOrDefault(c => c.Id == courseEditVM.Id);
            if (courseEditVM.Photo != null)
            {
                Helper.DeleteImage(_env.WebRootPath, "img/course", dbCourse.Image);
                dbCourse.Image = await courseEditVM.Photo.SaveImg(_env.WebRootPath, "img/course");

            }

            dbCourse.Language = courseEditVM.Language;
            dbCourse.SkilLevel = courseEditVM.SkilLevel;
            dbCourse.StartTime = courseEditVM.StartTime;
            dbCourse.StudentsCount = courseEditVM.StudentsCount;
            dbCourse.Title = courseEditVM.Title;
            dbCourse.Assesments = courseEditVM.Assesments;
            dbCourse.ClassDuration = courseEditVM.ClassDuration;
            dbCourse.CourseUsers = courseEditVM.CourseUsers;
            dbCourse.Description = courseEditVM.Description;
            dbCourse.Duration = courseEditVM.Duration;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Course course = await _db.Courses.FindAsync(id);

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCourse(int? id)
        {
            if (id == null) return NotFound();
            Course course = _db.Courses.Where(c=>c.IsDeleted==false).FirstOrDefault(c=>c.Id==id);
            course.IsDeleted = true;
            await _db.SaveChangesAsync();
            await Task.Delay(1000);

            return RedirectToAction(nameof(Index));
        }


    }
}
