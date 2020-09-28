using Data;
using Data.Repo;
using Entities;

namespace Bump.Data.Repo
{
    public class UserRepoImpl : IUserRepo
    {
        private readonly ILocalApi _local;

        public void AddUser(User user) => _local.AddUser(user);
    }
}