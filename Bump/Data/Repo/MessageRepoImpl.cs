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

        public void UpdateMessage(Message message) => _local.UpdateMessage(message);
    }
}