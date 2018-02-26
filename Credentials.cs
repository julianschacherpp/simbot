using System.Linq;
using System.Xml.Linq;

namespace simbot
{
    public static class Credentials 
    {
        public static string GetDiscordToken()
        {
            XElement credentials = XElement.Load("credentials.xml");
            return credentials.Elements("Discord").Elements("Token").SingleOrDefault().Value;
        }
    }
}
