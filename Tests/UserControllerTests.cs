using Bump.Controllers;
using Bump.Models;
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
        public void LoginPostTeswt()
        {
            var model = new LoginModel()
            {
                Login = "Login",
                Password = "Password"
            };
            _controller.ControllerContext = Utils.GetAuthContext();

            var res = _controller.Login(model);

            _userRepo.Verify(it => it.Login(model.Login, model.Password));
            Assert.IsInstanceOf<RedirectResult>(res);
            Assert.AreEqual(Utils.GenerateUrl("User", "Profile"), (res as RedirectResult)?.Url);
        }

        [Test]
        public void LoginGetTest()
        {
            var res = _controller.Login();
            Assert.NotNull(res);
        }

        [Test]
        public void RegisterPostTest()
        {
            var model = new RegistrationModel()
            {
                Login = "Login",
                Password = "Password",
                Name = "Name"
            };
            _controller.ControllerContext = Utils.GetAuthContext();

            var res = _controller.Register(model);

            _userRepo.Verify(it => it.Register(model.Login, model.Password, model.Name));
            Assert.IsInstanceOf<RedirectResult>(res);
            Assert.AreEqual(Utils.GenerateUrl("User", "Profile"), (res as RedirectResult)?.Url);
        }

        [Test]
        public void RegisterGetTest()
        {
            var res = _controller.Register();
            Assert.NotNull(res);
        }

        [Test]
        public void LogoutTest()
        {
            var res = _controller.Logout();
            _userRepo.Verify(it => it.Logout());
            Assert.AreEqual(Utils.GenerateUrl("User", "Login"), res.Url);
        }

        [Test]
        public void ProfileTestSuccess()
        {
            _userRepo
                .Setup(it => it.GetCurrentUser())
                .Returns(new User(1, "name" , "login"));

            var res = _controller.Profile();
            Assert.IsInstanceOf<ViewResult>(res);
        }
    }
}