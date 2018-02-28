using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace simbot.Discord.Commands
{
    public class General
    {
        [Command("twitch"), Description("Returns the URL of Leios' Twitch channel.")]
        public async Task Twitch(CommandContext context)
        {
            Log.Console.LogCommand("twitch", context);

            await context.RespondAsync($"Leios' Twitch channel: https://www.twitch.tv/simuleios");
        }

        [Command("youtube"), Description("Returns the URL of Leios' YouTube channel.")]
        public async Task Youtube(CommandContext context)
        {
            Log.Console.LogCommand("youtube", context);

            await context.RespondAsync($"Leios' YouTube channel: https://www.youtube.com/user/LeiosOS");
        }
    }
}