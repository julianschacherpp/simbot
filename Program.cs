using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using simbot;

namespace simbot
{
    public class Program
    {
        static string configPath = "Config.json";

        static void Main(string[] args) => MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
            var bot = new Discord.Bot(await Config.Config.LoadAsync(configPath));
            await bot.StartAsync();
            await Task.Delay(-1);
        }
    }
}
