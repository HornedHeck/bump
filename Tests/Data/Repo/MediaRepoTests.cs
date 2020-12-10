using Data.Api.Local;
using Data.Repo;
using Entities;
using Moq;
using NUnit.Framework;

namespace Tests.Data.Repo {

    public class MediaRepoTests {

        private readonly Mock< ILocalApi > LocalApi = new Mock< ILocalApi >();
        private MediaRepo Repo;

        [SetUp]
        public void SetUp() {
            Repo = new MediaRepo( LocalApi.Object );
        }

        [Test]
        public void GetMediaSuccess() {
            Repo.GetMedia( 1 );

            LocalApi.Verify( api => api.GetMedia( 1 ) );
        }

        [Test]
        public void AddMediaSuccess() {
            var media = new Media();

            Repo.AddMedia( media );

            LocalApi.Verify( api => api.AddMedia( media ) );
        }

        [Test]
        public void RemoveMediaSuccess() {
            Repo.RemoveMedia( 1 );

            LocalApi.Verify( api => api.RemoveMedia( 1 ) );
        }

    }

}