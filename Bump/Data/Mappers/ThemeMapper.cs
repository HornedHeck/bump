using System.Collections.Generic;
using System.Linq;
using Entities;
using Microsoft.EntityFrameworkCore;
using LThemeSubcategory = Bump.Data.Models.ThemeSubcategory;
using LTheme = Bump.Data.Models.Theme;
using Media = Bump.Data.Models.Media;

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
                messages: item.Messages.Map().OrderBy(it => it.CreationTime).Reverse().ToArray(),
                media: item.Media.Select(it => it.Id).ToArray(),
                creationTime: item.CreationTime,
                subcategory: item.Subcategory.Map()
            );
        }

        public static IEnumerable<Theme> Map(this IEnumerable<LTheme> items) => items.Select(Map);

        public static LTheme Map(this Theme entity, LThemeSubcategory subcategory, DbSet<Media> mediaSet)
        {
            var theme = new LTheme
            {
                Id = entity.Id,
                Author = entity.Author.Map(),
                Content = entity.Content,
                Title = entity.Name,
                Subcategory = subcategory,
                CreationTime = entity.CreationTime,
                Media = entity.Media.Select(it => mediaSet.Find(it)).ToList(),
            };
            theme.Messages = entity.Messages.Map(theme, mediaSet).ToList();
            return theme;
        }
    }
}