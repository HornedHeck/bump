using System;
using System.Diagnostics.CodeAnalysis;

namespace Entities
{
    public class Theme
    {
        public Theme(long id, User author, string name, string content, Message[] messages, long[] media, DateTime creationTime)
        {
            Id = id;
            Author = author;
            Name = name;
            Content = content;
            Messages = messages;
            Media = media;
            CreationTime = creationTime;
        }

        public long Id { get; set; }

        public User Author { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public Message[] Messages { get; set; }

        public long[] Media { get; set; }

        public DateTime CreationTime { get; set; }

        public ThemeSubcategory Subcategory { get; set; }

        public ThemeCategory Category => Subcategory?.Category;
    }
}