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


        public Bot(Config.Config config)
        {
            mainChatChannel = config.Discord.MainChatChannel;
            twitchChatChannel = config.Discord.TwitchChatChannel;
            discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = config.Discord.Token,
                TokenType = TokenType.Bot
            });

            AddAllEventHandlers();

            twitch = new Twitch.Twitch(config, discord);

            DependencyCollection dep = null;
            using (var d = new DependencyCollectionBuilder())
            {
                d.AddInstance(new Discord.TwitchCommunicationDependencies()
                {
                    Client = twitch.Client
                });
                d.AddInstance(new Discord.TwitchInfoDependencies()
                {
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
            lastIsOnlineState = await twitch.Api.IsOnline;
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
        }

        private async Task PresenceUpdatedHandler(DSharpPlus.EventArgs.PresenceUpdateEventArgs e)
        {
            // if (e.Member.Id == 357141789733421057)
            if (e.Member.Id == 113417954699321344)
            {
                var isOnline = await twitch.Api.IsOnline;

                if (isOnline && !lastIsOnlineState)
                {
                    var message = "// Leios is live!";
                    Log.Console.Log(Log.Category.Discord, message);
                    await discord.SendMessageAsync(await discord.GetChannelAsync(mainChatChannel), message);
                }
                else if (!isOnline && lastIsOnlineState)
                {
                    var message = "// Leios is not live anymore.";
                    Log.Console.Log(Log.Category.Discord, message);
                    await discord.SendMessageAsync(await discord.GetChannelAsync(mainChatChannel), message);
                }

                lastIsOnlineState = await twitch.Api.IsOnline;
            }
        }
    }
}