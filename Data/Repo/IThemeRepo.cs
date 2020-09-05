using System.Collections.Generic;
using Entities;

namespace Data.Repo
{
    public interface IThemeRepo
    {
        Theme GetTheme(int id);

        List<ThemeHeader> GetThemeHeaders();

    }
}