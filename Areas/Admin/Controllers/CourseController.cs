using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Extentions;
using EducationBackendFinal.Helpers;
using EducationBackendFinal.Models;
using EducationBackendFinal.Services;
using EducationBackendFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationBackendFinal.Areas.Admin.Controllers
{
   
    [Area("Admin")]
    [Authorize(Roles = "Admin,CourseManager")]
    public class CourseController : Controller
    {

        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<AppUser> _usermanager;
        public CourseController(AppDbContext db, IHostingEnvironment env, UserManager<AppUser> usermanager)
        {
            _db = db;
            _env = env;
            _usermanager = usermanager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View(_db.Courses.Include(x => x.CourseCategories).ThenInclude(c=>c.Category));
            }
            //int count = _db.Categories.Where(c=?)
            if (User.IsInRole("CourseManager"))
            {
                var user = await _db.Users.Include(x => x.CourseUsers).ThenInclude(x => x.Course).ThenInclude(x=> x.CourseCategories)
                    .ThenInclude(c=>c.Category).SingleOrDefaultAsync(x => x.UserName == User.Identity.Name);
                if(user.CourseUsers.Count > 0)
                {
                    //return View(_db.Courses.Where(c => c.IsDeleted == false ).Include(p => p.CourseCategories).ThenInclude(c => c.Category).ToList());
                    var courseUser = user.CourseUsers;
                    List<Course> courses = new List<Course>();
                    foreach (var item in courseUser)
                    {
                        courses.Add(item.Course);
                    } 
                    return View(courses);
                }
               
            }

         

            return NotFound();
           
        }
        [Authorize(Roles ="Admin")]
        public async Task< IActionResult >Create()
        {
           
            var users = await _usermanager.GetUsersInRoleAsync("CourseManager");
            ViewBag.Roles = users;
            ViewBag.Categories = _db.Categories.Where(c=>c.IsDeleted==false).ToList();
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM courseCreateVM,List<int> List,List<string> Userlist)
        {

            var users =await _usermanager.GetUsersInRoleAsync("CourseManager");
            ViewBag.Roles = users;

            ViewBag.Categories = _db.Categories.ToList();
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

            if (List.Count() == 0)
            {
                TempData["Error"]="Pls choose categoryasdfggd";
                return View();
            }

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
                CourseFee=courseCreateVM.CourseFee,
                AboutCourse=courseCreateVM.AboutCourse,
                HowToApply=courseCreateVM.HowToApply,
                Certification=courseCreateVM.Certification
            };
            List<CourseUser> courseUsers = new List<CourseUser>();
            foreach (var item in Userlist)
            {
                CourseUser course = new CourseUser
                {
                    AppUserId = item,
                    CourseId = newcourse.Id
                };
                courseUsers.Add(course);

            }
            newcourse.CourseUsers = courseUsers;

            List<CourseCategory> courseCategories = new List<CourseCategory>();
            foreach (var item in List)
            {
                CourseCategory courseCategory = new CourseCategory
                {
                    CourseId = newcourse.Id,
                    CategoryId = item
                };
                courseCategories.Add(courseCategory);
            }
            newcourse.CourseCategories = courseCategories;

            await _db.Courses.AddAsync(newcourse);
            _db.SaveChanges();
            var callbackUrl = Url.Action(
                    "Detail",
                    "Course",
                    new { Id = courseCreateVM.Id },
                    protocol: HttpContext.Request.Scheme);
            EmailService email = new EmailService();
            List<string> e = _db.Subscriptions.Select(x => x.Email).ToList();
            await email.SendEmailAsync(e, "Yeni course",
                   "Yeni Course: <a href=https://localhost:44375/Courses/Detail/" + $"{newcourse.Id}" + ">link</a>");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Course course = await _db.Courses.Include(p => p.CourseCategories).ThenInclude(c => c.Category).FirstOrDefaultAsync(p => p.Id == id);
            return View(course);
        }


        public async Task< IActionResult> Update(int? id)
        {
            var currentusers = await _usermanager.GetUsersInRoleAsync("CourseManager");
           
            ViewBag.allusers = currentusers;
            
            //ViewBag.Roles = users;
            ViewBag.Categories = _db.Categories.Where(c=>!c.IsDeleted).ToList();
            if (id == null) return NotFound();
            Course course =  _db.Courses.Include(c=>c.CourseCategories).Include(c=>c.CourseUsers).FirstOrDefault(p=>p.Id==id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CourseEditVM courseEditVM,List<int>List/*,List<string> ListUser*/)
        {
            var users = await _usermanager.GetUsersInRoleAsync("CourseManager");
            ViewBag.Roles = users;
            ViewBag.Categories = _db.Categories.Where(c=>!c.IsDeleted).ToList();
            if (!ModelState.IsValid) return View();
            Course dbCourse = _db.Courses.Where(c => c.IsDeleted == false).Include(c => c.CourseCategories).FirstOrDefault(c => c.Id == courseEditVM.Id);
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
            dbCourse.CourseFee = courseEditVM.CourseFee;
            dbCourse.AboutCourse = courseEditVM.AboutCourse;
            dbCourse.HowToApply = courseEditVM.HowToApply;
            dbCourse.Certification = courseEditVM.Certification;
            var dbcoursecategory = _db.CourseCategories.Where(p => p.CourseId == dbCourse.Id);

            //foreach (var item in dbCourse.CourseUsers)
            //{
            //    dbCourse.CourseUsers.Remove(item);
            //}
            //List<CourseUser> newusers = new List<CourseUser>();
            //foreach (var item in ListUser)
            //{
            //    CourseUser courseUser = new CourseUser
            //    {
            //        CourseId = dbCourse.Id,
            //        AppUserId = item
            //    };
            //    newusers.Add(courseUser);
            //}
            //dbCourse.CourseUsers = newusers;
            //_db.SaveChanges();

            foreach (var item in dbcoursecategory)
            {
                dbCourse.CourseCategories.Remove(item);

            }
            _db.SaveChanges();
           
            List<CourseCategory> courseCategories = new List<CourseCategory>();
            foreach (var item in List)
            {
                CourseCategory newcourseCategory = new CourseCategory
                {
                    CourseId = dbCourse.Id,
                    CategoryId = item
                };
                courseCategories.Add(newcourseCategory);

            }
            dbCourse.CourseCategories = courseCategories;

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
