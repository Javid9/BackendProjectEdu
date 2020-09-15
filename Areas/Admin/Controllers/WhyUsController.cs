using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EducationBackendFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WhyUsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        public WhyUsController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            WhyUs whyUs = _db.WhyUs.FirstOrDefault();
            return View(whyUs);
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            WhyUs whyUs = _db.WhyUs.FirstOrDefault(p => p.Id == id);
            if (whyUs == null) return NotFound();
            return View(whyUs);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, WhyUs whyus)
        {
            if (id == null) return NotFound();
            WhyUs dbWhyus = await _db.WhyUs.FindAsync(id);
            if (dbWhyus == null) return NotFound();
            dbWhyus.Answer = whyus.Answer;
            dbWhyus.Question = whyus.Question;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }

}


