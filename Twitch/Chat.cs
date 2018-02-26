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
            client = new TwitchClient(credentials, Config.GetTwitchChannelName(),logging: false);

            client.OnConnected += Client_OnConnected;

            client.Connect();
        }

        public void Disconnect() => client.Disconnect();

        private void Client_OnConnected(object sender, OnConnectedArgs e) => Log.Console.Log(Log.Category.Twitch, "Connected to the Twitch Chat.");

        public void SendMessage(string message)
        {
            client.SendMessage(message);
            Log.Console.Log(Log.Category.Twitch, $"Sent message: \"{message}\"");
        }
    }
}