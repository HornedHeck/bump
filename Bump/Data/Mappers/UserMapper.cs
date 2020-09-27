using Bump.Data.Models;
using Entities;

namespace Bump.Data.Mappers
{
    public static class UserMapper
    {
        public static User Map(this BumpUser item) =>
            new User(
                id: item.Id,
                name: item.Name,
                login: item.Login
            );

        public static BumpUser Map(this User entity) =>
            new BumpUser
            {
                Id = entity.Id,
                Login = entity.Login,
                Name = entity.Name
            };
    }
}