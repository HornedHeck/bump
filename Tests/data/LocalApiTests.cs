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
        private readonly ILocalApi _local = new EntityLocal();
        private const string Name = "Name";
        private const string Password = "Password";
        private const string WrongPassword = "WrongPassword";
        private const string Login = "Login";


        public LocalApiTests()
        {
            _user = new User(
                id: 0,
                name: Name,
                login: Login
            );
            _theme = new Theme(
                0,
                _user,
                Name,
                "Content",
                new Message[0],
                new int[0]
            );
            _message = new Message(
                id: 0,
                author: _user,
                content: "Content",
                media: new int[0],
                theme: 0
            );
        }

        private readonly User _user;
        private readonly Message _message;
        private readonly Theme _theme;

        private void RegisterWithAssert()
        {
            _local.Register(Login, Password, Name);
            Assert.NotNull(_local.GetCurrentUser());
        }

        private void LogoutWithAssert()
        {
            _local.Logout();
            Assert.Null(_local.GetCurrentUser());
        }

        [SetUp]
        public void SetUp()
        {
            _local.ResetDatabase();
        }

        // [Test]
        public void RegisterTest()
        {
            RegisterWithAssert();
            Assert.NotNull(_local.GetCurrentUser());
            Assert.AreEqual(Login, _local.GetCurrentUser().Login);
            Assert.AreEqual(Name, _local.GetCurrentUser().Name);
        }

        // [Test]
        public void LogoutTest()
        {
            RegisterWithAssert();
            LogoutWithAssert();
        }

        // [Test]
        public void LoginTest_Successful()
        {
            RegisterWithAssert();
            LogoutWithAssert();
            _local.Login(Login, Password);
            Assert.NotNull(_local.GetCurrentUser());
            Assert.AreEqual(Login, _local.GetCurrentUser().Login);
            Assert.AreEqual(Name, _local.GetCurrentUser().Name);
        }

        // [Test]
        public void LoginTest_Fail()
        {
            RegisterWithAssert();
            LogoutWithAssert();
            // _local.Login();
            Assert.Null(_local.GetCurrentUser());
        }

        [Test]
        public void CreateThemeTest()
        {
            _local.CreateTheme(_theme);

            Assert.Contains(_theme.Name, _local.GetThemeHeaders().Select(it => it.Name).ToArray());
            Assert.NotNull(_local.GetTheme(_theme.Id));
        }

        [Test]
        public void CreateMessageTest()
        {
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

            _local.CreateMessage(_message);
            _local.UpdateMessage(updated);

            var res = _local.GetMessage(updated.Id);

            Assert.AreEqual(updated, res);
        }

        [Test]
        public void DeleteMessageTest()
        {
            _local.CreateMessage(_message);
            var createRes = _local.GetMessage(_message.Id);
            _local.DeleteMessage(_message.Id);
            var deleteRes = _local.GetMessage(_message.Id);

            Assert.NotNull(createRes);
            Assert.Null(deleteRes);
        }
    }
}