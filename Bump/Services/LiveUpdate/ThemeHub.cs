using System;
using System.Threading.Tasks;
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
}