using System.Collections.Generic;
using Data.Api.Local;
using Entities;

namespace Data.Repo
{
    public class ThemeRepo
    {
        private readonly ILocalApi _local;

        internal ThemeRepo(ILocalApi local) => _local = local;

        public Theme GetTheme(long id) => _local.GetTheme(id);

        public List<Theme> GetThemes(long subcategory) => _local.GetThemes(subcategory);

        public List<ThemeCategory> GetCategories() => _local.GetCategories();

        public List<ThemeSubcategory> GetSubcategories(long category) => _local.GetSubcategories(category);

        public void CreateTheme(Theme theme) => _local.CreateTheme(theme);

        public void UpdateTheme(long theme, string title, string content, IEnumerable<long> media) =>
            _local.UpdateTheme(theme, title, content, media);
    }
}