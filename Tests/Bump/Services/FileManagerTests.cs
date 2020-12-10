using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Bump.Services;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace Tests.Bump.Services {

    public class FileManagerTests {

        private FileManager FileManager;
        private readonly Mock< IMediaRepo > Repo = new Mock< IMediaRepo >();

        private readonly Mock< IWebHostEnvironment > Environment = new Mock< IWebHostEnvironment >();

        [SetUp]
        public void SetUp() {
            FileManager = new FileManager( Repo.Object , Environment.Object );
        }

        [Test]
        public void GetPathTest() {
            var Media = new Media {
                Id = 1 ,
                Name = "Name"
            };

            var path = FileManager.GetPath( Media );

            Assert.AreEqual( $"/files/{Media.Id}/{Media.Name}" , path );
        }

        [Test]
        public void GetFolderTest() {
            var Media = new Media {
                Id = 1 ,
                Name = "Name"
            };

            var path = FileManager.GetFolder( Media );

            Assert.AreEqual( $"/files/{Media.Id}" , path );
        }

        [Test]
        public void SaveFileTest() {
            var File = new Mock< IFormFile >();
            File
                .SetupGet( file => file.FileName )
                .Returns( "Name" );

            FileManager.SaveFile( File.Object );

            File.Verify(
                file => file.CopyToAsync(
                    It.IsAny< Stream >() ,
                    It.IsAny< CancellationToken >()
                ) );

            Repo.Verify( repo => repo.AddMedia( It.IsAny< Media >() ) );
        }

        [Test]
        public async Task DeleteMediaTest() {
            const string FileName = "Name";
            var Media = new Media {
                Id = 0 ,
                Name = FileName
            };
            var FormFile = new Mock< IFormFile >();
            FormFile
                .SetupGet( file => file.FileName )
                .Returns( FileName );

            Repo
                .Setup( repo => repo.GetMedia( 0 ) )
                .Returns( Media );

            Environment
                .Setup( env => env.WebRootPath )
                .Returns( "./" );

            FileManager = new FileManager( Repo.Object , Environment.Object );

            await FileManager.SaveFile( FormFile.Object );
            FileManager.RemoveMedia( Media.Id );

            Assert.That( !File.Exists( "./" + FileManager.GetPath( Media ) ) );
            Assert.That( !File.Exists( "./" + FileManager.GetPath( Media ) ) );
            Repo.Verify( repo => repo.RemoveMedia( Media.Id ) );
        }


    }

}