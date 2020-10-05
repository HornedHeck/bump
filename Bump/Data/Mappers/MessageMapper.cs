using System.Collections.Generic;
using System.Linq;
using Bump.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bump.Data.Mappers
{
    public static class MessageMapper
    {
        public static Entities.Message Map(this Message item)
        {
            return new Entities.Message(
                id: item.Id,
                author: item.Author.Map(),
                content: item.Content,
                media: item.Media.Select(it => it.Id).ToArray(),
                theme: item.Theme.Id
            );
        }

        public static IEnumerable<Entities.Message> Map(this IEnumerable<Message> items) =>
            items.Select(it => it.Map());

        public static Message Map(this Entities.Message entity, Theme theme, DbSet<Media> mediaSet)
        {
            return new Message
            {
                Id = entity.Id,
                Author = entity.Author.Map(),
                Content = entity.Content,
                Theme = theme,
                Media = mediaSet.Where(it => entity.Media.Contains(it.Id)).ToList()
            };
        }

        public static IEnumerable<Message> Map(this IEnumerable<Entities.Message> entities, Theme theme , DbSet<Media> mediaSet) =>
            entities.Select(it => it.Map(theme, mediaSet));
    }
}