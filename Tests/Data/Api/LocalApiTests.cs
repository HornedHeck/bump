using System.IO;
using System.Linq;
using Data.Api.Local;
using Entities;
using NUnit.Framework;

namespace Tests.Data.Api {

    public class LocalApiTests {

        private ILocalApi _local;

        [SetUp]
        public void SetUp() {
            File.Delete( "Tests.db" );
            _local = new EntityLocal( "Tests.db" );
            TestInitializer.InitDbContent( _local );
        }

        [Test]
        public void MessageReadSuccess() {
            var message = _local.GetMessage( 1 );

            Assert.NotNull( message );
        }

        [Test]
        public void MessageReadFail() {
            var message = _local.GetMessage( -1 );

            Assert.Null( message );
        }

        [Test]
        public void UpdateMessageSuccess() {
            const string NewContent = "New content";

            var old = _local.GetMessage( 1 );
            old.Content = NewContent;
            _local.UpdateMessage( old );
            var updated = _local.GetMessage( 1 );

            Assert.AreEqual( NewContent , updated.Content );
        }

        [Test]
        public void DeleteMessageSuccess() {
            var message = _local.GetMessage( 1 );
            _local.DeleteMessage( 1 );
            var deleted = _local.GetMessage( 1 );

            Assert.NotNull( message );
            Assert.Null( deleted );
        }

        [Test]
        public void ThemeReadSuccess() {
            var theme = _local.GetTheme( 1 );

            Assert.NotNull( theme );
        }

        [Test]
        public void CategoriesListSuccess() {
            var categories = _local.GetCategories();

            Assert.NotNull( categories );
            Assert.IsNotEmpty( categories );
        }

        [Test]
        public void CategoryReadTest() {
            var subcategories = _local.GetSubcategories( 1 );

            Assert.NotNull( subcategories );
            Assert.IsNotEmpty( subcategories );
        }

        [Test]
        public void SubcategoryReadSuccess() {
            var themes = _local.GetThemes( 1 );

            Assert.NotNull( themes );
            Assert.IsNotEmpty( themes );
        }

        [Test]
        public void GetMediaSuccess() {
            var media = _local.GetMedia( 1 );

            Assert.NotNull( media );
        }

        [Test]
        public void RemoveMediaSuccess() {
            var media = _local.GetMedia( 1 );
            _local.RemoveMedia( 1 );
            var deleted = _local.GetMedia( 1 );

            Assert.NotNull( media );
            Assert.Null( deleted );
        }

        [Test]
        public void VoteUpSuccess() {
            var vote = new Vote {
                UserId = "1"
            };

            var message = _local.GetMessage( 1 );
            _local.VoteUp( 1 , vote );
            var voted = _local.GetMessage( 1 );
            _local.VoteUp( 1 , vote );
            var doubleVoted = _local.GetMessage( 1 );

            Assert.IsEmpty( message.Votes );
            Assert.AreEqual( 1 , voted.Votes.Count );
            Assert.AreEqual( 1 , doubleVoted.Votes.Count );
        }

        [Test]
        public void UpdateThemeSuccess() {
            const string NewContent = "New Content";
            const string NewTitle = "New Title";

            var theme = _local.GetTheme( 1 );
            _local.UpdateTheme( 1 , NewTitle , NewContent , theme.Media.Append( 1 ) );
            var updated = _local.GetTheme( 1 );

            Assert.NotNull( theme );
            Assert.AreEqual( NewTitle , updated.Name );
            Assert.AreEqual( NewContent , updated.Content );
            Assert.IsNotEmpty( updated.Media );
        }

    }

}