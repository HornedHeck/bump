using System.Collections.Generic;
using System.Linq;
using Entities;
using LThemeSubcategory = Bump.Data.Models.ThemeSubcategory;
using LTheme = Bump.Data.Models.Theme;

namespace Bump.Data.Mappers
{
    public static class ThemeMapper
    {
        public static Theme Map(this LTheme item)
        {
            return new Theme(
                id: item.Id,
                author: item.Author.Map(),
                name: item.Title,
                content: item.Content,
                messages: item.Messages.Map().ToArray(),
                media: new int[0]
            );
        }

        public static IEnumerable<Theme> Map(this IEnumerable<LTheme> items) => items.Select(Map);

        public static LTheme Map(this Theme entity , LThemeSubcategory subcategory)
        {
            var theme = new LTheme
            {
                Id = entity.Id,
                Author = entity.Author.Map(),
                Content = entity.Content,
                Title = entity.Name,
                Subcategory = subcategory
            };
            theme.Messages = entity.Messages.Map(theme).ToList();
            return theme;
        }
    }
}