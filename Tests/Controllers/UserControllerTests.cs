using System.Threading.Tasks;
using Bump.Auth;
using Bump.Controllers;
using Data.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepo> _repo = new Mock<IUserRepo>();
        private readonly UserManager<BumpUser> _userManager = new MockUserManager();
        private readonly SignInManager<BumpUser> _signInManager;
        private UserController _userController;

        public UserControllerTests()
        {
            _signInManager = new MockSignInManager(_userManager);
        }

        [SetUp]
        public void SetUp()
        {
            _userController = new UserController(_userManager);
        }

        [Test]
        public async Task LoginViewTest()
        {
            var res = await _userController.Login("");

            Assert.IsInstanceOf<ViewResult>(res);
        }

        [Test]
        public void RegisterViewTest()
        {
            var res = _userController.Register();

            Assert.IsInstanceOf<ViewResult>(res);
        }

        // [Test]
        public async Task UserViewTest()
        {
            _userController.ControllerContext = Utils.Utils.GetAuthContext();
            var res = await _userController.Profile();

            Assert.IsInstanceOf<ViewResult>(res);
        }
    }
}