namespace Game.Utils
{
    using System;
    using System.Configuration;

    public class ApplicationSettings
    {
        public static string Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }
            string str = ConfigurationManager.AppSettings[key];
            if (str == null)
            {
                throw new FrameworkExcption("WebConfigHasNotAddKey", new string[] { key });
            }
            return str;
        }
    }
}

