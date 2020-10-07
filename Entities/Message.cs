using System;
using System.Collections.Generic;

namespace Entities
{
    public class Message
    {
        public Message() { }

        public Message(int id, User author, string content, long[] media, int theme, DateTime creationTime , List<Vote> votes)
        {
            Id = id;
            Author = author;
            Content = content;
            Media = media;
            Theme = theme;
            CreationTime = creationTime;
            Votes = votes;
        }

        public DateTime CreationTime { get; set; }
        
        public int Id { get; set; }

        public User Author { get; }
        
        public string Content { get; set; }

        public long[] Media { get; set; }
        
        public int Theme { set; get; }

        public List<Vote> Votes { get; set; }

    }
}