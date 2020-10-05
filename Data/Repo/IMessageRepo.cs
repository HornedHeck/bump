using Entities;

namespace Data.Repo
{
    public interface IMessageRepo
    {
        void CreateMessage(Message message);

        void DeleteMessage(int id);

        void UpdateMessage(int id, string content, long[] media);

        Message GetMessage(int id);
    }
}