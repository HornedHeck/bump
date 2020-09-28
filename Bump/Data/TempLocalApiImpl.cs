using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entities;

namespace Bump.Data
{
    public class TempLocalApiImpl : ILocalApi
    {
        public void CreateTheme(Theme theme)
        {
            
        }

        private readonly List<User> _users = new List<User>
        {
            new User("1")
        };

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        private readonly List<Message> _messages = new List<Message>();

        public TempLocalApiImpl()
        {
            var rand = new Random();

            for (var i = 0; i < 100; i++)
            {
                _messages.Add(new Message(
                    id: i,
                    author: _users.First(),
                    content: $"Test message {i}",
                    media: new int[0],
                    theme: rand.Next(1, 4)
                ));
            }
        }

        public Media LoadMedia(int id)
        {
            return null;
        }

        public Theme GetTheme(int id)
        {
            return new Theme(
                id: id,
                author: _users.First(),
                name: $"Test theme {id}",
                content: $"Test content {id}",
                messages: _messages.Where(it => it.Theme == id).ToArray(),
                media: new int[0]
            );
        }

        public List<ThemeHeader> GetThemeHeaders()
        {
            return new[]
            {
                new ThemeHeader(1, "Test theme header 1", _users.First()),
                new ThemeHeader(2, "Test theme header 2", _users.First()),
                new ThemeHeader(3, "Test theme header 3", _users.First()),
            }.ToList();
        }

        public void CreateMessage(Message message)
        {
            _messages.Add(message);
        }

        public void UpdateMessage(Message message) => _messages
            .Find(it => it.Id == message.Id)
            ?.Also(it =>
            {
                it.Content = message.Content;
                it.Media = message.Media;
            });

        public void DeleteMessage(int id) => _messages
            .Find(it => it.Id == id)
            ?.Run(it => _messages.Remove(it));

        public Message GetMessage(int id)
        {
            return _messages.Find(it => it.Id == id);
        }

        public void ResetDatabase()
        {
            throw new NotImplementedException();
        }
    }
}