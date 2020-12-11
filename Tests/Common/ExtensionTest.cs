using System;
using Entities;
using NUnit.Framework;
using static Common.Extensions;

namespace Tests.Common {

    public class ExtensionTest {

        [Test]
        [TestCase( "1" , ExpectedResult = "Not null" )]
        [TestCase( null , ExpectedResult = "Null" )]
        public string RunTests( string s ) {
            return s?.Run( str => "Not null" ) ?? Run( () => "Null" );
        }

        [Test]
        public void AlsoTest() {
            const string newContent = "New Content";

            var message = new Message(
                    0 ,
                    null ,
                    "Old Content" ,
                    new long[0] ,
                    0 ,
                    DateTime.Now ,
                    null
                )
                .Also( it => { it.Content = newContent; } );

            Assert.AreEqual( message.Content , newContent );
        }

        [Test]
        public void DateFormatTest() {
            var Date = new DateTime( 2020 , 01 , 01 , 01 , 10 , 10 );
            const string Pattern = "{0} {1}";

            var res = Date.Format( Pattern );
            
            Assert.AreEqual( "01.01.2020 01:10" , res );
        }

    }

}