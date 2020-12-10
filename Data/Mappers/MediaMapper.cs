using System.Diagnostics.CodeAnalysis;
using Entities;
using Media = Entities.Media;
using LMedia = Data.Models.Media;

namespace Data.Mappers
{
    internal static class MediaMapper
    {
        internal static Media Map(this LMedia item) =>
            new Media
            {
                Id = item.Id,
                Name = item.Name,
                Type = (MediaType) item.Type
            };

        internal static LMedia Map(this Media entity) =>
            new LMedia
            {
                Name = entity.Name,
                Type = (int) entity.Type
            };
    }
}