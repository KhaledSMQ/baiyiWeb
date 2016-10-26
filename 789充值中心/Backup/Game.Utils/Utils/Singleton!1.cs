namespace Game.Utils
{
    using System;

    public sealed class Singleton<T> where T: new()
    {
        private static T instance;
        private static object lockHelper;

        static Singleton()
        {
            Singleton<T>.instance = new T();
            Singleton<T>.lockHelper = new object();
        }

        private Singleton()
        {
        }

        public static T GetInstance()
        {
            if (Singleton<T>.instance == null)
            {
                lock (Singleton<T>.lockHelper)
                {
                    if (Singleton<T>.instance == null)
                    {
                        Singleton<T>.instance = new T();
                    }
                }
            }
            return Singleton<T>.instance;
        }

        public void SetInstance(T value)
        {
            Singleton<T>.instance = value;
        }
    }
}

