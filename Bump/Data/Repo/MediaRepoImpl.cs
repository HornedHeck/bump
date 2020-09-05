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

        public Media LoadMedia(int id) => _local.LoadMedia(id);

        public IEnumerable<Media> LoadMedia(IEnumerable<int> ids)
        {
            return ids.Select(id => _local.LoadMedia(id));
        }
    }
}