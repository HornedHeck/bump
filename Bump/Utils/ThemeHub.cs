using System;
using System.Text.Json;
using System.Threading.Tasks;
using Data.Api.Live;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Bump.Utils
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ThemeHub : Hub
    {
        internal static string GetThemeGroup(long subcategory)
        {
            return $"themes-{subcategory}";
        }

        private long GetSubcategoryId()
        {
            var http = Context.GetHttpContext();
            var value = http.Request.Query["s_id"];
            return long.Parse(value);
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, GetThemeGroup(GetSubcategoryId()));
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, GetThemeGroup(GetSubcategoryId()));
            return base.OnDisconnectedAsync(exception);
        }
    }
    
    public class MessageHub : Hub
    {
        internal static string GetMessageGroup(long theme)
        {
            return $"messages-{theme}";
        }

        private long GetSubcategoryId()
        {
            var http = Context.GetHttpContext();
            var value = http.Request.Query["t_id"];
            return long.Parse(value);
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, GetMessageGroup(GetSubcategoryId()));
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, GetMessageGroup(GetSubcategoryId()));
            return base.OnDisconnectedAsync(exception);
        }
    }

    public class SignalRLive : ILiveUpdate
    {
        public SignalRLive(IHubContext<ThemeHub> themeContext)
        {
            Clients = themeContext.Clients;
        }

        private IHubClients Clients { get; }

        public void NotifyThemeCreated(Theme theme)
        {
            SendMessage(ThemeHub.GetThemeGroup(theme.Subcategory.Id), JsonSerializer.Serialize(theme));
        }

        public void NotifyMessageCreated(Message message)
        {
            SendMessage(MessageHub.GetMessageGroup(message.Theme), JsonSerializer.Serialize(message));
        }

        private async void SendMessage(string name, string message)
        {
            await Clients.Group(name).SendAsync("Created", message);
        }
    }
}