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
    }
}
