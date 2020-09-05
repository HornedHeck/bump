using Data;
using Data.Repo;
using Entities;

namespace Bump.Data.Repo
{
    public class UserRepoImpl : IUserRepo
    {
        private readonly ILocalApi _local;

        public UserRepoImpl(ILocalApi local)
        {
            _local = local;
        }

        public User GetCurrentUser() => _local.GetCurrentUser();

        public void Logout() => _local.Logout();

        public void Login() => _local.Login();

    }
}