using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Models;
using Data.Repo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginModel = Bump.Models.LoginModel;

namespace Bump.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly UserManager<BumpUser> _userManager;
        private readonly SignInManager<BumpUser> _signInManager;

        public UserController(IUserRepo userRepo, UserManager<BumpUser> userManager,
            SignInManager<BumpUser> signInManager)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var res = await _signInManager.PasswordSignInAsync(model.Name, model.Password, false, false);
            if (res.Succeeded)
            {
                return RedirectToAction("Profile", "User");
            }

            ModelState.AddModelError(string.Empty, "Wrong login or password");
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new BumpUser
            {
                UserName = model.Name,
                Email = model.Email,
            };
            var res = await _userManager.CreateAsync(user, model.Password);
            if (res.Succeeded)
            {
                _userRepo.AddUser(user.Map());
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Profile", "User");
            }

            foreach (var error in res.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            return View(await _userManager.FindByNameAsync(User.Identity.Name));
        }
    }
}