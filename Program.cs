using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using simbot;

namespace simbot
{
    public class Program
    {
        static string configPath = "Config.json";
        static Config.Config config;
        static DiscordClient discord;
        static CommandsNextModule commands;

        static void Main(string[] args) => MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
            config = await Config.Config.LoadAsync(configPath);

            discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = config.Discord.Token,
                TokenType = TokenType.Bot
            });

            DependencyCollection dep = null;
            using (var d = new DependencyCollectionBuilder())
            {
                d.AddInstance(new Discord.Dependencies()
                {
                    DiscordClient = discord,
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
            commands.RegisterCommands<Discord.Commands.Twitch>();

            Log.Console.Log(Log.Category.Discord, "Connecting to Discord...");
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
