using Data.Models;
using Entities;

namespace Data.Mappers {

    internal static class UserMapper {

        internal static User Map( this BumpUser item ) =>
            new User( item.UserId );

        internal static BumpUser Map( this User entity ) =>
            new BumpUser {
                UserId = entity.Id
            };

    }

}