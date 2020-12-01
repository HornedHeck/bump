using Data.Api.Local;
using Entities;

namespace Data.Repo
{
    public class UserRepo
    {
        private readonly ILocalApi _local;

        internal UserRepo(ILocalApi local)
        {
            _local = local;
        }

        public void AddUser(User user) => _local.AddUser(user);
    }
}