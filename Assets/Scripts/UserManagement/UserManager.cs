using System.Collections.Generic;
using Scripts.EventSystem;
using Scripts.Model;
using Scripts.UserManagement;
using UnityEngine;

namespace Scripts
{
    /// <summary>
    ///     Simple class that is responsible for user management
    ///     It's MonoBehavior only because of the fact that MonoBehaviors have Awake and OnDestroy,
    ///     I know that I could use constructors and destructors, but this is a bit more straightforward.
    /// </summary>
    public class UserManager : MonoBehaviour
    {
        private IUserManagement _userManagement;

        public static User LocalUser { get; private set; }

        private void Awake()
        {
            _userManagement = new LocalUserManagementService();
            EventSystem<OnItemAdded>.Subscribe(OnItemAdded);
            EventSystem<OnGUIItemRemoved>.Subscribe(OnItemRemoved);
        }


        private void OnDisable()
        {
            _userManagement.SaveAllUsers();
        }


        private void OnItemRemoved(OnGUIItemRemoved itemRemoved)
        {
            if (!LocalUser.CurrentlySelectedItems.ContainsKey(itemRemoved.Item.Category.Name))
                return;
            if (LocalUser.CurrentlySelectedItems[itemRemoved.Item.Category.Name] != itemRemoved.Item.Name)
                return;

            LocalUser.CurrentlySelectedItems.Remove(itemRemoved.Item.Category.Name);
            EventSystem<OnItemRemoved>.Invoke(new OnItemRemoved { Item = itemRemoved.Item });
        }

        private void OnItemAdded(OnItemAdded itemAdded)
        {
            if (!LocalUser.OwnedItems.Contains(itemAdded.Item.Name))
                //Maybe Name for the NFT will be something else, like it's address or sth like that?
                LocalUser.OwnedItems.Add(itemAdded.Item.Name);

            LocalUser.CurrentlySelectedItems[itemAdded.Item.Category.Name] = itemAdded.Item.Name;
        }

        public bool TryLogin(string username, string password, out string message)
        {
            var loggedIn = _userManagement.TryLogin(username, password, out var user, out message);

            if (loggedIn)
            {
                EventSystem<OnUserLogin>.Invoke(new OnUserLogin());
                LocalUser = user;
            }

            return loggedIn;
        }

        public bool TryRegister(string username, string password, out string message)
        {
            var user = new User(username, password, new Dictionary<string, string>(), new HashSet<string>());

            var registered = _userManagement.TryRegister(user, out message);
            if (registered)
            {
                EventSystem<OnUserRegister>.Invoke(new OnUserRegister());
                LocalUser = user;
            }

            return registered;
        }

        public void Logout()
        {
            _userManagement.SaveAllUsers();
            LocalUser = null;
            EventSystem<OnUserLogout>.Invoke(new OnUserLogout());
        }
    }
}