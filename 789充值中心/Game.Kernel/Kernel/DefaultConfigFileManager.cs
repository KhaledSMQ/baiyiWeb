namespace Game.Kernel
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    public class DefaultConfigFileManager
    {
        private static string m_configfilepath;
        private static IConfigInfo m_configinfo = null;
        private static object m_lockHelper = new object();

        public static IConfigInfo DeserializeInfo(string configfilepath, Type configtype)
        {
            IConfigInfo info;
            FileStream stream = null;
            try
            {
                stream = new FileStream(configfilepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(configtype);
                info = (IConfigInfo) serializer.Deserialize(stream);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return info;
        }

        protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigInfo configinfo)
        {
            return LoadConfig(ref fileoldchange, configFilePath, configinfo, true);
        }

        protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigInfo configinfo, bool checkTime)
        {
            object obj2;
            m_configfilepath = configFilePath;
            IConfigInfo info = configinfo;
            if (checkTime)
            {
                DateTime lastWriteTime = File.GetLastWriteTime(configFilePath);
                if (fileoldchange != lastWriteTime)
                {
                    fileoldchange = lastWriteTime;
                    lock ((obj2 = m_lockHelper))
                    {
                        info = DeserializeInfo(configFilePath, configinfo.GetType());
                    }
                }
                return info;
            }
            lock ((obj2 = m_lockHelper))
            {
                return DeserializeInfo(configFilePath, configinfo.GetType());
            }
        }

        public virtual bool SaveConfig()
        {
            return true;
        }

        public bool SaveConfig(string configFilePath, IConfigInfo configinfo)
        {
            bool flag = false;
            FileStream stream = null;
            try
            {
                stream = new FileStream(configFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                new XmlSerializer(configinfo.GetType()).Serialize((Stream) stream, configinfo);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return flag;
        }

        public static string ConfigFilePath
        {
            get
            {
                return m_configfilepath;
            }
            set
            {
                m_configfilepath = value;
            }
        }

        public static IConfigInfo ConfigInfo
        {
            get
            {
                return m_configinfo;
            }
            set
            {
                m_configinfo = value;
            }
        }
    }
}

