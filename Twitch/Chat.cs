using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using DSharpPlus;
using DSharpPlus.Entities;
using TwitchLib;
using TwitchLib.Events.Client;
using TwitchLib.Models.Client;

namespace simbot.Twitch
{
    public class Chat
    {
        private Config.Config config;
        private readonly ConnectionCredentials credentials;
        private TwitchClient twitchClient;
        private DiscordClient discordClient;
        public NotificationUsers notificationUsers { get; set; }

        public Chat(DiscordClient discordClient, Config.Config config)
        {
            this.config = config;
            credentials = new ConnectionCredentials(
                twitchUsername: config.Twitch.Username,
                twitchOAuth: config.Twitch.AccessToken
            );
            this.discordClient = discordClient;
            notificationUsers = new NotificationUsers();
        }

        public void Connect()
        {
            twitchClient = new TwitchClient(credentials, config.Twitch.ChannelName,logging: false);

            twitchClient.OnConnected += Client_OnConnected;
            twitchClient.OnMessageReceived += Client_OnMessageReceivedAsync;

            twitchClient.Connect();
        }

        public void Disconnect() => twitchClient.Disconnect();

        private void Client_OnConnected(object sender, OnConnectedArgs e) => Log.Console.Log(Log.Category.Twitch, "Connected to the Twitch Chat.");

        private async void Client_OnMessageReceivedAsync(object sender, OnMessageReceivedArgs e)
        {
            if (!e.ChatMessage.Message.ToLower().Contains(e.ChatMessage.BotUsername.ToLower()))
                return;

            DiscordChannel discordChannel;
            discordChannel = await discordClient.GetChannelAsync((ulong)config.Discord.TwitchChatChannel);

            foreach(var user in notificationUsers.Users)
            {
                if (e.ChatMessage.Message.ToLower().Contains(user.Value.Username.ToLower()) && user.Value.EnableNotifications)
                    await discordClient.SendMessageAsync(discordChannel, $"<@{user.Key}> Twitch message: \"{e.ChatMessage.Message}\" by {e.ChatMessage.DisplayName}");
            }
        }

        public void SendMessage(string message)
        {
            twitchClient.SendMessage(message);
            Log.Console.Log(Log.Category.Twitch, $"Sent message: \"{message}\"");
        }
    }
}