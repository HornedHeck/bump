using Entities;
using Microsoft.AspNetCore.SignalR;

namespace Data.Api.Live
{
    internal class SignalRLive : ILiveUpdate
    {
        public void NotifyThemeCreated(Theme theme)
        {
            throw new System.NotImplementedException();
        }

        public void NotifyMessageCreated(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}