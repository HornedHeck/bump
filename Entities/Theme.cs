using System.Diagnostics.CodeAnalysis;

namespace Entities
{
    public class Theme
    {
        public Theme(int id, User author, string name, string content, Message[] messages, int[] media)
        {
            Id = id;
            Author = author;
            Name = name;
            Content = content;
            Messages = messages;
            Media = media;
        }

        public int Id { get; }

        public User Author { get; }

        public string Name { get; }

        public string Content { get; }

        public Message[] Messages { get; }

        public int[] Media { get; }
    }
}