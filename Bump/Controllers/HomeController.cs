using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bump.Models;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Authorization;

namespace Bump.Controllers
{
    public class HomeController : Controller
    {
        private readonly IThemeRepo _themeRepo;

        public HomeController(IThemeRepo themeRepo)
        {
            _themeRepo = themeRepo;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_themeRepo.GetThemeHeaders());
        }

        [Authorize]
        public IActionResult Theme(int themeId)
        {
            return View(_themeRepo.GetTheme(themeId));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}