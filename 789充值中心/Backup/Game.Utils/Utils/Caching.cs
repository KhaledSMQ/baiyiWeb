namespace Game.Utils
{
    using System;
    using System.Web;
    using System.Web.Caching;

    public class Caching
    {
        public static object Get(string name)
        {
            string str = ApplicationSettings.Get("AppPrefix");
            if (null != HttpContext.Current)
            {
                return HttpContext.Current.Cache.Get(str + name);
            }
            return HttpRuntime.Cache.Get(str + name);
        }

        public static T Get<T>(string key)
        {
            object obj2 = Get(key);
            if (obj2 == null)
            {
                return default(T);
            }
            return (T) obj2;
        }

        public static void Remove(string name)
        {
            string str = ApplicationSettings.Get("AppPrefix");
            object obj2 = HttpContext.Current.Cache[str + name];
            if (obj2 != null)
            {
                HttpContext.Current.Cache.Remove(str + name);
            }
        }

        public static void Set(string name, object value, CacheDependency cacheDependency)
        {
            Set(name, value, cacheDependency, DateTime.Now.AddSeconds(20.0), TimeSpan.Zero);
        }

        public static void Set(string name, object value, CacheDependency cacheDependency, DateTime dt)
        {
            Set(name, value, cacheDependency, dt, TimeSpan.Zero);
        }

        public static void Set(string name, object value, CacheDependency cacheDependency, TimeSpan ts)
        {
            Set(name, value, cacheDependency, Cache.NoAbsoluteExpiration, ts);
        }

        public static void Set(string name, object value, CacheDependency cacheDependency, DateTime dt, TimeSpan ts)
        {
            string str = ApplicationSettings.Get("AppPrefix");
            HttpRuntime.Cache.Insert(str + name, value, cacheDependency, DateTime.Now.AddSeconds(10.0), Cache.NoSlidingExpiration);
        }
    }
}

