using Entities;

namespace Data.Api.Live
{
    internal interface ILiveUpdate
    {
        void NotifyThemeCreated(Theme theme);

        void NotifyMessageCreated(Message message);
    }
}