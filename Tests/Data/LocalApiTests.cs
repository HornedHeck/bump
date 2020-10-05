using System.Linq;
using Bump.Data;
using Data;
using Entities;
using Moq;
using NUnit.Framework;

namespace Tests.data
{
    public class LocalApiTests
    {
        private readonly ILocalApi _local = new EntityLocal("Tests.db");
        private const string Name = "Name";
        private const string Content = "Content";

        private Message _message;
        private Theme _theme;
        private User _user;

        [SetUp]
        public void SetUp()
        {
            _user = new User(
                id: "1"
            );
            _theme = new Theme(
                id: 1,
                author: _user,
                name: Name,
                content: Content,
                messages: new Message[0],
                media: new int[0]
            );
            _message = new Message(
                id: 1,
                author: _user,
                content: Content,
                media: new int[0],
                theme: _theme.Id
            );
            _local.ResetDatabase();
            _local.AddUser(_user);
        }

        [Test]
        public void CreateThemeTest()
        {
            _local.CreateTheme(_theme);

            Assert.NotNull(_local.GetTheme(_theme.Id));
        }

        [Test]
        public void CreateMessageTest()
        {
            _local.CreateTheme(_theme);
            _message.Theme = _theme.Id;
            _local.CreateMessage(_message);
            var result = _local.GetMessage(_message.Id);

            Assert.NotNull(result);
            Assert.AreEqual(_message.Content, result.Content);
        }

        [Test]
        public void UpdateMessageTest()
        {
            var updated = new Message(_message.Id, _message.Author, _message.Content, _message.Media, _message.Theme)
            {
                Content = "Content2"
            };

            _local.CreateTheme(_theme);
            _message.Theme = _theme.Id;
            updated.Theme = _theme.Id;
            _local.CreateMessage(_message);
            updated.Id = _message.Id;
            _local.UpdateMessage(updated);

            var res = _local.GetMessage(updated.Id);

            Assert.AreEqual(updated.Content, res.Content);
        }

        [Test]
        public void DeleteMessageTest()
        {
            _local.CreateTheme(_theme);
            _message.Theme = _theme.Id;
            _local.CreateMessage(_message);
            var createRes = _local.GetMessage(_message.Id);
            _local.DeleteMessage(_message.Id);
            var deleteRes = _local.GetMessage(_message.Id);

            Assert.NotNull(createRes);
            Assert.Null(deleteRes);
        }

        [Test]
        public void GetMessageTest()
        {
            _local.CreateTheme(_theme);
            _message.Theme = _theme.Id;
            _local.CreateMessage(_message);

            var r1 = _local.GetMessage(_message.Id);
            var r2 = _local.GetMessage(_message.Id);
            
            Assert.NotNull(r1);
            Assert.NotNull(r2);
        }
        
    }
}