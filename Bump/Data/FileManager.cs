using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Threading.Tasks;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Bump.Data
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

        private readonly IMediaRepo _repo;

        private readonly IWebHostEnvironment _environment;

        private const string Prefix = "/files/";

        public FileManager(IMediaRepo repo, IWebHostEnvironment environment)
        {
            _repo = repo;
            _environment = environment;
        }

        public static string GetPath(Media media) => $"{Prefix}{media.Id}/{media.Name}";

        public static string GetFolder(Media media) => $"{Prefix}{media.Id}";
        
        public async Task SaveFile(IFormFile file)
        {
            var media = new Media
            {
                Name = file.Name
            };

            var postfix = file.Name.Substring(file.Name.LastIndexOf(".", StringComparison.Ordinal) + 1);
            if (ImagePostfixes.Contains(postfix))
            {
                media.Type = MediaType.Image;
            }
            else
            {
                media.Type = MediaType.File;
            }

            _repo.AddMedia(media);
            await using var fileStream = new FileStream(_environment.WebRootPath + GetPath(media), FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}