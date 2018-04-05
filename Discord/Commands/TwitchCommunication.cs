using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using simbot.Discord;
using simbot.Twitch;

namespace simbot.Discord.Commands
{
    public class TwitchCommunication
    {
        private Client client;
        TwitchCommunicationDependencies dep;

        public TwitchCommunication(TwitchCommunicationDependencies dep)
        {
            this.dep = dep;
            client = dep.Client;
        }

        [Command("TwitchMessage"), Aliases("tm"), Description("Writes the input after the command call to the simuleios Twitch chat. This happens in the following format: \"[DiscordUserName]: [input]\".")]
        public async Task TwitchMessage(CommandContext context)
        {
            Log.Console.LogCommand("TwitchMessage", context);

            client.SendMessage($"{context.Message.Author.Username}: {context.RawArgumentString.TrimStart()}");
        }

        [Command("TwitchMessageAfter"), Aliases("tma"), Description("Writes the input after the command call to the simuleios Twitch chat. This command should be used, if you want to trigger Twitch commands, since the input is written first. So the format in the Twitch chat is the following: \"[input] *[DiscordUserName]\".")]
        public async Task TwitchMessageAfter(CommandContext context)
        {
            Log.Console.LogCommand("TwitchMessageAfter", context);

            client.SendMessage($"{context.RawArgumentString.TrimStart()} *{context.Message.Author.Username}");
        }

        [Command("ToggleTwitchChatNotifications"), Aliases("ttcn"), Description("Toggles whether or not you get notified, when someone in the Twitch chat sends a message, that contains the name of the bot and your name.")]
        public async Task ToggleTwitchChatNotifications(CommandContext context)
        {
            Log.Console.LogCommand("ToggleTwitchChatNotifications", context);
            
            var result = client.notificationUsers.ToggleGetEnableNotifications(context.User.Id);
            System.Console.WriteLine(await result == null);

            if (await result == null)
            {
                await client.notificationUsers.AddUserAsync(context.User.Id, context.User.Username, true);
                await context.RespondAsync("Twitch chat notifications have been enabled.");
            }
            else if (await result == true)
                await context.RespondAsync("Twitch chat notifications have been enabled.");
            else
                await context.RespondAsync("Twitch chat notifications have been disabled.");
        }
    }
}