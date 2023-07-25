using System;
using TMPro;
using UnityEngine;

namespace Scripts.GUI
{
    public class GUI_LoginRegister : MonoBehaviour
    {
        [SerializeField] private TMP_InputField username, password;
        [SerializeField] private UserManager userManager;
        [SerializeField] private TextMeshProUGUI lblLoginError;

        public void Login()
        {
            lblLoginError.SetText("");
            if (!userManager.TryLogin(username.text, password.text, out var message))
                lblLoginError.SetText(message);
        }

        public void Register()
        {
            if (!userManager.TryRegister(username.text, password.text, out var message))
                lblLoginError.SetText(message);
        }
    }
}