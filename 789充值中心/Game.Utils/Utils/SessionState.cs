namespace Game.Utils
{
    using System;
    using System.Web;

    public class SessionState
    {
        public static object Get(string name)
        {
            string str = ApplicationSettings.Get("AppPrefix");
            return HttpContext.Current.Session[str + name];
        }

        public static void Remove(string name)
        {
            string str = ApplicationSettings.Get("AppPrefix");
            if (HttpContext.Current.Session[str + name] != null)
            {
                HttpContext.Current.Session.Remove(str + name);
            }
        }

        public static void RemoveAll()
        {
            HttpContext.Current.Session.RemoveAll();
        }

        public static void Set(string name, object value)
        {
            string str = ApplicationSettings.Get("AppPrefix");
            HttpContext.Current.Session.Add(str + name, value);
        }
    }
}

