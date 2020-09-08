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

        public bool Login(string username, string password) => _local.Login(username, password);

        public void Register(string username, string password, string visibleName) =>
            _local.Register(username, password, visibleName);
    }
}