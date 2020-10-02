using System;
using System.Linq;
using Bump.Auth;
using Data;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace Bump.Data
{
    public class TestInitializer
    {
        public static void Initialize(ILocalApi local, UserManager<BumpUser> userManager)
        {
            local.ResetDatabase();

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
            for (var i = 0; i < 5; i++)
            {
                var theme = new Theme(
                    id: 0,
                    author: users[random.Next(users.Count)],
                    name: $"Theme {i + 1}",
                    content: $"Content of theme {i + 1}",
                    messages: new Message[0],
                    media: new int[0]
                )
                {
                    Subcategory = subcategory
                };
                local.CreateTheme(theme);
                for (var j = 0; j < 10; j++)
                {
                    var message = new Message(
                        id: 0,
                        author: users[random.Next(users.Count)],
                        content: $"Content of message {j + 1}",
                        media: new int[0],
                        theme: theme.Id
                    );
                    local.CreateMessage(message);
                }
            }
        }
    }
}