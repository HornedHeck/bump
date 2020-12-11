using Bump.Auth;
using Bump.VM;
using Data.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bump.Controllers
{
    [Authorize(Roles = AuthConstants.Admin)]
    public class AdminController : Controller
    {
        private readonly ThemeRepo _repo;

        public AdminController(ThemeRepo repo)
        {
            _repo = repo;
        }

        public IActionResult AdminNavigation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Categories()
        {
            var categories = _repo.GetCategories();
            return View(categories);
        }

        [HttpPost]
        public IActionResult Category(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                _repo.CreateCategory(name);
            }

            return RedirectToAction("Categories");
        }

        [HttpGet]
        public IActionResult Subcategories(int category)
        {
            var subcategories = _repo.GetSubcategories(category);
            return View(new AdminSubcategoriesVm
            {
                Subcategories = subcategories,
                Category = category
            });
        }

        [HttpPost]
        public IActionResult Subcategory(int category, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                _repo.CreateSubcategory(name, category);
            }

            return RedirectToAction("Subcategories", new {category = category});
        }
    }
}