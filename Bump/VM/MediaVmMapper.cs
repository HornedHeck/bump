using Bump.Services;
using Entities;

namespace Bump.VM {

    public static class MediaVmMapper {

        public static MediaVm ToVm( this Media entity ) {
            return new MediaVm {
                Name = entity.Name ,
                Path = FileManager.GetPath( entity )
            };
        }

    }

}