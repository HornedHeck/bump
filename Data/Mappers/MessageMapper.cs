using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LMessage = Data.Models.Message;
using Message = Entities.Message;
using LTheme = Data.Models.Theme;
using LMedia = Data.Models.Media;
using Vote = Entities.Vote;

namespace Data.Mappers
{
    internal static class MessageMapper
    {
        internal static Message Map(this LMessage item)
        {
            return new Message(
                item.Id,
                item.Author.Map(),
                item.Content,
                item.Media.Select(it => it.Id).ToArray(),
                item.Theme.Id,
                item.CreationTime,
                item.Votes?.Map()?.ToList() ?? new List<Vote>()
            );
        }

        internal static IEnumerable<Message> Map(this IEnumerable<LMessage> items) =>
            items.Select(it => it.Map());

        internal static LMessage Map(this Message entity, LTheme theme, DbSet<LMedia> mediaSet)
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

        internal static IEnumerable<LMessage> Map(this IEnumerable<Message> entities, LTheme theme , DbSet<LMedia> mediaSet) =>
            entities.Select(it => it.Map(theme, mediaSet));
    }
}