namespace Entities
{
    public class Message
    {
        public Message(int id, User author, string content, int[] media)
        {
            Id = id;
            Author = author;
            Content = content;
            Media = media;
        }

        public int Id { get; }

        public User Author { get; }
        
        public string Content { get; }

        public int[] Media { get; }

    }
}