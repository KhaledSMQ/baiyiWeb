namespace Game.Utils
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class TextEncrypt
    {
        private TextEncrypt()
        {
        }

        public static string Base64Decode(string message)
        {
            byte[] bytes = Convert.FromBase64String(message);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string Base64Encode(string message)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(message));
        }

        public static string DSAEncryptPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            DSACryptoServiceProvider provider = new DSACryptoServiceProvider();
            string str = BitConverter.ToString(provider.SignData(Encoding.UTF8.GetBytes(password)));
            provider.Clear();
            return str.Replace("-", null);
        }

        public static string EncryptPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            return MD5EncryptPassword(password);
        }

        public static string MD5EncryptPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            return MD5EncryptPassword(password, MD5ResultMode.Strong);
        }

        public static string MD5EncryptPassword(string password, MD5ResultMode mode)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            string str = BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(password)));
            provider.Clear();
            if (mode != MD5ResultMode.Strong)
            {
                return str.Replace("-", null).Substring(8, 0x10);
            }
            return str.Replace("-", null);
        }

        public static string SHA1EncryptPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider();
            string str = BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(password)));
            provider.Clear();
            return str.Replace("-", null);
        }

        public static string SHA256(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            SHA256Managed managed = new SHA256Managed();
            return Convert.ToBase64String(managed.ComputeHash(bytes));
        }
    }
}

