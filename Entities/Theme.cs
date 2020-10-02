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

        public int Id { get; set; }

        public User Author { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public Message[] Messages { get; set; }

        public int[] Media { get; set; }

        public ThemeSubcategory Subcategory { get; set; }

        public ThemeCategory Category => Subcategory?.Category;
    }
}