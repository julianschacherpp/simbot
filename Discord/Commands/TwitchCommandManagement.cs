using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace simbot.Discord.Commands
{
    public class TwitchCommandManagement
    {
        private Twitch.Client client;
        private Config.Config config;
        TwitchCommandManagementDependencies dep;

        public TwitchCommandManagement(TwitchCommandManagementDependencies dep)
        {
            this.dep = dep;
            this.client = dep.Client;
            this.config = dep.Config;
        }

        [Command("AddTwitchCommand"), Aliases("atc"), Description("Adds a command to the list of twitch commands.")]
        public async Task AddTwitchCommand(CommandContext context)
        {
            Log.Console.LogCommand("AddTwitchCommand", context);

            if (!(context.User.Id == config.Discord.BotCreator || context.User.Id == config.Discord.Streamer))
            {
                await context.RespondAsync("You're not allowed to add a twitch command.");
                return;
            }
            else
            {
                var message = context.Message.Content;
                message = message.Trim();
                message = message.Remove(0, 1);
                message = message.TrimStart();

                var discordCommand = message.Split(' ')[0];
                message = message.Remove(0, discordCommand.Length);
                message = message.TrimStart();

                var twitchCommand = message.Split(' ')[0];

                if (client.DynamicCommands.HasCommand(twitchCommand))
                {
                    await context.RespondAsync($"The command \"{twitchCommand}\" already exists.");
                    return;
                }

                message = message.Remove(0, twitchCommand.Length);
                message = message.TrimStart();

                await client.DynamicCommands.AddCommandAsync(twitchCommand, message);
                await context.RespondAsync($"You successfully created a twitch command with the following properties: command = \"{twitchCommand}\", answer = \"{message}\"");
            }
        }

        [Command("RemoveTwitchCommand"), Aliases("rtc"), Description("Removes a command from the list of twitch commands.")]
        public async Task RemoveTwitchCommand(CommandContext context)
        {
            Log.Console.LogCommand("RemoveTwitchCommand", context);

            if (!(context.User.Id == config.Discord.BotCreator || context.User.Id == config.Discord.Streamer))
            {
                await context.RespondAsync("You're not allowed to remove a twitch command.");
                return;
            }
            else
            {
                var message = context.Message.Content;
                message = message.Trim();
                message = message.Remove(0, 1);
                message = message.TrimStart();

                var discordCommand = message.Split(' ')[0];
                message = message.Remove(0, discordCommand.Length);
                message = message.TrimStart();

                var twitchCommand = message.Split(' ')[0];

                if (!client.DynamicCommands.HasCommand(twitchCommand))
                {
                    await context.RespondAsync($"The command \"{twitchCommand}\" doesn't exist.");
                    return;
                }

                await client.DynamicCommands.RemoveCommandAsync(twitchCommand);
                await context.RespondAsync($"You successfully removed the {twitchCommand} command"); 
            }
        }

        [Command("ListTwitchCommands"), Aliases("ltc"), Description("Returns a list of all twitch commands.")]
        public async Task ListTwitchCommands(CommandContext context)
        {
            Log.Console.LogCommand("ListTwitchCommands", context);

            var answer = new StringBuilder();
            answer.AppendLine("List of all twitch commands:");
            answer.AppendLine("");

            foreach (var command in client.DynamicCommands.Commands)
                answer.AppendLine($"\"!{command.Command}\" => \"{command.Answer}\"");

            await context.RespondAsync(answer.ToString());
        }
    }
}