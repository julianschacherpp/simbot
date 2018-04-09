using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using simbot.Twitch;

namespace simbot.Discord.Commands
{
    public class TwitchInfo
    {
        private Api api;
        TwitchInfoDependencies dep;

        public TwitchInfo(TwitchInfoDependencies dep)
        {
            this.dep = dep;
            api = dep.Api;
        }

        [Command("IsLive"), Aliases("il"), Description("Returns whether or not Leios is live.")]
        public async Task IsLive(CommandContext context)
        {
            Log.Console.LogCommand("IsLive", context);

            if (await api.CheckStreamerOnlineStatus())
                await context.RespondAsync("Leios is live.");
            else
                await context.RespondAsync("Leios is not live.");
        }
    }
}