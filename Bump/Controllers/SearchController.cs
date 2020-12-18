using System.Collections.Generic;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bump.Controllers
{
    public class SearchController : Controller
    {
        private readonly ThemeRepo _repo;

        public SearchController(ThemeRepo repo)
        {
            _repo = repo;
        }

        public IActionResult Search(string query)
        {
            var themes = !string.IsNullOrEmpty(query) ? _repo.SearchThemes(query) : new List<Theme>();
            return View(themes);
        }
    }
}