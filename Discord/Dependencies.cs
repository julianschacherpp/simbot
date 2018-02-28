using DSharpPlus;
using simbot;

namespace simbot.Discord
{
    public class Dependencies
    {
        internal DiscordClient DiscordClient { get; set; }
        internal Config.Config Config { get; set; }
    }
}