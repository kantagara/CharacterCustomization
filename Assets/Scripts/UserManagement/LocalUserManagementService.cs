using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Scripts.Model;
using UnityEngine;

namespace Scripts.UserManagement
{
    /// <summary>
    ///     For 'local database' we're just retrieving users in a list immediately in the constructor.
    ///     For Remote locations we'd probably just have some sort of a connection to the database
    /// </summary>
    public class LocalUserManagementService : IUserManagement
    {
        private readonly List<User> _users;

        public LocalUserManagementService()
        {
            _users = GetAllUsers();
        }

        private static string Path => Application.persistentDataPath + "/users.json";


        public bool TryRegister(User user, out string message)
        {
            message = "";
            if (_users.Any(x =>
                    user.Username.Trim().Equals(x.Username.Trim(), StringComparison.InvariantCultureIgnoreCase)))
            {
                message = "User with the same username already exists!";
                return false;
            }

            //Potentially add some password security checks here

            _users.Add(user);
            return true;
        }

        public void SaveAllUsers()
        {
            File.WriteAllText(Path,
                JsonConvert.SerializeObject(_users));
        }

        public bool TryLogin(string username, string password, out User user, out string message)
        {
            message = "";
            user = _users.FirstOrDefault(x =>
                username.Trim().Equals(x.Username.Trim(), StringComparison.InvariantCultureIgnoreCase) &&
                password == x.Password);
            if (user != null)
                return true;
            message = "Username and/or password are not correct";
            return false;
        }

        private static List<User> GetAllUsers()
        {
            return !File.Exists(Path)
                ? new List<User>()
                : JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(Path));
        }
    }
}