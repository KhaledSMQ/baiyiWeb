namespace Game.Utils
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class INIReader
    {
        private readonly string iniPath = string.Empty;

        public INIReader(string iniPath)
        {
            this.iniPath = iniPath;
        }

        public void ClearAllSection()
        {
            this.IniWriteValue(null, null, null);
        }

        public void ClearSection(string Section)
        {
            this.IniWriteValue(Section, null, null);
        }

        [DllImport("kernel32")]
        private static extern long GetPrivateProfileSection(string section, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder retVal = new StringBuilder(0xff);
            int num = GetPrivateProfileString(Section, Key, "", retVal, 0xff, this.iniPath);
            return retVal.ToString();
        }

        public string IniSectionReadValue(string Section)
        {
            StringBuilder retVal = new StringBuilder(0xff);
            long num = GetPrivateProfileSection(Section, retVal, 0xff, this.iniPath);
            return retVal.ToString();
        }

        public void IniSectionWriteValue(string Section, StringBuilder str)
        {
            WritePrivateProfileSection(Section, str, this.iniPath);
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.iniPath);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileSection(string section, StringBuilder val, string filePath);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}

