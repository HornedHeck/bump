using System;
using System.Security.Claims;
using Bump.Models;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bump.Controllers
{
    public class ThemeController : Controller
    {
        private readonly IThemeRepo _repo;

        public ThemeController(IThemeRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult CreateTheme(long subcategory)
        {
            var model = new ThemeVm
            {
                Subcategory = new ThemeSubcategory {Id = subcategory}
            };
            return View("Theme", model);
        }

        [HttpPost]
        public IActionResult EditTheme(ThemeVm vm)
        {
            if (ModelState.IsValid)
            {
                var author = new User(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var theme = new Theme(
                    id: 0,
                    author: author,
                    name: vm.Title,
                    content: vm.Content,
                    messages: new Message[0],
                    media: new int[0],
                    creationTime: DateTime.Now
                )
                {
                    Subcategory = vm.Subcategory
                };
                _repo.CreateTheme(theme);
                return RedirectToAction("Theme", "Home", new {themeId = theme.Id});
            }
            else
            {
                return View("Theme", vm);
            }
        }
    }
}