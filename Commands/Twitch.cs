using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using simbot.Twitch;

namespace simbot.Commands
{
    public class Twitch
    {
        private Chat chat;

        public Twitch()
        {
            chat = new Chat();
            chat.Connect();
        }

        [Command("tm")]
        public async Task Tm(CommandContext context)
        {
            Log.Console.LogCommand("tm", context);

            chat.SendMessage($"{context.Message.Author.Username}: {context.RawArgumentString.TrimStart()}");
        }

        [Command("tma")]
        public async Task Tma(CommandContext context)
        {
            Log.Console.LogCommand("tma", context);

            chat.SendMessage($"{context.RawArgumentString.TrimStart()} *{context.Message.Author.Username}");
        }
    }
}