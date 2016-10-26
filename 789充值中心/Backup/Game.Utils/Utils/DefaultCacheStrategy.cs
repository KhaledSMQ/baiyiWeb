namespace Game.Utils
{
    using System;
    using System.Collections;

    public class DefaultCacheStrategy : ICacheStrategy
    {
        private Hashtable objectTable = new Hashtable();

        public object Get(string key)
        {
            return this.objectTable[key];
        }

        public T Get<T>(string key)
        {
            object obj2 = this.Get(key);
            if (!((obj2 != null) && (obj2 is T)))
            {
                return default(T);
            }
            return (T) obj2;
        }

        public void Remove(string key)
        {
            this.objectTable.Remove(key);
        }

        public void Set(string key, object obj)
        {
            this.objectTable.Add(key, obj);
        }

        public void Set(string key, object obj, DateTime expiresAt)
        {
            this.objectTable.Add(key, obj);
        }
    }
}

