using System.Collections.Generic;
using Entities;

namespace Data
{
    public interface ILocalApi
    {

        User GetCurrentUser();

        void Logout();

        void Login();

        Media LoadMedia(int id);

        Theme GetTheme(int id);

        List<ThemeHeader> GetThemeHeaders();

        void CreateMessage(Message message);

        void UpdateMessage(Message message);

        void DeleteMessage(int id);

    }
}