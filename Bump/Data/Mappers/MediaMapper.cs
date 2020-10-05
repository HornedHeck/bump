using Entities;
using Media = Entities.Media;
using LMedia = Bump.Data.Models.Media;

namespace Bump.Data.Mappers
{
    public static class MediaMapper
    {
        public static Media Map(this LMedia item) =>
            new Media
            {
                Id = item.Id,
                Name = item.Name,
                Type = (MediaType) item.Type
            };

        public static LMedia Map(this Media entity) =>
            new LMedia
            {
                Name = entity.Name,
                Type = (int) entity.Type
            };
    }
}