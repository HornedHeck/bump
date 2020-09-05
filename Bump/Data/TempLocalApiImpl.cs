using System.Collections.Generic;
using System.Linq;
using Data;
using Entities;

namespace Bump.Data
{
    public class TempLocalApiImpl : ILocalApi
    {
        private User _user;

        private readonly List<Message> _messages = new List<Message>();

        public TempLocalApiImpl()
        {
            for (var i = 0; i < 10; i++)
            {
                _messages.Add(new Message(
                    id: i,
                    author: _user,
                    content: $"Test message {i}",
                    media: new int[0],
                    theme: i / 3 + 1
                ));
            }
        }

        public User GetCurrentUser() => _user;

        public void Logout()
        {
            _user = null;
        }

        public void Login()
        {
            _user = new User(1, "Test user");
        }

        public Media LoadMedia(int id)
        {
            return null;
        }

        public Theme GetTheme(int id)
        {
            return new Theme(
                id: id,
                author: _user,
                name: $"Test theme {id}",
                content: $"Test content {id}",
                messages: _messages.ToArray(),
                media: new int[0]
            );
        }

        public List<ThemeHeader> GetThemeHeaders()
        {
            return new[]
            {
                new ThemeHeader(1, "Test theme header 1", _user),
                new ThemeHeader(2, "Test theme header 2", _user),
                new ThemeHeader(3, "Test theme header 3", _user),
            }.ToList();
        }

        public void CreateMessage(Message message)
        {
            _messages.Add(message);
        }

        public void UpdateMessage(Message message) => _messages
            .Find(it => it.Id == message.Id)
            ?.Run(it =>
            {
                it.Content = message.Content;
                it.Media = message.Media;
            });

        public void DeleteMessage(int id) => _messages
            .Find(it => it.Id == id)
            ?.Run(it => _messages.Remove(it));
    }
}