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

        [Command("tm"), Description("Writes the input after the command call to the simuleios Twitch chat. This happens in the following format: \"[DiscordUserName]: [input]\".")]
        public async Task Tm(CommandContext context)
        {
            Log.Console.LogCommand("tm", context);

            chat.SendMessage($"{context.Message.Author.Username}: {context.RawArgumentString.TrimStart()}");
        }

        [Command("tma"), Description("Writes the input after the command call to the simuleios Twitch chat. This command should be used, if you want to trigger Twitch commands, since the input is written first. So the format in the Twitch chat is the following: \"[input] *[DiscordUserName]\".")]
        public async Task Tma(CommandContext context)
        {
            Log.Console.LogCommand("tma", context);

            chat.SendMessage($"{context.RawArgumentString.TrimStart()} *{context.Message.Author.Username}");
        }
    }
}