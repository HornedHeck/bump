using System.Collections.Generic;
using Entities;

namespace Data.Repo
{
    public interface IThemeRepo
    {
        Theme GetTheme(int id);

        List<Theme> GetThemes(long subcategory);

        List<ThemeCategory> GetCategories();

        List<ThemeSubcategory> GetSubcategories(long category);
    }
}