using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Bump.Auth;
using Common;
using Moq;
using NUnit.Framework;

namespace Tests.Bump.Auth {

    public class AuthExtensionsTests {

        public static object[] VisibleNameTestData = {
            new object[] {"Claim" , null , "Claim"} ,
            new object[] {null , "Name" , "Name"} ,
            new object[] {null , null , string.Empty}
        };

        [Test]
        [TestCaseSource( nameof( VisibleNameTestData ) )]
        public void VisibleNameTests( string claim , string name , string expected ) {
            var Claims = new List< Claim >();
            if( claim != null ) {
                Claims.Add( new Claim( ClaimTypes.GivenName , claim ) );
            }

            var Identity = new ClaimsIdentity( new Mock< IIdentity >().Also( mock => {
                mock
                    .SetupGet( i => i.Name )
                    .Returns( name );
            } ).Object , Claims );

            var res = Identity.VisibleName();

            Assert.AreEqual( expected , res );
        }

    }

}