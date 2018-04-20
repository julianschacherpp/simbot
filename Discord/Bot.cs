using System;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace simbot.Discord
{
    public class Bot
    {

        Config.Config config;
        DiscordClient discord;
        CommandsNextModule commands;
        Twitch.Twitch twitch;
        private ulong mainChatChannel;
        private ulong twitchChatChannel;
        private bool lastIsOnlineState;
        StreamerLiveNotificationUsers StreamerLiveNotificationUsers { get; set; } = new StreamerLiveNotificationUsers();


        public Bot(Config.Config config)
        {
            mainChatChannel = config.Discord.MainChatChannel;
            twitchChatChannel = config.Discord.TwitchChatChannel;
            discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = config.Discord.Token,
                TokenType = TokenType.Bot
            });

            twitch = new Twitch.Twitch(config, discord);

            AddAllEventHandlers();

            DependencyCollection dep = null;
            using (var d = new DependencyCollectionBuilder())
            {
                d.AddInstance(new Discord.TwitchCommunicationDependencies()
                {
                    Client = twitch.Client
                });
                d.AddInstance(new Discord.TwitchInfoDependencies()
                {
                    StreamerLiveNotificationUsers = StreamerLiveNotificationUsers,
                    Api = twitch.Api
                });
                d.AddInstance(new Discord.TwitchCommandManagementDependencies()
                {
                    Client = twitch.Client,
                    Config = config
                });
                dep = d.Build();
            }

            commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefix = "!",
                CaseSensitive = false,
                Dependencies = dep
            });

            commands.RegisterCommands<Discord.Commands.General>();
            commands.RegisterCommands<Discord.Commands.TwitchCommunication>();
            commands.RegisterCommands<Discord.Commands.TwitchInfo>();
            commands.RegisterCommands<Discord.Commands.TwitchCommandManagement>();
        }

        public async Task StartAsync()
        {
            Log.Console.Log(Log.Category.Discord, "Connecting to Discord...");
            await discord.ConnectAsync();
        }

        public async Task StopAsync()
        {
            await discord.DisconnectAsync();
        }

        private void AddAllEventHandlers()
        {
            discord.PresenceUpdated += PresenceUpdatedHandler;
            twitch.Api.StreamerGoesOnline += StreamerGoesOnlineHandler;
            twitch.Api.StreamerGoesOffline += StreamerGoesOfflineHandler;
        }

        private async Task PresenceUpdatedHandler(DSharpPlus.EventArgs.PresenceUpdateEventArgs e)
        {
            if (e.Member.Id == config.Discord.Streamer)
                await twitch.Api.PollStreamerOnlineStatus();
        }

        private async Task StreamerGoesOnlineHandler()
        {
            var message = "// Leios is live!\n";
            foreach(var user in StreamerLiveNotificationUsers.Users)
            {
                if (user.Value)
                    message += $"<@{user.Key}> ";
            }

            Log.Console.Log(Log.Category.Discord, message);
            await discord.SendMessageAsync(await discord.GetChannelAsync(mainChatChannel), message);
        }

        private async Task StreamerGoesOfflineHandler()
        {
            var message = "// Leios is not live anymore.";
            Log.Console.Log(Log.Category.Discord, message);
            await discord.SendMessageAsync(await discord.GetChannelAsync(mainChatChannel), message);
        }
    }
}