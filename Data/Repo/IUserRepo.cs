using Entities;

namespace Data.Repo
{
    public interface IUserRepo
    {

        public User GetCurrentUser();

        public void Logout();

        public void Login();

    }
}