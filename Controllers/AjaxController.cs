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
            IEnumerable<BaseEntity> list = new List<Teacher>();
            switch (hidden)
            {
                case "teacher":
                  list =  _db.Teachers.Where(x => x.FullName.ToLower().Contains(search.ToLower()));
                    break;
                case "course":
                    list = _db.Courses.Where(x => x.Title.ToLower().Contains(search.ToLower()));
                    break;
                default:
                    break;
                  
            }
            
            return Ok(list);
        }
    }
}
