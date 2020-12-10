using Data.Api.Live;
using Entities;
using Microsoft.AspNetCore.SignalR;

namespace Bump.Services.LiveUpdate {

    public class SignalRLive : ILiveUpdate {

        public SignalRLive(
            IHubContext< ThemeHub > themeContext ,
            IHubContext< MessageHub > messageContext
        ) {
            ThemeClients = themeContext.Clients;
            MessageClients = messageContext.Clients;
        }

        private IHubClients ThemeClients { get; }

        private IHubClients MessageClients { get; }

        public void NotifyThemeCreated( Theme theme ) {
            ThemeClients
                .Group( ThemeHub.GetThemeGroup( theme.Subcategory.Id ) )
                .SendAsync( "Created" , theme.Id.ToString() );
        }

        public void NotifyMessageCreated( Message message ) {
            MessageClients
                .Group( MessageHub.GetMessageGroup( message.Theme ) )
                .SendAsync( "Created" , message.Id.ToString() );
        }

    }

}