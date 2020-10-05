using System;
using Bump.Models;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bump.Components
{
    public class Media : ViewComponent
    {
        private readonly IMediaRepo _repo;

        public Media(IMediaRepo repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke(long id)
        {
            var media = _repo.GetMedia(id);
            return media.Type switch
            {
                MediaType.Image => View("Image", media.ToVm()),
                MediaType.Video => null,
                MediaType.File => View("File", media),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}