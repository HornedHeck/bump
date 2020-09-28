using System.Collections.Generic;
using Bump.Auth;

namespace Bump.Models
{
    public class MessageVM
    {
        public int Id { get; set; }

        public int Theme { get; set; }

        public BumpUser Author { get; set; }

        public string Content { get; set; }

        public List<int> Media { get; } = new List<int>();

        public string Method { get; set; }
    }
}