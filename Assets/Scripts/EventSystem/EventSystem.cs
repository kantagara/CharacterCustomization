using System;

namespace Scripts.EventSystem
{
    /// <summary>
    /// Global event system that I like to use in all of my Unity games.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class EventSystem<T> where T:EventArgs
    {
        private static event Action<T> @event;
        
        public static void Subscribe(Action<T> action)
        {
            @event += action;
        }

        public static void Unsubscribe(Action<T> action)
        {
            @event -= action;
        }

        public static void Invoke(T parameter) => @event?.Invoke(parameter);

    }
}