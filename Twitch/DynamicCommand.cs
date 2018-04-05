using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace simbot.Twitch
{
    public class DynamicCommand
    {
        public string Command { get; set; }
        public string Answer { get; set; }

        public DynamicCommand(string command, string answer)
        {
            this.Command = command;
            this.Answer = answer;
        }
    }

    public class DynamicCommands
    {
        private static string path = "DynamicCommands.json";
        public List<DynamicCommand> Commands { get; private set; } = new List<DynamicCommand>();

        public DynamicCommands() => LoadCommands();

        public void LoadCommands()
        {
            if (File.Exists(path))
            {
                var text = File.ReadAllText(path);
                Commands = JsonConvert.DeserializeObject<List<DynamicCommand>>(text);
            }
        }

        public async Task SaveCommandsAsync()
        {
            var commandsJson = JsonConvert.SerializeObject(Commands, Formatting.Indented);
            await File.WriteAllTextAsync(path, commandsJson);
        }

        public bool HasCommand(string command)
        {
            if (Commands.Exists(c => c.Command == command.Trim()))
                return true;
            else
                return false;
        }

        public async Task AddCommandAsync(string command, string answer)
        {
            command = command.Trim();
            answer = answer.Trim();

            if (!HasCommand(command))
            {
                Commands.Add(new DynamicCommand(command, answer));
                await SaveCommandsAsync();
            }
        }

        public async Task RemoveCommandAsync(string command)
        {
            var target = Commands.SingleOrDefault(c => c.Command == command.Trim());
            if (command != null)
            {
                Commands.Remove(target);
                await SaveCommandsAsync();
            }
        }
    }
}