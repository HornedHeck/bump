using System.Collections.Generic;
using Entities;

namespace Data.Api.Local
{
    public interface ILocalApi
    {
        void AddUser(User user);

        Media GetMedia(long id);

        void AddMedia(Media media);

        void RemoveMedia(long id);

        Theme GetTheme(long id);

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

        void VoteUp(int message, Vote vote);

        public void UpdateTheme(long theme, string title, string content, IEnumerable<long> media);

        public List<Theme> SearchThemes(string query);

    }
}