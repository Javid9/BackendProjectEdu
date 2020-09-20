using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.DAL;
using EducationBackendFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EducationBackendFinal.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SkillController : Controller
    {
        private readonly AppDbContext _db;
       
        public SkillController(AppDbContext db)
        {
            _db = db; 
        }
        public IActionResult Index()
        {
            return View(_db.Skills.Include(s=>s.Teacher).Where(t=>!t.Teacher.IsDeleted).ToList());
        }



        public IActionResult Create()
        {
            //ViewBag.Teachers = _db.Teachers.ToList();
            ViewBag.Teachers = _db.Teachers.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill skill)
        {
            //ViewBag.Teachers = _db.Teachers.ToList();
            ViewBag.Teachers = _db.Teachers.ToList();
            if (!ModelState.IsValid) return View();

            Skill existSkill = _db.Skills.FirstOrDefault(s => s.TeacherId == skill.TeacherId);
            if (existSkill != null)
            {
                ModelState.AddModelError("TeacherId", $" {existSkill.Teacher.FullName} -in skilleri movcuddur,update edin zehmet olmasa");
                return View();
            }
            Skill newSkill = new Skill
            {
                Language = skill.Language,
                Design = skill.Design,
                Development = skill.Development,
                Innovation = skill.Innovation,
                Communication = skill.Communication,
                TeamLeader = skill.TeamLeader,
                TeacherId=skill.TeacherId
            };

            _db.Skills.Add(newSkill);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null) return NotFound();
        //    Slider slider = await _db.Sliders.FindAsync(id);
        //    if (slider == null) return NotFound();
        //    return View(slider);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Delete")]
        //public async Task<IActionResult> DeletePost(int? id)
        //{
        //    if (id == null) return NotFound();
        //    Slider slider = await _db.Sliders.FindAsync(id);
        //    if (slider == null) return NotFound();
        //    string path = Path.Combine("img", "slider");
        //    Helper.DeleteImage(_env.WebRootPath, path, slider.Image);

        //    _db.Sliders.Remove(slider);
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Skill skill = await _db.Skills.FindAsync(id);
            if (skill == null) return NotFound();
            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Skill skill)
        {
            if (id == null) return NotFound();
            Skill dbskill = await _db.Skills.FindAsync(id);
            if (dbskill == null) return NotFound();

            dbskill.Language = skill.Language;
            dbskill.Design = skill.Design;
            dbskill.Development = skill.Development;
            dbskill.TeamLeader = skill.TeamLeader;
            dbskill.Innovation = skill.Innovation;
            dbskill.Communication = skill.Communication;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

