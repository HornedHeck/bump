using System.Collections.Generic;
using Data;
using Data.Repo;
using Entities;

namespace Bump.Data.Repo
{
    public class ThemeRepoImpl : IThemeRepo
    {
        private readonly ILocalApi _local;

        public ThemeRepoImpl(ILocalApi local)
        {
            _local = local;
        }

        public Theme GetTheme(long id) => _local.GetTheme(id);

        public List<Theme> GetThemes(long subcategory)
        {
            return _local.GetThemes(subcategory);
        }

        public List<ThemeCategory> GetCategories()
        {
            return _local.GetCategories();
        }

        public List<ThemeSubcategory> GetSubcategories(long category)
        {
            return _local.GetSubcategories(category);
        }

        public void CreateTheme(Theme theme)
        {
            _local.CreateTheme(theme);
        }

        public void UpdateTheme(long theme, string title, string content, long[] media)
        {
            _local.UpdateTheme(theme, title, content, media);
        }
    }
}