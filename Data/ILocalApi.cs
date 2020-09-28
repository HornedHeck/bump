using System.Collections.Generic;
using Entities;

namespace Data
{
    public interface ILocalApi
    {
        void AddUser(User user);

        Media LoadMedia(int id);

        Theme GetTheme(int id);

        void CreateTheme(Theme theme);
        
        List<ThemeHeader> GetThemeHeaders();

        void CreateMessage(Message message);

        void UpdateMessage(Message message);

        void DeleteMessage(int id);

        Message GetMessage(int id);

        void ResetDatabase();

    }
}