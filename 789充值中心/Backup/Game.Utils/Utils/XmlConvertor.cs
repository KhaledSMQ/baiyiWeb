namespace Game.Utils
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class XmlConvertor
    {
        public static string ObjectToXml(object obj, bool toBeIndented)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            UTF8Encoding encoding = new UTF8Encoding(false);
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            MemoryStream w = new MemoryStream();
            XmlTextWriter writer2 = new XmlTextWriter(w, encoding) {
                Formatting = toBeIndented ? Formatting.Indented : Formatting.None
            };
            XmlTextWriter writer = writer2;
            try
            {
                serializer.Serialize((XmlWriter) writer, obj);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Can not convert object to xml.");
            }
            finally
            {
                writer.Close();
            }
            return encoding.GetString(w.ToArray());
        }

        public static object XmlToObject(string xml, Type type)
        {
            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            object obj2 = null;
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader input = new StringReader(xml);
            XmlReader xmlReader = new XmlTextReader(input);
            try
            {
                obj2 = serializer.Deserialize(xmlReader);
            }
            catch (InvalidOperationException exception)
            {
                throw new InvalidOperationException("Can not convert xml to object", exception);
            }
            finally
            {
                xmlReader.Close();
            }
            return obj2;
        }
    }
}

