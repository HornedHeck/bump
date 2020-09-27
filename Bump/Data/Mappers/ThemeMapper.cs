using System.Collections.Generic;
using System.Linq;
using Entities;
using Message = Bump.Data.Models.Message;
using Theme = Bump.Data.Models.Theme;

namespace Bump.Data.Mappers
{
    public static class ThemeMapper
    {
        public static Entities.Theme Map(this Theme item)
        {
            return new Entities.Theme(
                id: item.Id,
                author: item.Author.Map(),
                name: item.Title,
                content: item.Content,
                messages: item.Messages.Map().ToArray(),
                media: new int[0]
            );
        }

        public static Theme Map(this Entities.Theme entity)
        {
            var theme = new Theme
            {
                Id = entity.Id,
                Author = entity.Author.Map(),
                Content = entity.Content,
                Title = entity.Name
            };
            theme.Messages = entity.Messages.Map(theme).ToList();
            return theme;
        }

        public static IEnumerable<ThemeHeader> Map(this IEnumerable<Theme> items)
        {
            return items.Select(it =>
                new ThemeHeader(
                    id: it.Id,
                    name: it.Title,
                    author: it.Author.Convert()
                )
            );
        }
    }
}