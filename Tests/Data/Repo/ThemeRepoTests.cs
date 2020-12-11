using System.Collections.Generic;
using Data.Api.Live;
using Data.Api.Local;
using Data.Repo;
using Entities;
using Moq;
using NUnit.Framework;
using static Tests.Utils.TestObjectsFactory;

namespace Tests.Data.Repo
{
    public class ThemeRepoTests
    {
        private readonly Mock<ILiveUpdate> Live = new Mock<ILiveUpdate>();

        private readonly Mock<ILocalApi> LocalApi = new Mock<ILocalApi>();

        private ThemeRepo Repo;

        [SetUp]
        public void SetUp()
        {
            Repo = new ThemeRepo(LocalApi.Object, Live.Object);
        }

        [Test]
        public void CreateThemeTest()
        {
            var Theme = new Theme();

            Repo.CreateTheme(Theme);

            LocalApi.Verify(api => api.CreateTheme(Theme));
            Live.Verify(live => live.NotifyThemeCreated(Theme));
        }

        [Test]
        public void GetThemeTest()
        {
            const int ThemeId = 1;

            Repo.GetTheme(ThemeId);

            LocalApi.Verify(api => api.GetTheme(ThemeId));
        }

        [Test]
        public void GetThemesTest()
        {
            const int SubcategoryId = 1;

            Repo.GetThemes(SubcategoryId);

            LocalApi.Verify(api => api.GetThemes(SubcategoryId));
        }

        [Test]
        public void GetCategoriesTest()
        {
            Repo.GetCategories();

            LocalApi.Verify(api => api.GetCategories());
        }

        [Test]
        public void GetSubcategoriesTest()
        {
            Repo.GetSubcategories(1);

            LocalApi.Verify(api => api.GetSubcategories(1));
        }

        [Test]
        public void UpdateThemeTest()
        {
            const int ThemeId = 1;
            const string Title = "Title";
            const string Content = "Content";
            var Media = new[] {1L};

            Repo.UpdateTheme(ThemeId, Title, Content, Media);

            LocalApi.Verify(api => api.UpdateTheme(ThemeId, Title, Content, Media));
        }

        [Test]
        public void CreateCategoryTest()
        {
            var category = CategoryEntity;
            LocalApi.SetupSequence(api => api.GetCategories())
                .Returns(new List<ThemeCategory>())
                .Returns(new List<ThemeCategory>
                {
                    category
                });

            Repo.CreateCategory(category.Name);
            Repo.CreateCategory(category.Name);

            LocalApi.Verify(api => api.AddCategory(It.IsAny<ThemeCategory>()), Times.Once);
        }

        [Test]
        public void CreateSubcategoryTest()
        {
            var subcategory = SubcategoryEntity;

            LocalApi.Setup(api => api.GetCategories())
                .Returns(new List<ThemeCategory> {subcategory.Category});

            LocalApi.SetupSequence(api => api.GetSubcategories(subcategory.Category.Id))
                .Returns(new List<ThemeSubcategory>())
                .Returns(new List<ThemeSubcategory>
                {
                    subcategory
                });

            Repo.CreateSubcategory(subcategory.Name, subcategory.Category.Id);
            Repo.CreateSubcategory(subcategory.Name, subcategory.Category.Id);

            LocalApi.Verify(api => api.AddSubcategory(It.IsAny<ThemeSubcategory>()), Times.Once);
        }
    }
}