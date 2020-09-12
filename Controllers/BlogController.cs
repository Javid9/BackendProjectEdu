using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Models;
using Microsoft.AspNetCore.Mvc;

namespace EducationBackendFinal.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db)
        {
            _db = db;
        }
        //public IActionResult Index()
        //{

        //    return View();
        //}
        public IActionResult Index(int? page)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_db.Blogs.Count() / 3);
            ViewBag.Page = page;
            if (page == null)
            {
                return View(_db.Blogs.OrderByDescending(p => p.Id).Take(3).ToList());
            }
            else
            {
                return View(_db.Blogs.OrderByDescending(p => p.Id).Skip(((int)page - 1) * 3).Take(3).ToList());
            }

        }
        public IActionResult Detail()
        {

            return View();
        }
    }
}
