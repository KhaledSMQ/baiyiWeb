namespace Game.Utils
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class DES
    {
        private static byte[] Keys = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };

        public static string Decrypt(string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = TextUtility.CutLeft(decryptKey, 8);
                decryptKey = decryptKey.PadRight(8, ' ');
                byte[] bytes = Encoding.UTF8.GetBytes(decryptKey);
                byte[] keys = Keys;
                byte[] buffer = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(bytes, keys), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
            catch
            {
                return "";
            }
        }

        public static string Encrypt(string encryptString, string encryptKey)
        {
            encryptKey = TextUtility.CutLeft(encryptKey, 8);
            encryptKey = encryptKey.PadRight(8, ' ');
            byte[] bytes = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] keys = Keys;
            byte[] buffer = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(bytes, keys), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            return Convert.ToBase64String(stream.ToArray());
        }
    }
}

