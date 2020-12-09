using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Bump.Features.LiveUpdate
{
    public class MessageHub : Hub
    {
        internal static string GetMessageGroup(long theme)
        {
            return $"messages-{theme}";
        }

        private long GetThemeId()
        {
            var http = Context.GetHttpContext();
            var value = http.Request.Query["t_id"];
            return long.Parse(value);
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, GetMessageGroup(GetThemeId()));
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, GetMessageGroup(GetThemeId()));
            return base.OnDisconnectedAsync(exception);
        }
    }
}