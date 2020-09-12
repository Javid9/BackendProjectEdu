    using EducationBackendFinal.DAL;
using EducationBackendFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.ViewComponents
{
    
    public class CourseViewComponent:ViewComponent
    {
        private readonly AppDbContext _db;
        public CourseViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take=6)
        {
            List<Course> model = _db.Courses.Take(take).ToList();
            return View(await Task.FromResult(model));
        }
    }
}
