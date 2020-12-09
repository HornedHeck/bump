using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Bump
{
    public class FileManager
    {
        private static readonly ISet<string> ImagePostfixes = new HashSet<string>
        {
            "jpg",
            "jpeg",
            "gif",
            "png",
            "apng",
            "svg",
            "svgz",
            "bmp",
            "rle",
            "dib",
            "ico"
        };

        private readonly MediaRepo _repo;

        private readonly IWebHostEnvironment _environment;

        private const string Prefix = "/files/";

        public FileManager(MediaRepo repo, IWebHostEnvironment environment)
        {
            _repo = repo;
            _environment = environment;
        }

        public static string GetPath(Media media) => $"{Prefix}{media.Id}/{media.Name}";

        public static string GetFolder(Media media) => $"{Prefix}{media.Id}";

        public async Task<long> SaveFile(IFormFile file)
        {
            var media = new Media
            {
                Name = file.FileName
            };

            var filename = file.FileName;
            var postfix = filename.Substring(
                filename.LastIndexOf(".", StringComparison.Ordinal) + 1
            );

            media.Type = ImagePostfixes.Contains(postfix) ? MediaType.Image : MediaType.File;

            _repo.AddMedia(media);
            Directory.CreateDirectory(_environment.WebRootPath + GetFolder(media));
            await using var fileStream = new FileStream(_environment.WebRootPath + GetPath(media), FileMode.Create);
            await file.CopyToAsync(fileStream);
            return media.Id;
        }

        public void RemoveMedia(long id)
        {
            var media = _repo.GetMedia(id);
            File.Delete(_environment.WebRootPath + GetPath(media));
            Directory.Delete(_environment.WebRootPath + GetFolder(media));
            _repo.RemoveMedia(id);
        }
    }
}