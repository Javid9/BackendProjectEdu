using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Models;
using EducationBackendFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EducationBackendFinal.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _db;
        public TeacherController(AppDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Search(string search)
        {

            List<Teacher> model = _db.Teachers.Where(p => p.FullName.ToLower().Contains(search.ToLower())).ToList()/*.OrderByDescending(p => p.Id).Take(5)*/;

            return PartialView("_partialSearch", model);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = _db.Teachers.FirstOrDefault(p => p.Id==id);
            return View(teacher);
        }
    }
}
