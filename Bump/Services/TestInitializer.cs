using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bump.Auth;
using Bump.Services;
using Data.Api.Local;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Bump
{
    public class TestInitializer
    {
        public static void Initialize(ILocalApi local, UserManager<BumpUser> userManager,
            IWebHostEnvironment environment)
        {
            local.ResetDatabase();
            if (Directory.Exists(environment.WebRootPath + "/files"))
            {
                Directory.Delete(environment.WebRootPath + "/files", true);
            }

            InitDbContent(local, userManager, environment);
        }

        private static void InitDbContent(ILocalApi local, UserManager<BumpUser> userManager,
            IWebHostEnvironment environment)
        {
            var users = userManager.Users
                .Select(it => new User(it.Id))
                .ToList();

            users.ForEach(local.AddUser);
            var random = new Random();
            var category = new ThemeCategory
            {
                Id = 0,
                Name = "Category"
            };
            local.AddCategory(category);
            var subcategory = new ThemeSubcategory
            {
                Id = 0,
                Name = "Subcategory",
                Category = category
            };
            local.AddSubcategory(subcategory);
            var messageCounter = 0;
            for (var i = 0; i < 5; i++)
            {
                var theme = new Theme(
                    id: i + 1,
                    author: users[random.Next(users.Count)],
                    name: $"Theme {i + 1}",
                    content: $"Content of theme {i + 1}",
                    messages: new Message[0],
                    media: new long[0],
                    creationTime: DateTime.Today,
                    subcategory: subcategory
                )
                {
                    Subcategory = subcategory
                };
                local.CreateTheme(theme);
                for (var j = 0; j < 10; j++, messageCounter++)
                {
                    var media = new Media
                    {
                        Type = MediaType.Image,
                        Name = "doc.png"
                    };
                    local.AddMedia(media);
                    var dir = environment.WebRootPath + FileManager.GetFolder(media);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    File.Copy(environment.WebRootPath + "/doc.png",
                        environment.WebRootPath + FileManager.GetPath(media));

                    var message = new Message(
                        id: 0,
                        author: users[random.Next(users.Count)],
                        content: $"Content of message {j + 1}",
                        media: new[] {media.Id},
                        theme: theme.Id,
                        creationTime: DateTime.Now.AddHours(random.NextDouble() * 3 - 1.5),
                        votes: new List<Vote>()
                    );
                    local.CreateMessage(message);
                }
            }
        }
    }
}