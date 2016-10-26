namespace Game.Utils
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml.Serialization;

    public class SerializationHelper
    {
        private SerializationHelper()
        {
        }

        public static object Deserialize(byte[] buffer)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream(buffer, 0, buffer.Length, false);
            object obj2 = formatter.Deserialize(serializationStream);
            serializationStream.Close();
            return obj2;
        }

        public static object Deserialize(Type type, string filename)
        {
            FileStream stream = null;
            object obj2;
            try
            {
                stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                obj2 = new XmlSerializer(type).Deserialize(stream);
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
            return obj2;
        }

        public static byte[] Serialize<T>(T t)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream(0x2800);
            formatter.Serialize(serializationStream, t);
            serializationStream.Seek(0L, SeekOrigin.Begin);
            byte[] buffer = new byte[(int) serializationStream.Length];
            serializationStream.Read(buffer, 0, buffer.Length);
            serializationStream.Close();
            return buffer;
        }

        public static void Serialize<T>(T t, string filename)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                new XmlSerializer(t.GetType()).Serialize((Stream) stream, t);
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
        }
    }
}

