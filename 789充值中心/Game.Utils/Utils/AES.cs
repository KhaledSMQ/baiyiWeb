namespace Game.Utils
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public sealed class AES
    {
        private static byte[] Keys = new byte[] { 0x41, 0x72, 0x65, 0x79, 0x6f, 0x75, 0x6d, 0x79, 0x53, 110, 0x6f, 0x77, 0x6d, 0x61, 110, 0x3f };

        private AES()
        {
        }

        public static string Decrypt(string cipherText, string cipherkey)
        {
            try
            {
                cipherkey = TextUtility.CutLeft(cipherkey, 0x20);
                cipherkey = cipherkey.PadRight(0x20, ' ');
                ICryptoTransform transform = new RijndaelManaged { Key = Encoding.UTF8.GetBytes(cipherkey), IV = Keys }.CreateDecryptor();
                byte[] inputBuffer = Convert.FromBase64String(cipherText);
                byte[] bytes = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return "";
            }
        }

        public static byte[] DecryptBuffer(byte[] cipherText, string cipherkey)
        {
            try
            {
                cipherkey = TextUtility.CutLeft(cipherkey, 0x20);
                cipherkey = cipherkey.PadRight(0x20, ' ');
                RijndaelManaged managed = new RijndaelManaged {
                    Key = Encoding.UTF8.GetBytes(cipherkey),
                    IV = Keys
                };
                return managed.CreateDecryptor().TransformFinalBlock(cipherText, 0, cipherText.Length);
            }
            catch
            {
                return null;
            }
        }

        public static string Encrypt(string plainText, string cipherkey)
        {
            cipherkey = TextUtility.CutLeft(cipherkey, 0x20);
            cipherkey = cipherkey.PadRight(0x20, ' ');
            ICryptoTransform transform = new RijndaelManaged { Key = Encoding.UTF8.GetBytes(cipherkey.Substring(0, 0x20)), IV = Keys }.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
        }

        public static byte[] EncryptBuffer(byte[] plainText, string cipherkey)
        {
            cipherkey = TextUtility.CutLeft(cipherkey, 0x20);
            cipherkey = cipherkey.PadRight(0x20, ' ');
            RijndaelManaged managed = new RijndaelManaged {
                Key = Encoding.UTF8.GetBytes(cipherkey.Substring(0, 0x20)),
                IV = Keys
            };
            return managed.CreateEncryptor().TransformFinalBlock(plainText, 0, plainText.Length);
        }
    }
}

