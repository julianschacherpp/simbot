using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using simbot.Twitch;

namespace simbot.Discord.Commands
{
    public class TwitchInfo
    {
        private StreamerLiveNotificationUsers streamerLiveNotificationUsers;
        private Api api;
        TwitchInfoDependencies dep;

        public TwitchInfo(TwitchInfoDependencies dep)
        {
            this.dep = dep;
            streamerLiveNotificationUsers = dep.StreamerLiveNotificationUsers;
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

        [Command("ToggleStreamerLiveNotifications"), Aliases("tsln"), Description("Toggles whether or not you get notified, when the streamer goes live.")]
        public async Task ToggleStreamerLiveNotification(CommandContext context)
        {
            Log.Console.LogCommand("ToggleStreamerLiveNotification", context);

            var userId = context.User.Id;
            var result = await streamerLiveNotificationUsers.ToggleGetEnableNotifications(userId);

            if (result == null)
            {
                await streamerLiveNotificationUsers.AddUsersAsync(userId, true);
                await context.RespondAsync("Streamer live notifications have been enabled.");
            }
            else if (result == true)
                await context.RespondAsync("Streamer live notifications have been enabled.");
            else
                await context.RespondAsync("Streamer live notifications have been disabled.");
        }
    }
}