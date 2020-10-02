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

        public Theme GetTheme(int id) => _local.GetTheme(id);

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
    }
}