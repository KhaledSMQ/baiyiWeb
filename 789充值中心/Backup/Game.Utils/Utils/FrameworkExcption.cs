namespace Game.Utils
{
    using Game.Utils.Properties;
    using System;

    public class FrameworkExcption : Exception
    {
        public FrameworkExcption(string message) : base(GetException(message))
        {
        }

        public FrameworkExcption(string message, params string[] args) : base(GetException(message, args))
        {
        }

        internal static string GetException(string name)
        {
            return AppExceptions.ResourceManager.GetString(name);
        }

        internal static string GetException(string name, params string[] args)
        {
            return string.Format(AppExceptions.ResourceManager.GetString(name), (object[]) args);
        }
    }
}

