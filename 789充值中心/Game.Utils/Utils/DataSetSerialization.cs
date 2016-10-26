namespace Game.Utils
{
    using System;
    using System.Data;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public sealed class DataSetSerialization
    {
        private DataSetSerialization()
        {
        }

        public static DataSet Load(string filename)
        {
            FileStream serializationStream = null;
            DataSet set = null;
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                serializationStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                set = formatter.Deserialize(serializationStream) as DataSet;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (serializationStream != null)
                {
                    serializationStream.Flush();
                    serializationStream.Close();
                }
            }
            return set;
        }

        public static void Save(DataSet serialDataSet, string filename)
        {
            FileStream stream = null;
            StreamWriter writer = null;
            BinaryFormatter formatter = new BinaryFormatter();
            serialDataSet.RemotingFormat = SerializationFormat.Binary;
            try
            {
                stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                writer = new StreamWriter(stream);
                formatter.Serialize(writer.BaseStream, serialDataSet);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (stream != null)
                {
                    writer.Flush();
                    stream.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
        }
    }
}

