using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Models;
using Microsoft.AspNetCore.Mvc;

namespace EducationBackendFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Categories.Where(c=>c.IsDeleted==false).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View();
            bool existName = _db.Categories.Any(c => c.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            if (existName)
            {
                ModelState.AddModelError("Name", "This name category is existed");
                return View();
            }
            Category newCategory = new Category
            {
                Name = category.Name
            };

            await _db.AddAsync(newCategory);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public  IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Category dbCategory =  _db.Categories.FirstOrDefault(c=>c.Id == id);
            if (dbCategory == null) return NotFound();
            return View(dbCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {

            if (id == null) return NotFound();
            Category dbCategory =  _db.Categories.FirstOrDefault(c=>c.Id == id);
            if (dbCategory == null) return NotFound();
            Category exisCategory =  _db.Categories.FirstOrDefault(x=>x.Name == category.Name);
            if(exisCategory!= null)
            {
                if(dbCategory != exisCategory)
                {
                    ModelState.AddModelError("Name", "Bu adda Category movcuddur");
                    return View();
                }
            }
            dbCategory.Name = category.Name;
           await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Category category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            category.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
