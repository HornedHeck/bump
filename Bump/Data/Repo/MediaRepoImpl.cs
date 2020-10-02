using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Media> GetMedia(IEnumerable<long> ids)
        {
            return ids.Select(id => _local.GetMedia(id));
        }
    }
}