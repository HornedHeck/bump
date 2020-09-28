using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bump.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<BumpUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<BumpUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> UserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = _roleManager.Roles.ToList();
            var userRoles = (await _userManager.GetRolesAsync(user)).ToImmutableHashSet();

            var model = new UserRolesViewModel
            {
                Name = user.UserName,
                Roles = roles,
                EnabledRoles = userRoles,
                UserId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserRoles(string action, List<string> roles, string id)
        {
            if (action == "save")
            {
                var user = await _userManager.FindByIdAsync(id);
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.AddToRolesAsync(user, roles.Except(userRoles));
                await _userManager.RemoveFromRolesAsync(user, userRoles.Except(roles));
            }

            return RedirectToAction("Users");
        }
    }
}