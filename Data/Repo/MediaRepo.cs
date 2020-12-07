using Data.Api.Local;
using Entities;

namespace Data.Repo
{
    public class MediaRepo
    {
        private readonly ILocalApi _local;

        internal MediaRepo(ILocalApi local)
        {
            _local = local;
        }

        public Media GetMedia(long id) => _local.GetMedia(id);

        public void AddMedia(Media media)
        {
            _local.AddMedia(media);
        }

        public void RemoveMedia(long id)
        {
            _local.RemoveMedia(id);
        }
    }
}