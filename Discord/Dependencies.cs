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
        internal Twitch.Api Api { get; set; }
    }
}