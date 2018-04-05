using DSharpPlus;
using TwitchLib;

namespace simbot.Twitch
{
    public class Twitch
    {
        public Client Client { get; set; }
        public Api Api { get; set; }
        public DiscordClient DiscordClient { get; set; }

        public Twitch(Config.Config config, DiscordClient discordClient)
        {
            DiscordClient = discordClient;
            Api = new Api(config);
            Client = new Client(discordClient, Api, config);
            Client.Connect();
        }
    }
}