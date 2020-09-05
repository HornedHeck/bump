using System.Collections.Generic;
using Entities;

namespace Data.Repo
{
    public interface IMediaRepo
    {
        Media LoadMedia(int id);

        IEnumerable<Media> LoadMedia(IEnumerable<int> ids);
    }
}