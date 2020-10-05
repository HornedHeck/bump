using System.Collections.Generic;

namespace Entities
{
    public class Message
    {
        public Message() { }

        public Message(int id, User author, string content, long[] media, int theme)
        {
            Id = id;
            Author = author;
            Content = content;
            Media = media;
            Theme = theme;
        }

        public int Id { get; set; }

        public User Author { get; }
        
        public string Content { get; set; }

        public long[] Media { get; set; }
        
        public int Theme { set; get; }

        public List<Vote> Votes { get; set; }

    }
}