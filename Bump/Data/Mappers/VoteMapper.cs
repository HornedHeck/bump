using System.Collections.Generic;
using System.Linq;
using Entities;
using LVote = Bump.Data.Models.Vote;

namespace Bump.Data.Mappers
{
    public static class VoteMapper
    {
        public static Vote Map(this LVote item) => new Vote {UserId = item.UserId};

        public static LVote Map(this Vote entity) => new LVote {UserId = entity.UserId};
        
        public static IEnumerable<Vote> Map(this IEnumerable<LVote> items) => items.Select(Map);
        
    }
}