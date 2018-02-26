using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace simbot.Commands
{
    public class General
    {
        [Command("twitch")]
        public async Task Twitch(CommandContext context)
        {
            Log.Console.LogCommand("twitch", context);

            await context.RespondAsync($"Leios' Twitch channel: https://www.twitch.tv/simuleios");
        }

        [Command("youtube")]
        public async Task Youtube(CommandContext context)
        {
            Log.Console.LogCommand("youtube", context);

            await context.RespondAsync($"Leios' YouTube channel: https://www.youtube.com/user/LeiosOS");
        }
    }
}