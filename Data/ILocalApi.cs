using System.Collections.Generic;
using Entities;

namespace Data
{
    public interface ILocalApi
    {

        User GetCurrentUser();

        void Logout();

        bool Login(string login , string password);

        void Register(string login , string password , string name);

        Media LoadMedia(int id);

        Theme GetTheme(int id);

        void CreateTheme(Theme theme);
        
        List<ThemeHeader> GetThemeHeaders();

        void CreateMessage(Message message);

        void UpdateMessage(Message message);

        void DeleteMessage(int id);

        Message GetMessage(int id);

    }
}