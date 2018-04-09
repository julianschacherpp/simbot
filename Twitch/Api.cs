using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus;
using TwitchLib;

namespace simbot.Twitch
{
    public class Api
    {
        private static TwitchAPI twitchAPI; 
        private string channelId;
        private bool? lastIsOnlineStatus = null;
        public event AsyncEventHandler StreamerGoesOnline;
        public event AsyncEventHandler StreamerGoesOffline;

        public Api(Config.Config config)
        {
            twitchAPI = new TwitchAPI(config.Twitch.ClientId, config.Twitch.AccessToken);
            channelId = config.Twitch.ChannelId;

            Task streamerOnlineStatusTimerTask = RunPeriodically(PollStreamerOnlineStatus, TimeSpan.FromMinutes(1));
        }


        public async Task<TimeSpan?> Uptime()
        {
            if (!await CheckStreamerOnlineStatus())
                return null;

            var targetHelixUserIds = new List<string>();
            targetHelixUserIds.Add(channelId);

            var streams = await twitchAPI.Streams.helix.GetStreams(null, null, 20, null, null, "all", targetHelixUserIds, null);

            return DateTime.Now.Subtract(streams.Streams[0].StartedAt.AddHours(2));
        }

        public async Task PollStreamerOnlineStatus()
        {
            var isOnline = await CheckStreamerOnlineStatus();
            if (lastIsOnlineStatus == null)
            {
                lastIsOnlineStatus = isOnline;
                return;
            }

            if (!lastIsOnlineStatus.Value && isOnline)
                await StreamerGoesOnline?.Invoke();
            else if (lastIsOnlineStatus.Value && !isOnline)
                await StreamerGoesOffline?.Invoke();
            
            lastIsOnlineStatus = isOnline;
        }

        public async Task<bool> CheckStreamerOnlineStatus() => await twitchAPI.Streams.v5.BroadcasterOnlineAsync(channelId);

        async Task RunPeriodically(Func<Task> action, TimeSpan interval)
        {
            while (true)
            {
                await action();
                await Task.Delay(interval);
            }
        }
    }
}