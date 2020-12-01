using System.Collections.Generic;
using System.Linq;
using Entities;
using LVote = Data.Models.Vote;

namespace Data.Mappers
{
    internal static class VoteMapper
    {
        private static Vote Map(this LVote item) => new Vote {UserId = item.UserId};

        internal static LVote Map(this Vote entity) => new LVote {UserId = entity.UserId};
        
        internal static IEnumerable<Vote> Map(this IEnumerable<LVote> items) => items.Select(Map);
        
    }
}