using System.Diagnostics.CodeAnalysis;

namespace Entities
{
    public class User
    {
        public User(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}