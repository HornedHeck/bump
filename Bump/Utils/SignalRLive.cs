using System.Text.Json;
using Data.Api.Live;
using Entities;
using Microsoft.AspNetCore.SignalR;

namespace Bump.Utils
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SignalRLive : Hub, ILiveUpdate
    {
        public void NotifyThemeCreated(Theme theme)
        {
            SendMessage(GetThemeGroup(theme), JsonSerializer.Serialize(theme));
        }

        public void NotifyMessageCreated(Message message)
        {
            SendMessage(GetMessageGroup(message), JsonSerializer.Serialize(message));
        }

        private async void SendMessage(string name, string message)
        {
            await Clients.Group(name).SendAsync("Created", message);
        }

        public static string GetMessageGroup(Message message)
        {
            return $"messages-{message.Theme}";
        }

        public static string GetThemeGroup(Theme theme)
        {
            return $"themes-{theme.Subcategory.Id}";
        }
    }
}