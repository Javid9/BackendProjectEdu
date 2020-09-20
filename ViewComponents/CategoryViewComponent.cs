using EducationBackendFinal.DAL;
using EducationBackendFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {

        private readonly AppDbContext _db;
        public CategoryViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            List<Category> model = _db.Categories.Where(c=>c.IsDeleted==false).ToList();
            return View(await Task.FromResult(model));
        }

    }
}
