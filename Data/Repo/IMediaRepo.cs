﻿using Entities;

namespace Data.Repo {

    public interface IMediaRepo {

        Media GetMedia(long id);

        void AddMedia(Media media);

        void RemoveMedia(long id);

    }

}