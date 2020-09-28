using System.Collections.Generic;
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
        private readonly IThemeRepo _themeRepo;
        private readonly UserManager<BumpUser> _userManager;

        public HomeController(IThemeRepo themeRepo, UserManager<BumpUser> userManager)
        {
            _themeRepo = themeRepo;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_themeRepo.GetThemeHeaders());
        }

        [Authorize]
        public async Task<IActionResult> Theme(int themeId)
        {
            var entity = _themeRepo.GetTheme(themeId);
            var messages = new List<MessageVM>(entity.Messages.Length);
            foreach (var message in entity.Messages)
            {
                messages.Add(new MessageVM
                {
                    Id = message.Id,
                    Author = await _userManager.FindByIdAsync(message.Author.Id),
                    Content = message.Content,
                    Theme = message.Theme
                });
            }

            var theme = new ThemeVM
            {
                Author = await _userManager.FindByIdAsync(entity.Author.Id),
                Content = entity.Content,
                Title = entity.Name,
                Messages = messages
            };

            return View(theme);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}