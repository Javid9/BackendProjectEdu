using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationBackendFinal.Models;
using EducationBackendFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EducationBackendFinal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager,
                             SignInManager<AppUser> signInManager,
                             RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
       public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm login)
        {
            if (!ModelState.IsValid) return View();

            AppUser loginUser = await _userManager.FindByEmailAsync(login.Email);
            if (loginUser == null)
            {
                ModelState.AddModelError("", "Email or password wrong!");
                return View(login);
            }
            if (!loginUser.IsActivated)
            {
                ModelState.AddModelError("", "Emaliniz block olunub");
                return View(login);
            }


            var signInResult = await _signInManager.PasswordSignInAsync(loginUser, login.Password, login.RememberMe, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "The account is locked out!");
                return View(login);
            }

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or password wrong!");
                return View(login);
            }


            var role = await _userManager.GetRolesAsync(loginUser);

            foreach (var item in role)
            {
                if (item == "Admin")
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
            }

            foreach (var item in role)
            {
                if (item == "CourseManager")
                {
                    return RedirectToAction("Index", "Course", new { area = "Admin" });
                }
            }


            return RedirectToAction("Index", "Home");
        }
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm register)
        {
            if (!ModelState.IsValid) return View();
            AppUser newUser = new AppUser
            {
                Fullname = register.Fullname,
                Email = register.Email,
                UserName = register.Username
            };

            IdentityResult identityResult = await _userManager.CreateAsync(newUser, register.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }
            newUser.IsActivated =true;
           await _userManager.AddToRoleAsync(newUser, "Member");
           
            await _signInManager.SignInAsync(newUser,true);
            return RedirectToAction("Index", "Home");
           
        }
        //public async Task CreateRole()
        //{
        //    if (!await _roleManager.RoleExistsAsync("Admin"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

        //    }
        //    if (!await _roleManager.RoleExistsAsync("Member"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });

        //    }
        //    if (!await _roleManager.RoleExistsAsync("Blogger"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Blogger" });

        //    }
        //}

    }
}
