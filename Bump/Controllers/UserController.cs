using System.Collections.Generic;
using System.Security.Claims;
using Bump.Models;
using Data.Repo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LoginModel = Bump.Models.LoginModel;

namespace Bump.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            _userRepo.Login(login.Convert());
            Authenticate(login.Login);
            return Redirect("~/User/Profile");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        private void Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            var id = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public RedirectResult Logout()
        {
            _userRepo.Logout();
            return Redirect("~/User/Start");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationModel model)
        {
            if (!ModelState.IsValid) return View(model);

            Authenticate(model.Name);
            _userRepo.Register(model.Name);
            return Redirect(new PathString("/User/Profile"));
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(_userRepo.GetCurrentUser());
        }
    }
}