using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Bump.Data;
using Bump.Models;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bump.Controllers
{
    public class ThemeController : Controller
    {
        private readonly IThemeRepo _repo;
        private readonly FileManager _fileManager;

        public ThemeController(IThemeRepo repo, FileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        [HttpGet]
        public IActionResult CreateTheme(long subcategory)
        {
            var model = new ThemeVm
            {
                Subcategory = new ThemeSubcategory {Id = subcategory},
                Media = new List<long>()
            };
            return View("Theme", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditTheme(ThemeVm vm, IFormFile uploadingMedia, string action = null)
        {
            vm.Media ??= new List<long>();

            if (uploadingMedia != null)
            {
                var mediaId = await _fileManager.SaveFile(uploadingMedia);
                vm.Media.Add(mediaId);
                return View("Theme", vm);
            }

            if (ModelState.IsValid)
            {
                if (action == "CreateTheme")
                {
                    return View("Theme", vm);
                }

                var author = new User(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var theme = new Theme(
                    id: 0,
                    author: author,
                    name: vm.Title,
                    content: vm.Content,
                    messages: new Message[0],
                    media: vm.Media.ToArray(),
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

        [HttpPost]
        public async Task<IActionResult> UploadMedia(IList<IFormFile> files)
        {
            foreach (var file in files)
            {
                await _fileManager.SaveFile(file);
            }

            Response.Clear();
            Response.StatusCode = 204;
            return Content("");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMedia(IList<IFormFile> files)
        {
            Response.Clear();
            Response.StatusCode = 204;
            return Content("");
        }
    }
}