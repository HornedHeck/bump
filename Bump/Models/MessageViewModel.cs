using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Bump.Models
{
    public class Message
    {
        public Message()
        {
        }

        public Message(Entities.Message message)
        {
            Id = message.Id;
            Theme = message.Theme;
            Author = message.Author;
            Content = message.Content;
            Media = message.Media.ToList();
        }

        public int Id { get; set; }
        
        public int Theme { get; set; }

        public User Author { get; set; }

        public string Content { get; set; }

        public List<int> Media { get; } = new List<int>();

        public string Method { get; set; }

        public Entities.Message Convert() =>
            new Entities.Message(
                id: Id,
                author: Author,
                content: Content,
                media: Media.ToArray(),
                theme: Theme
            );
    }
}