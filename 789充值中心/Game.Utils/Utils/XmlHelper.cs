namespace Game.Utils
{
    using System;
    using System.IO;

    public class XmlHelper
    {
        private XmlHelper()
        {
        }

        public static object LoadObjectFromXml(string path, Type type)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return XmlConvertor.XmlToObject(reader.ReadToEnd(), type);
            }
        }

        public static void SaveObjectToXml(string path, object obj)
        {
            string str = XmlConvertor.ObjectToXml(obj, true);
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(str);
            }
        }
    }
}

