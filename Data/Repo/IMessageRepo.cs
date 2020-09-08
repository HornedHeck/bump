using Entities;

namespace Data.Repo
{
    public interface IMessageRepo
    {
        void CreateMessage(Message message);

        void DeleteMessage(int id);

        void UpdateMessage(int id, string content, int[] media);

        Message GetMessage(int id);
    }
}