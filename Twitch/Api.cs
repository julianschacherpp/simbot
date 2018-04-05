using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib;

namespace simbot.Twitch
{
    public class Api
    {
        private static TwitchAPI twitchAPI; 
        private string channelId;
        public Task<bool> IsOnline { get => twitchAPI.Streams.v5.BroadcasterOnlineAsync(channelId); }

        public Api(Config.Config config)
        {
            twitchAPI = new TwitchAPI(config.Twitch.ClientId, config.Twitch.AccessToken);
            channelId = config.Twitch.ChannelId;
        }

        public async Task<TimeSpan?> Uptime()
        {
            if (!await IsOnline)
                return null;

            var targetHelixUserIds = new List<string>();
            targetHelixUserIds.Add(channelId);

            var streams = await twitchAPI.Streams.helix.GetStreams(null, null, 20, null, null, "all", targetHelixUserIds, null);

            return DateTime.Now.Subtract(streams.Streams[0].StartedAt.AddHours(2));
        }
    }
}