using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace simbot
{
    class Program
    {
        static DiscordClient discord;
        static CommandsNextModule commands;

        static void Main(string[] args) => MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
            discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = Config.GetDiscordToken(),
                TokenType = TokenType.Bot
            });

            commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefix = "!"
            });

            commands.RegisterCommands<Commands.General>();
            commands.RegisterCommands<Commands.Twitch>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
