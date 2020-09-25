using System.Data.Entity;
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
        private readonly ILocalApi _local = new TempLocalApiImpl();
        private const string Name = "Name";
        private const string Password = "Password";
        private const string WrongPassword = "WrongPassword";
        private const string Login = "Login";

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
        }

        [Test]
        public void RegisterTest()
        {
            RegisterWithAssert();
            Assert.NotNull(_local.GetCurrentUser());
            Assert.AreEqual(Login, _local.GetCurrentUser().Login);
            Assert.AreEqual(Name, _local.GetCurrentUser().Name);
        }

        [Test]
        public void LogoutTest()
        {
            RegisterWithAssert();
            LogoutWithAssert();
        }

        [Test]
        public void LoginTest_Successful()
        {
            RegisterWithAssert();
            LogoutWithAssert();
            _local.Login(Login, Password);
            Assert.NotNull(_local.GetCurrentUser());
            Assert.AreEqual(Login, _local.GetCurrentUser().Login);
            Assert.AreEqual(Name, _local.GetCurrentUser().Name);
        }

        [Test]
        public void LoginTest_Fail()
        {
            RegisterWithAssert();
            LogoutWithAssert();
            _local.Login(Login, WrongPassword);
            Assert.Null(_local.GetCurrentUser());
        }

        [Test]
        public void CreateThemeTest()
        {
            var theme = new Theme(10, new User(10, Name, Login), Name, "Content", new Message[0], new int[0]);
            _local.CreateTheme(theme);
            Assert.Contains(Name, _local.GetThemeHeaders().Select(it => it.Name).ToArray());
            Assert.NotNull(_local.GetTheme(10));
        }

        [Test]
        public void CreateMessageTest()
        {
            var message = Mock.Of<Message>();
            _local.CreateMessage(message);

            var result = _local.GetMessage(message.Id);

            Assert.NotNull(result);
            Assert.AreEqual(message.Content, result.Content);
        }

        [Test]
        public void UpdateMessageTest()
        {
            var message = Mock.Of<Message>();
            var updated = new Message(message.Id, message.Author, message.Content, message.Media, message.Theme)
            {
                Content = "Content"
            };

            _local.CreateMessage(message);
            _local.UpdateMessage(updated);
            var res = _local.GetMessage(updated.Id);

            Assert.AreEqual(updated, res);
        }

        [Test]
        public void DeleteMessageTest()
        {
            var message = Mock.Of<Message>();

            _local.CreateMessage(message);
            var createRes = _local.GetMessage(message.Id);
            _local.DeleteMessage(message.Id);
            var deleteRes = _local.GetMessage(message.Id);

            Assert.NotNull(createRes);
            Assert.Null(deleteRes);
        }
    }
}