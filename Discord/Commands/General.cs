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

        [Command("ArcaneAlgorithmArchive"), Aliases("aaa"), Description("Returns the URL of the Arcane Algorithm Archive.")]
        public async Task ArcaneAlgorithmArchive(CommandContext context)
        {
            Log.Console.LogCommand("ArcaneAlgorithmArchive", context);

            await context.RespondAsync($"The Arcane Algorithm Archive: https://www.gitbook.com/book/leios/algorithm-archive/details");
        }

        [Command("ArcaneAlgorithmArchiveDev"), Aliases("aaadev"), Description("Returns the URL of the Arcane Algorithm Archive's GitHub repository.")]
        public async Task ArcaneAlgorithmArchiveDev(CommandContext context)
        {
            Log.Console.LogCommand("ArcaneAlgorithmArchiveDev", context);

            await context.RespondAsync($"The Arcane Algorithm Archive's GitHub repo: https://github.com/leios/algorithm-archive");
        }
    }
}