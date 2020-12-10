using System;
using System.Collections.Generic;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Entities
{
    public class Message
    {
        public Message() { }

        public Message(int id, User author, string content, long[] media, long theme, DateTime creationTime , List<Vote> votes)
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

        public User Author { get; set; }
        
        public string Content { get; set; }

        public long[] Media { get; set; }
        
        public long Theme { set; get; }

        public List<Vote> Votes { get; set; }

    }
}