using Bump.Controllers;
using Data.Repo;
using Entities;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class HomeControllerTests
    {
        private HomeController _controller;
        private readonly Mock<IThemeRepo> _repo = new Mock<IThemeRepo>();


        [SetUp]
        public void SetUp()
        {
            _controller = new HomeController(_repo.Object);
        }

        [Test]
        public void ThemesTest()
        {
            var res = _controller.Index();
            _repo.Verify(repo => repo.GetThemeHeaders(), Times.Once);
            Assert.NotNull(res);
        }

        [Test]
        public void ThemeTest()
        {
            var theme = new Theme(
                0,
                null,
                "",
                "",
                new Message[0],
                new int[0]
            );
            _repo.Setup(repo => repo.GetTheme(It.IsAny<int>())).Returns<int>((id) => theme);

            var res = _controller.Theme(0);

            _repo.Verify(repo => repo.GetTheme(It.IsAny<int>()), Times.Once);
            Assert.NotNull(res);
        }
    }
}