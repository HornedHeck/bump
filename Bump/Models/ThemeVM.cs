using System.Collections.Generic;
using Bump.Auth;

namespace Bump.Models
{
    public class ThemeVM
    {
        public BumpUser Author { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public List<MessageVM> Messages { get; set; }
    }
}