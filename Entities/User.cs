using System.Diagnostics.CodeAnalysis;

namespace Entities
{
    public class User
    {
        public User(int id, string name, string login, int? photo = null)
        {
            Id = id;
            Name = name;
            Login = login;
            Photo = photo;
        }

        public int Id { get; }

        public string Name { get; }
        
        public string Login { get; }

        public int? Photo { get; }
    }
}