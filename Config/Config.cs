using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace simbot.Config
{
    public class Discord
    {
        public string Token { get; set; }
        public ulong TwitchChatChannel { get; set; }
    }

    public class Twitch
    {
        public string Username { get; set; }
        public string ChannelName { get; set; }
        public string ClientId { get; set; }
        public string AccessToken { get; set; }
    }

    public class Config
    {
        public Discord Discord { get; set; }
        public Twitch Twitch { get; set; }

        public static async Task<Config> LoadAsync(string path)
        {
            return JsonConvert.DeserializeObject<Config>(await File.ReadAllTextAsync(path));
        }
    }
}