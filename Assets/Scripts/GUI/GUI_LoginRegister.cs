using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GUI
{
    public class GUI_LoginRegister : MonoBehaviour
    {
        [SerializeField] private TMP_InputField username, password;
        [SerializeField] private UserManager userManager;
        [SerializeField] private TextMeshProUGUI lblLoginError;
        [SerializeField] private Button loginButton, registerButton;

        private void Start()
        {
            loginButton.interactable = registerButton.interactable = false;
            username.onValueChanged.AddListener(OnTextChanged);
            password.onValueChanged.AddListener(OnTextChanged);
        }

        private void OnDestroy()
        {
            username.onValueChanged.RemoveAllListeners();
            password.onValueChanged.RemoveAllListeners();
        }

        private void OnTextChanged(string arg0)
        {
            loginButton.interactable = registerButton.interactable = !string.IsNullOrEmpty(username.text.Trim()) &&
                                                                           !string.IsNullOrEmpty(password.text.Trim());   
        }

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