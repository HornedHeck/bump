using Data;
using Data.Repo;
using Entities;

namespace Bump.Data.Repo
{
    public class MessageRepoImpl : IMessageRepo
    {
        private readonly ILocalApi _local;

        public MessageRepoImpl(ILocalApi local)
        {
            _local = local;
        }

        public void CreateMessage(Message message) => _local.CreateMessage(message);

        public void DeleteMessage(int id) => _local.DeleteMessage(id);

        public void UpdateMessage(int id, string content, long[] media) =>
            _local.GetMessage(id)?.Also(message =>
            {
                message.Content = content;
                message.Media = media;
                _local.UpdateMessage(message);
            });

        public Message GetMessage(int id)
        {
            return _local.GetMessage(id);
        }
    }
}