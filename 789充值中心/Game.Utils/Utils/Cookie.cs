namespace Game.Utils
{
    using System;
    using System.Web;

    public class Cookie
    {
        public static HttpCookie Get(string name)
        {
            string str = ApplicationSettings.Get("AppPrefix");
            return HttpContext.Current.Request.Cookies[str + name];
        }

        public static void Remove(string name)
        {
            Remove(Get(name));
        }

        public static void Remove(HttpCookie cookie)
        {
            if (cookie != null)
            {
                cookie.Expires = new DateTime(0x7bf, 5, 0x15);
                Save(cookie);
            }
        }

        public static void Save(HttpCookie cookie)
        {
            string serverDomain = Utility.ServerDomain;
            string str2 = HttpContext.Current.Request.Url.Host.ToLower();
            if (serverDomain != str2)
            {
                cookie.Domain = serverDomain;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static HttpCookie Set(string name)
        {
            return new HttpCookie(ApplicationSettings.Get("AppPrefix") + name);
        }
    }
}

