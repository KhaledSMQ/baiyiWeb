namespace Game.Utils
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    public class ApplicationEnvironment
    {
        public static string GetAssemblyCopyright()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).LegalCopyright;
        }

        public static string GetAssemblyFullName(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type 不能为空");
            }
            return type.Assembly.GetName().FullName;
        }

        public static string GetAssemblyProductName()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductName;
        }

        public static string GetAssemblyVersion()
        {
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            return string.Format("{0}.{1}.{2}", versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart);
        }

        public static string GetAssemblyVersion(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return type.Assembly.GetName().Version.ToString();
        }

        public static Guid GetInterfaceGuid(Type type)
        {
            return type.GUID;
        }

        public static Version GetInterfaceVersion(Type type)
        {
            return type.Assembly.GetName().Version;
        }

        public static bool InterfaceGuidCompare(Guid queryGuid, Type interfaceType)
        {
            return queryGuid.Equals(interfaceType.GUID);
        }

        public static bool InterfaceVersionCompare(Version queryVer, Version interfaceVer)
        {
            if (queryVer.Major != interfaceVer.Major)
            {
                return false;
            }
            if (queryVer.Minor > interfaceVer.Minor)
            {
                return false;
            }
            return true;
        }

        public static string ApplicationName
        {
            get
            {
                return GetAssemblyProductName();
            }
        }
    }
}

