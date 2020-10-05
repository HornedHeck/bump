using Bump.Data;
using Entities;

namespace Bump.Models
{
    public class MediaVm
    {
        public string Name { get; set; }

        public string Path { get; set; }
    }

    public static class MediaVmMapper
    {
        public static MediaVm ToVm(this Media entity)
        {
            return new MediaVm
            {
                Name = entity.Name,
                Path = FileManager.GetPath(entity),
            };
        }
    }
}