namespace Game.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.Caching;

    public sealed class CacheUtil
    {
        private const string CACHE_KEY_FORMAT = "{0}-UC";

        [Obsolete("慎用Add，因为两次调用Add第二次不会把第一次的值覆盖")]
        public static void Add<T>(string key, T value)
        {
            Add<T>(key, value, CacheTime.Default, CacheExpiresType.Sliding, null, CacheItemPriority.NotRemovable, null);
        }

        public static void Add<T>(string key, T value, CacheTime cacheTime)
        {
            Add<T>(key, value, cacheTime, CacheExpiresType.Sliding, null, CacheItemPriority.NotRemovable, null);
        }

        public static void Add<T>(string key, T value, CacheTime cacheTime, CacheExpiresType cacheExpiresType)
        {
            Add<T>(key, value, cacheTime, cacheExpiresType, null, CacheItemPriority.NotRemovable, null);
        }

        public static void Add<T>(string key, T value, CacheTime cacheTime, CacheExpiresType cacheExpiresType, CacheDependency dependencies)
        {
            Add<T>(key, value, cacheTime, cacheExpiresType, dependencies, CacheItemPriority.NotRemovable, null);
        }

        public static void Add<T>(string key, T value, CacheTime cacheTime, CacheExpiresType cacheExpiresType, CacheDependency dependencies, CacheItemPriority cacheItemPriority)
        {
            Add<T>(key, value, cacheTime, cacheExpiresType, dependencies, CacheItemPriority.NotRemovable, null);
        }

        public static void Add<T>(string key, T value, CacheTime cacheTime, CacheExpiresType cacheExpiresType, CacheDependency dependencies, CacheItemPriority cacheItemPriority, CacheItemRemovedCallback callback)
        {
            DateTime absoluteExpiration = GetAbsoluteExpiration(cacheTime, cacheExpiresType);
            TimeSpan slidingExpiration = GetSlidingExpiration(cacheTime, cacheExpiresType);
            if (cacheTime == CacheTime.NotRemovable)
            {
                cacheItemPriority = CacheItemPriority.NotRemovable;
            }
            HttpRuntime.Cache.Insert(GetPrivateCacheKey(key), value, dependencies, absoluteExpiration, slidingExpiration, cacheItemPriority, callback);
        }

        public static void Clear()
        {
            List<string> list = new List<string>();
            foreach (DictionaryEntry entry in HttpRuntime.Cache)
            {
                list.Add(entry.Key.ToString());
            }
            foreach (string str in list)
            {
                try
                {
                    HttpRuntime.Cache.Remove(str);
                    continue;
                }
                catch
                {
                    continue;
                }
            }
        }

        public static bool Contains<T>(string key)
        {
            object obj2 = HttpRuntime.Cache[GetPrivateCacheKey(key)];
            if (obj2 == null)
            {
                return false;
            }
            return (obj2 is T);
        }

        public static T Get<T>(string key)
        {
            return (T) HttpRuntime.Cache[GetPrivateCacheKey(key)];
        }

        private static DateTime GetAbsoluteExpiration(CacheTime cacheTime, CacheExpiresType cacheExpiresType)
        {
            if ((cacheTime == CacheTime.NotRemovable) || (cacheExpiresType == CacheExpiresType.Sliding))
            {
                return Cache.NoAbsoluteExpiration;
            }
            switch (cacheTime)
            {
                case CacheTime.Short:
                    return DateTime.Now.AddMinutes(2.0);

                case CacheTime.Long:
                    return DateTime.Now.AddMinutes(20.0);
            }
            return DateTime.Now.AddMinutes(5.0);
        }

        public static IList<T> GetBySearch<T>(string startText)
        {
            IList<T> list = new List<T>();
            foreach (DictionaryEntry entry in HttpRuntime.Cache)
            {
                string str = entry.Key.ToString();
                if (str.StartsWith(startText, StringComparison.OrdinalIgnoreCase))
                {
                    object obj2 = HttpRuntime.Cache[str];
                    if ((obj2 != null) && (obj2 is T))
                    {
                        list.Add((T) obj2);
                    }
                }
            }
            return list;
        }

        public static CacheDependency GetCacheDependencyFromKey(string key)
        {
            return new CacheDependency(null, new string[] { GetPrivateCacheKey(key) });
        }

        private static string GetPrivateCacheKey(string cacheKey)
        {
            return string.Format("{0}-UC", cacheKey);
        }

        private static TimeSpan GetSlidingExpiration(CacheTime cacheTime, CacheExpiresType cacheExpiresType)
        {
            if ((cacheTime == CacheTime.NotRemovable) || (cacheExpiresType == CacheExpiresType.Absolute))
            {
                return Cache.NoSlidingExpiration;
            }
            switch (cacheTime)
            {
                case CacheTime.Short:
                    return TimeSpan.FromMinutes(2.0);

                case CacheTime.Long:
                    return TimeSpan.FromMinutes(20.0);
            }
            return TimeSpan.FromMinutes(5.0);
        }

        public static void Remove(string key)
        {
            try
            {
                HttpRuntime.Cache.Remove(GetPrivateCacheKey(key));
            }
            catch
            {
            }
        }

        public static void RemoveBySearch(params string[] startTexts)
        {
            if ((startTexts != null) && (startTexts.Length != 0))
            {
                List<string> list = new List<string>();
                foreach (DictionaryEntry entry in HttpRuntime.Cache)
                {
                    string item = entry.Key.ToString();
                    foreach (string str2 in startTexts)
                    {
                        if (item.StartsWith(str2, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(item);
                        }
                    }
                }
                foreach (string str3 in list)
                {
                    try
                    {
                        HttpRuntime.Cache.Remove(str3);
                        continue;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        public static void RemoveBySearch(string startText)
        {
            if (!string.IsNullOrEmpty(startText))
            {
                List<string> list = new List<string>();
                foreach (DictionaryEntry entry in HttpRuntime.Cache)
                {
                    string item = entry.Key.ToString();
                    if (item.StartsWith(startText, StringComparison.OrdinalIgnoreCase))
                    {
                        list.Add(item);
                    }
                }
                foreach (string str2 in list)
                {
                    try
                    {
                        HttpRuntime.Cache.Remove(str2);
                        continue;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        public static void Set<T>(string key, T value)
        {
            Set<T>(key, value, CacheTime.Default, CacheExpiresType.Sliding, null, CacheItemPriority.NotRemovable, null);
        }

        public static void Set<T>(string key, T value, CacheTime cacheTime)
        {
            Set<T>(key, value, cacheTime, CacheExpiresType.Sliding, null, CacheItemPriority.NotRemovable, null);
        }

        public static void Set<T>(string key, T value, CacheTime cacheTime, CacheExpiresType cacheExpiresType)
        {
            Set<T>(key, value, cacheTime, cacheExpiresType, null, CacheItemPriority.NotRemovable, null);
        }

        public static void Set<T>(string key, T value, CacheTime cacheTime, CacheExpiresType cacheExpiresType, CacheDependency dependencies)
        {
            Set<T>(key, value, cacheTime, cacheExpiresType, dependencies, CacheItemPriority.NotRemovable, null);
        }

        public static void Set<T>(string key, T value, CacheTime cacheTime, CacheExpiresType cacheExpiresType, CacheDependency dependencies, CacheItemPriority cacheItemPriority)
        {
            Set<T>(key, value, cacheTime, cacheExpiresType, dependencies, cacheItemPriority, null);
        }

        public static void Set<T>(string key, T value, CacheTime cacheTime, CacheExpiresType cacheExpiresType, CacheDependency dependencies, CacheItemPriority cacheItemPriority, CacheItemRemovedCallback callback)
        {
            Add<T>(key, value, cacheTime, cacheExpiresType, dependencies, cacheItemPriority, callback);
        }

        public static bool TryGetValue<T>(string key, out T value)
        {
            key = GetPrivateCacheKey(key);
            object obj2 = HttpRuntime.Cache[key];
            if ((obj2 != null) && (obj2 is T))
            {
                value = (T) obj2;
                return true;
            }
            value = default(T);
            return false;
        }
    }
}

