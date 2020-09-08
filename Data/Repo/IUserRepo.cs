using Entities;

namespace Data.Repo
{
    public interface IUserRepo
    {
        public User GetCurrentUser();

        public void Logout();

        public bool Login(string username, string password);

        public void Register(string username, string password, string visibleName);
    }
}