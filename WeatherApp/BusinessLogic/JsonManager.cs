using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp.BusinessLogic
{
    public class JsonManager
    {
        private string _currentUser;
        public string CurrentUser { get { return _currentUser; } }

        public JsonManager(string currentUser) 
        { 
            _currentUser = currentUser;
            NewUser(currentUser);
        }
        public void NewUser(string newUsername)
        {
            string filePath = GetPath();

            List<User> users = new List<User>();

            try
            {
                users = GetUsers();
            }catch { }

            try
            {
                bool userExists = false;
                foreach (User user in users)
                {
                    if (user.Username == newUsername)
                    {
                        userExists = true;
                    }
                }

                if (userExists == false)
                {
                    users.Add(new User(newUsername));
                }

                SetUsers(users);

            } catch { }
            
        }
        public List<User> GetUsers()
        {
            string filePath = GetPath();

            using (FileStream reader = new FileStream(filePath, FileMode.Open))
            {
                return JsonSerializer.Deserialize<List<User>>(reader);
            }
        }

        public void SetUsers(List<User> users)
        {
            string filePath = GetPath();

            using (FileStream writer = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<List<User>>(writer, users);
            }
        }

        private string GetPath()
        {
            return Path.Combine(FileSystem.Current.AppDataDirectory, "userData.json");
        }
    }
}
