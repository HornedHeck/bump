using System.Diagnostics.CodeAnalysis;

namespace Entities
{
    public class User
    {
        public User(int id, string name, int? photo = null)
        {
            Id = id;
            Name = name;
            Photo = photo;
        }

        public int Id { get; }

        public string Name { get; }

        public int? Photo { get; }
    }
}