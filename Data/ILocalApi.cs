using System.Collections.Generic;
using Entities;

namespace Data
{
    public interface ILocalApi
    {
        void AddUser(User user);

        Media GetMedia(long id);

        void AddMedia(Media media);

        Theme GetTheme(int id);

        void CreateTheme(Theme theme);


        List<ThemeCategory> GetCategories();

        List<ThemeSubcategory> GetSubcategories(long category);

        List<Theme> GetThemes(long subcategory);

        void AddCategory(ThemeCategory category);

        void AddSubcategory(ThemeSubcategory subcategory);

        void CreateMessage(Message message);

        void UpdateMessage(Message message);

        void DeleteMessage(int id);

        Message GetMessage(int id);

        void ResetDatabase();
    }
}