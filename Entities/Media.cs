namespace Entities
{
    public class Media
    {
        public Media(int id, string type, string name, byte[] data)
        {
            Id = id;
            Type = type;
            Name = name;
            Data = data;
        }

        public int Id { get; }
        public string Type { get; }
        public string Name { get; }
        public byte[] Data { get; }
    }
}