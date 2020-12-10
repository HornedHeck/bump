using Common;
using Data.Api.Live;
using Data.Api.Local;
using Entities;

namespace Data.Repo
{
    public class MessageRepo
    {
        private readonly ILocalApi _local;
        private readonly ILiveUpdate _live;

        public MessageRepo(ILocalApi local, ILiveUpdate live)
        {
            _local = local;
            _live = live;
        }

        public void CreateMessage(Message message)
        {
            _local.CreateMessage(message);
            _live.NotifyMessageCreated(message);
        }

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

        public void VoteUp(int message, Vote vote)
        {
            _local.VoteUp(message, vote);
        }
    }
}