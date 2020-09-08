using System.Collections.Generic;
using Entities;

namespace Data
{
    public interface ILocalApi
    {

        User GetCurrentUser();

        void Logout();

        bool Login(string username , string password);

        void Register(string name , string password , string visibleName);

        Media LoadMedia(int id);

        Theme GetTheme(int id);

        List<ThemeHeader> GetThemeHeaders();

        void CreateMessage(Message message);

        void UpdateMessage(Message message);

        void DeleteMessage(int id);

        Message GetMessage(int id);

    }
}