using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.VM;
using Common;
using Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using Tests.Utils;
using static Tests.Utils.TestObjectsFactory;

namespace Tests.Bump.Vm {

    public class MappersTest {

        [Test]
        public void MediaVmMapTest() {
            var Entity = new Media {
                Id = 1 ,
                Name = "Name" ,
                Type = MediaType.Video
            };

            var vm = Entity.ToVm();

            Assert.NotNull( vm );
            Assert.AreEqual( Entity.Name , vm.Name );
        }

        [Test]
        public async Task MessageVmMapTest() {
            var Entity = MessageEntity;
            var UserManager = new Mock< MockUserManager >();

            UserManager
                .Setup( um => um.FindByIdAsync( Entity.Author.Id ) )
                .Returns( Task.FromResult( new BumpUser {
                    Id = Entity.Author.Id
                } ) );

            var vm = await Entity.ToVm( UserManager.Object , null );

            Assert.NotNull( vm );
            Assert.AreEqual( Entity.Id , vm.Id );
            Assert.AreEqual( Entity.Content , vm.Content );
            CollectionAssert.AreEqual( Entity.Media , vm.Media.ToArray() );
            Assert.AreEqual( Entity.Theme , vm.Theme );
            Assert.AreEqual( Entity.CreationTime , vm.CreationTime );
            CollectionAssert.AreEquivalent( Entity.Votes , vm.Votes );
            Assert.AreEqual( Entity.Author.Id , vm.Author.Id );
        }

        public static object[] ThemeVmTestData = {
            new object[] {ThemeEntity} ,
            new object[] {ThemeEntity.Also( it => it.Messages = null )}
        };

        [Test , TestCaseSource( nameof( ThemeVmTestData ) )]
        public async Task ThemeVmMapTest( Theme Entity ) {
            var UserManager = new Mock< MockUserManager >();

            UserManager
                .Setup( um => um.FindByIdAsync( It.IsAny< string >() ) )
                .Returns( ( string id ) => Task.FromResult( new BumpUser {
                    Id = id
                } ) );

            var vm = await Entity.ToVm( UserManager.Object );

            Assert.AreEqual( Entity.Id , vm.Id );
            Assert.AreEqual( Entity.Author.Id , vm.Author.Id );
            Assert.AreEqual( Entity.Content , vm.Content );
            CollectionAssert.AreEqual( Entity.Media , vm.Media );
            Assert.AreEqual( Entity.Name , vm.Title );
            Assert.AreEqual( Entity.Subcategory , vm.Subcategory );
            Assert.AreEqual( Entity.CreationTime , vm.StartTime );
            if( Entity.Messages == null ) {
                Assert.NotNull( vm.Messages );
                Assert.IsEmpty( vm.Messages );
            }
            else {
                CollectionAssert.AreEqual(
                    Entity.Messages.Select( it => it.Id ) ,
                    vm.Messages.Select( it => it.Id )
                );
            }
        }

    }

}