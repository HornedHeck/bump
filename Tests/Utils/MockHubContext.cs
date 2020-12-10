using Microsoft.AspNetCore.SignalR;
using Moq;

namespace Tests.Utils {

    public class MockHubContext< T > : IHubContext< T > where T : Hub {

        public MockHubContext( IMock< IHubClients > Clients ) {
            this.Clients = Clients.Object;
        }

        public IHubClients Clients { get; }

        public IGroupManager Groups { get; } = null;

    }

}