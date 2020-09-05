using Bump.Controllers;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepo> _userRepo = new Mock<IUserRepo>();
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new UserController(_userRepo.Object);
        }

        [Test]
        public void TestLogin()
        {
            var res = _controller.Login();
            _userRepo.Verify(it => it.Login());
            Assert.AreEqual(res.Url, "~/User/Profile");
        }

        [Test]
        public void TestLogout()
        {
            var res = _controller.Logout();
            _userRepo.Verify(it => it.Logout());
            Assert.AreEqual(res.Url, "~/User/Start");
        }

        [Test]
        public void ProfileTestSuccess()
        {
            _userRepo
                .Setup(it => it.GetCurrentUser())
                .Returns(new User(1, "name"));

            var res = _controller.Profile();
            Assert.IsInstanceOf<ViewResult>(res);
        }

        [Test]
        public void ProfileTestFail()
        {
            var res = _controller.Profile();
            Assert.IsInstanceOf<BadRequestResult>(res);
        }

        [Test]
        public void TestStart()
        {
            Assert.NotNull(_controller.Start());
        }
    }
}