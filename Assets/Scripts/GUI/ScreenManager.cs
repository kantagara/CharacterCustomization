using System;
using Scripts.EventSystem;
using UnityEngine;

namespace Scripts.GUI
{
    //This could be expanded into something more complex like a scriptable object 
    //(The same approach I'm doing for categories atm)
    public enum ScreenType
    {
        LoginRegister,
        Customization
    }

    [Serializable]
    public class Screen
    {
        [field: SerializeField] public ScreenType ScreenType { get; private set; }
        [field: SerializeField] public GameObject GameObject { get; private set; }
    }

    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] private Screen[] screens;
        [SerializeField] private ScreenType defaultScreen;

        private Screen _currentScreen;

        private void Awake()
        {
            foreach (var screen in screens)
                screen.GameObject.SetActive(false);

            ShowScreen(defaultScreen);

            EventSystem<OnUserLogin>.Subscribe(UserLogin);
            EventSystem<OnUserLogout>.Subscribe(UserLogout);
        }

        private void OnDestroy()
        {
            EventSystem<OnUserLogin>.Unsubscribe(UserLogin);
            EventSystem<OnUserLogout>.Unsubscribe(UserLogout);
        }

        private void UserLogout(OnUserLogout obj)
        {
            ShowScreen(ScreenType.LoginRegister);
        }


        private void UserLogin(OnUserLogin obj)
        {
            ShowScreen(ScreenType.Customization);
        }

        private void ShowScreen(ScreenType screenType)
        {
            //This ? will work because _currentScreen is either actually null or an actual reference
            //To an object that will never be destroyed
            _currentScreen?.GameObject.SetActive(false);

            foreach (var screen in screens)
            {
                if (screen.ScreenType != screenType) continue;

                _currentScreen = screen;
                screen.GameObject.SetActive(true);
                return;
            }
        }
    }
}