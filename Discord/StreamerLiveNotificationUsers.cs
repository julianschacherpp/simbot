using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace simbot.Discord
{
    public class StreamerLiveNotificationUsers
    {
        public static string path = "StreamerLiveNotificationUsers.json";
        public Dictionary<ulong, bool> Users { get; private set; } = new Dictionary<ulong, bool>();

        public StreamerLiveNotificationUsers() => LoadUsers();

        public void LoadUsers()
        {
            if (File.Exists(path))
            {
                var text = File.ReadAllText(path);
                Users = JsonConvert.DeserializeObject<Dictionary<ulong, bool>>(text);
            }
        }

        public async Task SaveUsersAsync()
        {
            var userJson = JsonConvert.SerializeObject(Users, Formatting.Indented);
            await File.WriteAllTextAsync(path, userJson);
        }

        public async Task AddUsersAsync(ulong id, bool enableNotifications)
        {
            if (!Users.ContainsKey(id))
            {
                Users.Add(id, enableNotifications);
                await SaveUsersAsync();
            }
        }

        // Returns null, if the user doesn't exists, otherwise it returns the new value of EnableNotifications.
        public async Task<bool?> ToggleGetEnableNotifications(ulong id)
        {
            if (!Users.ContainsKey(id))
                return null;
            else
            {
                var user = Users.GetValueOrDefault(id);
                if (Users[id])
                {
                    Users[id] = false;
                    await SaveUsersAsync();
                    return false;
                }
                else
                {
                    Users[id] = true;
                    await SaveUsersAsync();
                    return true;
                }
            }
        }
    }
}