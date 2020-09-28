namespace Entities
{
    public class Message
    {
        public Message() { }

        public Message(int id, User author, string content, int[] media, int theme)
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

        public int[] Media { get; set; }
        
        public int Theme { set; get; }

    }
}