using System.Collections.Generic;
using Entities;

namespace Data.Repo
{
    public interface IMediaRepo
    {
        Media GetMedia(long id);

        IEnumerable<Media> GetMedia(IEnumerable<long> ids);
    }
}