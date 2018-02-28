using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace simbot.Discord.Commands
{
    public class General
    {
        [Command("Twitch"), Aliases("tw"), Description("Returns the URL of Leios' Twitch channel.")]
        public async Task Twitch(CommandContext context)
        {
            Log.Console.LogCommand("Twitch", context);

            await context.RespondAsync($"Leios' Twitch channel: https://www.twitch.tv/simuleios");
        }

        [Command("YouTube"), Aliases("yt"), Description("Returns the URL of Leios' YouTube channel.")]
        public async Task YouTube(CommandContext context)
        {
            Log.Console.LogCommand("YouTube", context);

            await context.RespondAsync($"Leios' YouTube channel: https://www.youtube.com/user/LeiosOS");
        }
    }
}