using Bump.Data.Models;
using Entities;

namespace Bump.Data.Mappers
{
    public static class UserMapper
    {
        public static User Map(this BumpUser item) =>
            new User(
                id: item.UserId
            );

        public static BumpUser Map(this User entity) =>
            new BumpUser
            {
                UserId = entity.Id,
            };
    }
}