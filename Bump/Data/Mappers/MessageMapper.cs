using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LMessage = Bump.Data.Models.Message;
using Message = Entities.Message;
using LTheme = Bump.Data.Models.Theme;
using LMedia = Bump.Data.Models.Media;
using Vote = Entities.Vote;

namespace Bump.Data.Mappers
{
    public static class MessageMapper
    {
        public static Message Map(this LMessage item)
        {
            return new Message(
                id: item.Id,
                author: item.Author.Map(),
                content: item.Content,
                media: item.Media.Select(it => it.Id).ToArray(),
                theme: item.Theme.Id,
                creationTime: item.CreationTime,
                votes: item.Votes?.Map()?.ToList() ?? new List<Vote>()
            );
        }

        public static IEnumerable<Message> Map(this IEnumerable<LMessage> items) =>
            items.Select(it => it.Map());

        public static LMessage Map(this Message entity, LTheme theme, DbSet<LMedia> mediaSet)
        {
            return new LMessage
            {
                Id = entity.Id,
                Author = entity.Author.Map(),
                Content = entity.Content,
                Theme = theme,
                Media = mediaSet.Where(it => entity.Media.Contains(it.Id)).ToList(),
                CreationTime = entity.CreationTime
            };
        }

        public static IEnumerable<LMessage> Map(this IEnumerable<Message> entities, LTheme theme , DbSet<LMedia> mediaSet) =>
            entities.Select(it => it.Map(theme, mediaSet));
    }
}