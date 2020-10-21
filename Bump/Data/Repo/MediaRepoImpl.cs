using Data;
using Data.Repo;
using Entities;

namespace Bump.Data.Repo
{
    public class MediaRepoImpl : IMediaRepo
    {
        private readonly ILocalApi _local;

        public MediaRepoImpl(ILocalApi local)
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