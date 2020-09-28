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
            for (var i = 0; i < 5; i++)
            {
                var theme = new Theme(
                    id: 0,
                    author: users[random.Next(users.Count)],
                    name: $"Theme {i + 1}",
                    content: $"Content of theme {i + 1}",
                    messages: new Message[0],
                    media: new int[0]
                );
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