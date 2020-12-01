using System.Diagnostics;
using System.Threading.Tasks;
using Bump.Auth;
using Microsoft.AspNetCore.Mvc;
using Bump.Models;
using Data.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Bump.Controllers
{
    public class HomeController : Controller
    {
        private readonly ThemeRepo _themeRepo;
        private readonly UserManager<BumpUser> _userManager;

        public HomeController(ThemeRepo themeRepo, UserManager<BumpUser> userManager)
        {
            _themeRepo = themeRepo;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_themeRepo.GetCategories());
        }

        public IActionResult Category(long category)
        {
            return View(_themeRepo.GetSubcategories(category));
        }

        public IActionResult Subcategory(long subcategory)
        {
            return View(new SubcategoryVM
            {
                Id = subcategory,
                Themes = _themeRepo.GetThemes(subcategory)
            });
        }

        [Authorize]
        public async Task<IActionResult> Theme(int themeId)
        {
            var entity = _themeRepo.GetTheme(themeId);
            return View(await entity.ToVm(_userManager));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}