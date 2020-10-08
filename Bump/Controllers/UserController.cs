using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Models;
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
        private readonly UserManager<BumpUser> _userManager;

        public UserController(UserManager<BumpUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            return View(await _userManager.FindByNameAsync(User.Identity.Name));
        }
    }
}