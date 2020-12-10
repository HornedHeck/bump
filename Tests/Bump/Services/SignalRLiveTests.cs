using Bump.Services.LiveUpdate;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;
using Tests.Utils;
using static Tests.Utils.TestObjectsFactory;

namespace Tests.Bump.Services {

    public class SignalRLiveTests {

        private readonly Mock< IHubClients > MessageClients = new Mock< IHubClients >();
        private readonly Mock< IHubClients > ThemeClients = new Mock< IHubClients >();

        private SignalRLive Live;

        [SetUp]
        public void SetUp() {
            ThemeClients
                .Setup( c => c.Group( It.IsAny< string >() ) )
                .Returns( new Mock< IClientProxy >().Object );
            MessageClients
                .Setup( c => c.Group( It.IsAny< string >() ) )
                .Returns( new Mock< IClientProxy >().Object );
            
            Live = new SignalRLive(
                new MockHubContext< ThemeHub >( ThemeClients ) ,
                new MockHubContext< MessageHub >( MessageClients ) );
        }

        [Test]
        public void NotifyThemeTest() {
            Live.NotifyThemeCreated( ThemeEntity );

            ThemeClients.Verify( c => c.Group( It.IsAny< string >() ) );
        }

        [Test]
        public void NotifyMessageTest() {
            Live.NotifyMessageCreated( MessageEntity );

            MessageClients.Verify( c => c.Group( It.IsAny< string >() ) );
        }


    }

}