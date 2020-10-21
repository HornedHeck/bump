using System.Collections.Generic;
using Entities;

namespace Data.Repo
{
    public interface IThemeRepo
    {
        Theme GetTheme(long id);

        List<Theme> GetThemes(long subcategory);

        List<ThemeCategory> GetCategories();

        List<ThemeSubcategory> GetSubcategories(long category);

        void CreateTheme(Theme theme);
        
        void UpdateTheme(long theme, string title, string content, long[] media);
    }
}