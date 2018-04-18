using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using DSharpPlus;
using DSharpPlus.Entities;
using TwitchLib;
using TwitchLib.Events.Client;
using TwitchLib.Models.Client;

namespace simbot.Twitch
{
    public class Client
    {
        private Config.Config config;
        private readonly ConnectionCredentials credentials;
        private TwitchClient twitchClient;
        private Api api; 
        private DiscordClient discordClient;
        public NotificationUsers NotificationUsers { get; set; }
        public DynamicCommands DynamicCommands { get; set; }
        public Client(DiscordClient discordClient, Api api, Config.Config config)
        {
            this.config = config;
            credentials = new ConnectionCredentials(
                twitchUsername: config.Twitch.Username,
                twitchOAuth: config.Twitch.AccessToken
            );
            this.discordClient = discordClient;
            this.api = api;
            NotificationUsers = new NotificationUsers();
            DynamicCommands = new DynamicCommands();
        }

        public void Connect()
        {
            twitchClient = new TwitchClient(credentials, config.Twitch.ChannelName,logging: false);
            twitchClient.AddChatCommandIdentifier('!');

            twitchClient.OnConnected += Client_OnConnected;
            twitchClient.OnMessageReceived += Client_OnMessageReceivedAsync;
            twitchClient.OnChatCommandReceived += Client_OnChatCommandReceived;

            twitchClient.Connect();
        }

        public void Disconnect() => twitchClient.Disconnect();

        public void SendMessage(string message)
        {
            twitchClient.SendMessage(message);
            Log.Console.Log(Log.Category.Twitch, $"Sent message: \"{message}\"");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e) => Log.Console.Log(Log.Category.Twitch, "Connected to the Twitch Chat.");

        private async void Client_OnMessageReceivedAsync(object sender, OnMessageReceivedArgs e)
        {
            if (!e.ChatMessage.Message.ToLower().Contains(e.ChatMessage.BotUsername.ToLower()))
                return;

            DiscordChannel discordChannel;
            discordChannel = await discordClient.GetChannelAsync((ulong)config.Discord.TwitchChatChannel);

            foreach(var user in NotificationUsers.Users)
            {
                if (e.ChatMessage.Message.ToLower().Contains(user.Value.Username.ToLower()) && user.Value.EnableNotifications)
                    await discordClient.SendMessageAsync(discordChannel, $"<@{user.Key}> Twitch message: \"{e.ChatMessage.Message}\" by {e.ChatMessage.DisplayName}");
            }
        }

        private async void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            var command = e.Command.CommandText;

            foreach (var dynamicCommand in DynamicCommands.Commands)
            {
                if (command == dynamicCommand.Command)
                {
                    SendMessage(dynamicCommand.Answer);
                }
            }

            if (command == "uptime")
            {
                var uptime = await api.Uptime();
                if (uptime == null)
                    SendMessage("simuleios is currently offline");
                else
                    SendMessage(uptime.ToString());
            }

            if (command == "help")
            {
                var availableCommands = "Available commands:";

                // Add in-code commands.
                availableCommands += " !uptime";

                // Add dynamic commands.
                foreach (var dynamicCommand in DynamicCommands.Commands)
                {
                    availableCommands += " !" + dynamicCommand.Command;
                }
                SendMessage(availableCommands);
            }
        }
    }
}