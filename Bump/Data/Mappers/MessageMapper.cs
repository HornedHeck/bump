using System.Collections.Generic;
using System.Linq;
using Bump.Data.Models;

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
                media: new int[0],
                theme: item.Theme.Id
            );
        }

        public static IEnumerable<Entities.Message> Map(this IEnumerable<Message> items) =>
            items.Select(it => it.Map());

        public static Message Map(this Entities.Message entity, Theme theme)
        {
            return new Message
            {
                Id = entity.Id,
                Author = entity.Author.Map(),
                Content = entity.Content,
                Theme = theme
            };
        }

        public static IEnumerable<Message> Map(this IEnumerable<Entities.Message> entities, Theme theme) =>
            entities.Select(it => it.Map(theme));
    }
}