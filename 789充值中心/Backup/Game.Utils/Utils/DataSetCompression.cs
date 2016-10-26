namespace Game.Utils
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;

    public sealed class DataSetCompression
    {
        private DataSetCompression()
        {
        }

        public static byte[] CompressDataSet(DataSet ds)
        {
            ds.RemotingFormat = SerializationFormat.Binary;
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream();
            formatter.Serialize(serializationStream, ds);
            byte[] buffer = serializationStream.ToArray();
            MemoryStream stream = new MemoryStream();
            DeflateStream stream3 = new DeflateStream(stream, CompressionMode.Compress);
            stream3.Write(buffer, 0, buffer.Length);
            stream3.Flush();
            stream3.Close();
            return stream.ToArray();
        }

        public static DataSet DecompressDataSet(byte[] bytDs, out int len)
        {
            Debug.Write(bytDs.Length.ToString());
            DataSet set = new DataSet();
            MemoryStream stream = new MemoryStream(bytDs);
            stream.Seek(0L, SeekOrigin.Begin);
            DeflateStream stream2 = new DeflateStream(stream, CompressionMode.Decompress, true);
            byte[] buffer = ReadFullStream(stream2);
            stream2.Flush();
            stream2.Close();
            MemoryStream serializationStream = new MemoryStream(buffer);
            serializationStream.Seek(0L, SeekOrigin.Begin);
            set.RemotingFormat = SerializationFormat.Binary;
            BinaryFormatter formatter = new BinaryFormatter();
            len = (int) serializationStream.Length;
            return (DataSet) formatter.Deserialize(serializationStream, null);
        }

        public static byte[] ReadFullStream(Stream stream)
        {
            byte[] buffer = new byte[0x8000];
            using (MemoryStream stream2 = new MemoryStream())
            {
                int num;
                bool flag;
                goto Label_0040;
            Label_0015:
                num = stream.Read(buffer, 0, buffer.Length);
                if (num <= 0)
                {
                    return stream2.ToArray();
                }
                stream2.Write(buffer, 0, num);
            Label_0040:
                flag = true;
                goto Label_0015;
            }
        }
    }
}

