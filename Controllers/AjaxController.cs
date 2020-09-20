using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Models;
using Microsoft.AspNetCore.Mvc;

namespace EducationBackendFinal.Controllers
{
    
    public class AjaxController : Controller
    {
        private readonly AppDbContext _db;
        public AjaxController(AppDbContext db)
        {
            _db = db;

        }
        public IActionResult Search(string search,string hidden)
        {
            IEnumerable<BaseEntity> list = new List<BaseEntity>();
            switch (hidden)
            {
                case "teacher":
                  list =  _db.Teachers.Where(x => x.FullName.ToLower().Contains(search.ToLower()));
                    break;
                case "blog":
                    list = _db.Blogs.Where(x => x.Author.ToLower().Contains(search.ToLower()));
                    break;
                case "course":
                    list = _db.Courses.Where(x => x.Title.ToLower().Contains(search.ToLower()));
                    break;
                default:
                    break;
                  
            }
            
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> Subscribe([FromForm]string email)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            bool subscription = _db.Subscriptions.Any(e => e.Email == email);
            ViewBag.exist = subscription;
           
            var sub = new Subscription { Email = email };
            await _db.Subscriptions.AddAsync(sub);
            await _db.SaveChangesAsync();
           
            return Ok(sub.Id);
        }
    }
}
