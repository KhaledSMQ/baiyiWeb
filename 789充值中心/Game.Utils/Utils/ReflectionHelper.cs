namespace Game.Utils
{
    using System;
    using System.Reflection;

    public class ReflectionHelper
    {
        public static Type GetType(string typeAndAssName)
        {
            string[] strArray = typeAndAssName.Split(new char[] { ',' });
            if (strArray.Length < 2)
            {
                return Type.GetType(typeAndAssName);
            }
            return GetType(strArray[0].Trim(), strArray[1].Trim());
        }

        public static Type GetType(string typeFullName, string assemblyName)
        {
            if (assemblyName == null)
            {
                return Type.GetType(typeFullName);
            }
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly.FullName.Split(new char[] { ',' })[0].Trim() == assemblyName.Trim())
                {
                    return assembly.GetType(typeFullName);
                }
            }
            Assembly assembly2 = Assembly.Load(assemblyName);
            if (assembly2 != null)
            {
                return assembly2.GetType(typeFullName);
            }
            return null;
        }
    }
}

