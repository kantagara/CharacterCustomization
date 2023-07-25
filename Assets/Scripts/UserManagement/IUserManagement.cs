using System.Collections.Generic;
using Scripts.Model;

namespace Scripts.UserManagement
{
    public interface IUserManagement
    {
        bool TryRegister(User user, out string message);
        void SaveAllUsers();
        public bool TryLogin(string username, string password, out User user, out string message);
    }
}