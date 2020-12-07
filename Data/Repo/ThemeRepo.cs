using System.Collections.Generic;
using Data.Api.Live;
using Data.Api.Local;
using Entities;

namespace Data.Repo
{
    public class ThemeRepo
    {
        private readonly ILocalApi _local;
        private readonly ILiveUpdate _live;

        public ThemeRepo(ILocalApi local, ILiveUpdate live)
        {
            _local = local;
            _live = live;
        }

        public Theme GetTheme(long id) => _local.GetTheme(id);

        public List<Theme> GetThemes(long subcategory) => _local.GetThemes(subcategory);

        public List<ThemeCategory> GetCategories() => _local.GetCategories();

        public List<ThemeSubcategory> GetSubcategories(long category) => _local.GetSubcategories(category);

        public void CreateTheme(Theme theme)
        {
            _local.CreateTheme(theme);
            _live.NotifyThemeCreated(theme);
        }

        public void UpdateTheme(long theme, string title, string content, IEnumerable<long> media) =>
            _local.UpdateTheme(theme, title, content, media);
    }
}