namespace Game.Utils
{
    using System;

    public interface ICacheStrategy
    {
        object Get(string key);
        T Get<T>(string key);
        void Remove(string key);
        void Set(string key, object obj);
        void Set(string key, object obj, DateTime expiresAt);
    }
}

