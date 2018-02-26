using System;
using System.Threading;
using TwitchLib;
using TwitchLib.Events.Client;
using TwitchLib.Models.Client;

namespace simbot.Twitch
{
    public class Chat
    {
        private readonly ConnectionCredentials credentials;
        private TwitchClient client;

        public Chat()
        {
            credentials = new ConnectionCredentials(
                twitchUsername: Config.GetTwitchUsername(),
                twitchOAuth: Config.GetTwitchAccessToken()
            );
        }

        public void Connect()
        {
            client = new TwitchClient(credentials, Config.GetTwitchChannelName(),logging: true);

            client.Connect();
        }

        public void Disconnect() => client.Disconnect();

        public void SendMessage(string message)
        {
            client.SendMessage(message);
        }
    }
}