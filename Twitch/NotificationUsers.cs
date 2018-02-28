using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace simbot.Twitch
{
    public class NotificationUser
    {
        public string Username { get; set; }
        public bool EnableNotifications { get; set; }

        public NotificationUser(string username, bool enableNotifications)
        {
            Username = username;
            EnableNotifications = enableNotifications;
        }
    }

    public class NotificationUsers
    {
        private static string path = "TwitchNotificationUsers.json";
        public Dictionary<ulong, NotificationUser> Users { get; private set; } = new Dictionary<ulong, NotificationUser>();

        public NotificationUsers() => LoadUsers();

        public void LoadUsers()
        {
            if (File.Exists(path))
            {
                var text = File.ReadAllText(path);
                Users = JsonConvert.DeserializeObject<Dictionary<ulong, NotificationUser>>(text);
            }
        }

        public async Task SaveUsersAsync()
        {
            var userJson = JsonConvert.SerializeObject(Users, Formatting.Indented);
            await File.WriteAllTextAsync(path, userJson);
        }

        public async Task AddUserAsync(ulong id, string username, bool enableNotifications)
        {
            if (!Users.ContainsKey(id))
            {
                Users.Add(id, new NotificationUser(username, enableNotifications));
                await SaveUsersAsync();
            }
        }

        // Returns null, if the user doesn't exists, otherwise it returns the new value of EnableNotifications.
        public async Task<bool?> ToggleGetEnableNotifications(ulong id)
        {
            System.Console.WriteLine(Users.ContainsKey(id));
            if (!Users.ContainsKey(id))
                return null;
            else
            {
                var user = Users.GetValueOrDefault(id);
                if (Users[id].EnableNotifications)
                {
                    Users[id].EnableNotifications = false;
                    await SaveUsersAsync();
                    return false;
                }
                else
                {
                    Users[id].EnableNotifications = true;
                    await SaveUsersAsync();
                    return true;
                }
            }
        }
    }
}