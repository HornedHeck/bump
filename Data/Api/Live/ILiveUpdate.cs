using Entities;

namespace Data.Api.Live
{
    public interface ILiveUpdate
    {
        void NotifyThemeCreated(Theme theme);

        void NotifyMessageCreated(Message message);

    }
}