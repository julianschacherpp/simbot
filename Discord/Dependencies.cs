using DSharpPlus;
using simbot;

namespace simbot.Discord
{
    public class TwitchCommunicationDependencies
    {
        internal Twitch.Client Client { get; set; }
    }

    public class TwitchInfoDependencies
    {
        internal StreamerLiveNotificationUsers StreamerLiveNotificationUsers { get; set; }
        internal Twitch.Api Api { get; set; }
    }

    public class TwitchCommandManagementDependencies
    {
        internal Twitch.Client Client { get; set; }
        internal Config.Config Config { get; set; }
    }
}