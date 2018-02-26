using System.Linq;
using System.Xml.Linq;

namespace simbot
{
    public static class Config 
    {
        public static string GetDiscordToken()
        {
            XElement config = XElement.Load("Config.xml");
            return config.Elements("Discord").Elements("Token").SingleOrDefault().Value;
        }

        public static string GetTwitchUsername()
        {
            XElement config = XElement.Load("Config.xml");
            return config.Elements("Twitch").Elements("Username").SingleOrDefault().Value;
        }

        public static string GetTwitchAccessToken()
        {
            XElement config = XElement.Load("Config.xml");
            return config.Elements("Twitch").Elements("AccessToken").SingleOrDefault().Value;
        }

        public static string GetTwitchChannelName()
        {
            XElement config = XElement.Load("Config.xml");
            return config.Elements("Twitch").Elements("ChannelName").SingleOrDefault().Value;
        }
    }
}
