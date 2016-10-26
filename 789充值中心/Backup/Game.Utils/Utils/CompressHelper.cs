namespace Game.Utils
{
    using System;
    using System.IO;
    using System.IO.Compression;

    public class CompressHelper
    {
        public static byte[] DeflateCompress(byte[] data)
        {
            MemoryStream stream = new MemoryStream();
            DeflateStream stream2 = new DeflateStream(stream, CompressionMode.Compress, true);
            stream2.Write(data, 0, data.Length);
            stream2.Close();
            stream2.Dispose();
            stream2 = null;
            byte[] buffer = ReadStreamToBytes(stream);
            stream.Close();
            stream.Dispose();
            stream = null;
            return buffer;
        }

        public static byte[] DeflateDecompress(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            DeflateStream stream2 = new DeflateStream(stream, CompressionMode.Decompress);
            byte[] buffer = ReadStreamToBytes(stream2);
            stream.Close();
            stream.Dispose();
            stream = null;
            stream2.Close();
            stream2.Dispose();
            stream2 = null;
            return buffer;
        }

        public static byte[] ReadStreamToBytes(Stream stream)
        {
            byte[] buffer = new byte[0x10000];
            int count = 0;
            int num2 = 0;
            long position = -1L;
            if (stream.CanSeek)
            {
                position = stream.Position;
                stream.Position = 0L;
            }
            MemoryStream stream2 = new MemoryStream();
            while (true)
            {
                count = stream.Read(buffer, 0, buffer.Length);
                if (count > 0)
                {
                    num2 += count;
                    stream2.Write(buffer, 0, count);
                }
                else
                {
                    byte[] buffer2 = new byte[num2];
                    stream2.Position = 0L;
                    stream2.Read(buffer2, 0, num2);
                    stream2.Close();
                    stream2 = null;
                    if (position >= 0L)
                    {
                        stream.Position = position;
                    }
                    return buffer2;
                }
            }
        }
    }
}

