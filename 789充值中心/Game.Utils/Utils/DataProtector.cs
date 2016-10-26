namespace Game.Utils
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DataProtector
    {
        private const int CRYPTPROTECT_LOCAL_MACHINE = 4;
        private const int CRYPTPROTECT_UI_FORBIDDEN = 1;
        private static IntPtr NullPtr = IntPtr.Zero;
        private Store store;

        public DataProtector(Store tempStore)
        {
            this.store = tempStore;
        }

        [DllImport("Crypt32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern bool CryptProtectData(ref DATA_BLOB pDataIn, string szDataDescr, ref DATA_BLOB pOptionalEntropy, IntPtr pvReserved, ref CRYPTPROTECT_PROMPTSTRUCT pPromptStruct, int dwFlags, ref DATA_BLOB pDataOut);
        [DllImport("Crypt32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern bool CryptUnprotectData(ref DATA_BLOB pDataIn, string szDataDescr, ref DATA_BLOB pOptionalEntropy, IntPtr pvReserved, ref CRYPTPROTECT_PROMPTSTRUCT pPromptStruct, int dwFlags, ref DATA_BLOB pDataOut);
        public string Decrypt(string cipherText)
        {
            byte[] bytes = this.Decrypt(Convert.FromBase64String(cipherText), null);
            return Encoding.UTF8.GetString(bytes);
        }

        public byte[] Decrypt(byte[] cipherText, byte[] optionalEntropy)
        {
            Exception exception;
            DATA_BLOB pDataOut = new DATA_BLOB();
            DATA_BLOB pDataIn = new DATA_BLOB();
            CRYPTPROTECT_PROMPTSTRUCT ps = new CRYPTPROTECT_PROMPTSTRUCT();
            this.InitPromptstruct(ref ps);
            try
            {
                int num2;
                try
                {
                    int length = cipherText.Length;
                    pDataIn.pbData = Marshal.AllocHGlobal(length);
                    if (IntPtr.Zero == pDataIn.pbData)
                    {
                        throw new Exception("Unable to allocate cipherText buffer.");
                    }
                    pDataIn.cbData = length;
                    Marshal.Copy(cipherText, 0, pDataIn.pbData, pDataIn.cbData);
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    throw new Exception("Exception marshalling data. " + exception.Message);
                }
                DATA_BLOB pOptionalEntropy = new DATA_BLOB();
                if (Store.Machine == this.store)
                {
                    num2 = 5;
                    if (null == optionalEntropy)
                    {
                        optionalEntropy = new byte[0];
                    }
                    try
                    {
                        int cb = optionalEntropy.Length;
                        pOptionalEntropy.pbData = Marshal.AllocHGlobal(cb);
                        if (IntPtr.Zero == pOptionalEntropy.pbData)
                        {
                            throw new Exception("Unable to allocate entropy buffer.");
                        }
                        pOptionalEntropy.cbData = cb;
                        Marshal.Copy(optionalEntropy, 0, pOptionalEntropy.pbData, cb);
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        throw new Exception("Exception entropy marshalling data. " + exception.Message);
                    }
                }
                else
                {
                    num2 = 1;
                }
                if (!CryptUnprotectData(ref pDataIn, null, ref pOptionalEntropy, IntPtr.Zero, ref ps, num2, ref pDataOut))
                {
                    throw new Exception("Decryption failed. " + GetErrorMessage(Marshal.GetLastWin32Error()));
                }
                if (IntPtr.Zero != pDataIn.pbData)
                {
                    Marshal.FreeHGlobal(pDataIn.pbData);
                }
                if (IntPtr.Zero != pOptionalEntropy.pbData)
                {
                    Marshal.FreeHGlobal(pOptionalEntropy.pbData);
                }
            }
            catch (Exception exception3)
            {
                exception = exception3;
                throw new Exception("Exception decrypting. " + exception.Message);
            }
            byte[] destination = new byte[pDataOut.cbData];
            Marshal.Copy(pDataOut.pbData, destination, 0, pDataOut.cbData);
            return destination;
        }

        public string Encrypt(string plainText)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(this.Encrypt(bytes, null));
        }

        public byte[] Encrypt(byte[] plainText, byte[] optionalEntropy)
        {
            Exception exception;
            DATA_BLOB pDataIn = new DATA_BLOB();
            DATA_BLOB pDataOut = new DATA_BLOB();
            DATA_BLOB pOptionalEntropy = new DATA_BLOB();
            CRYPTPROTECT_PROMPTSTRUCT ps = new CRYPTPROTECT_PROMPTSTRUCT();
            this.InitPromptstruct(ref ps);
            try
            {
                int num;
                int length;
                try
                {
                    length = plainText.Length;
                    pDataIn.pbData = Marshal.AllocHGlobal(length);
                    if (IntPtr.Zero == pDataIn.pbData)
                    {
                        throw new Exception("Unable to allocate plaintext buffer.");
                    }
                    pDataIn.cbData = length;
                    Marshal.Copy(plainText, 0, pDataIn.pbData, length);
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    throw new Exception("Exception marshalling data. " + exception.Message);
                }
                if (Store.Machine == this.store)
                {
                    num = 5;
                    if (null == optionalEntropy)
                    {
                        optionalEntropy = new byte[0];
                    }
                    try
                    {
                        length = optionalEntropy.Length;
                        pOptionalEntropy.pbData = Marshal.AllocHGlobal(optionalEntropy.Length);
                        if (IntPtr.Zero == pOptionalEntropy.pbData)
                        {
                            throw new Exception("Unable to allocate entropy data buffer.");
                        }
                        Marshal.Copy(optionalEntropy, 0, pOptionalEntropy.pbData, length);
                        pOptionalEntropy.cbData = length;
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        throw new Exception("Exception entropy marshalling data. " + exception.Message);
                    }
                }
                else
                {
                    num = 1;
                }
                if (!CryptProtectData(ref pDataIn, "", ref pOptionalEntropy, IntPtr.Zero, ref ps, num, ref pDataOut))
                {
                    throw new Exception("Encryption failed. " + GetErrorMessage(Marshal.GetLastWin32Error()));
                }
            }
            catch (Exception exception3)
            {
                exception = exception3;
                throw new Exception("Exception encrypting. " + exception.Message);
            }
            byte[] destination = new byte[pDataOut.cbData];
            Marshal.Copy(pDataOut.pbData, destination, 0, pDataOut.cbData);
            return destination;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern unsafe int FormatMessage(int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanguageId, ref string lpBuffer, int nSize, IntPtr* Arguments);
        private static unsafe string GetErrorMessage(int errorCode)
        {
            int num = 0x100;
            int num2 = 0x200;
            int num3 = 0x1000;
            int nSize = 0xff;
            string lpBuffer = "";
            int dwFlags = (num | num3) | num2;
            IntPtr lpSource = new IntPtr();
            IntPtr arguments = new IntPtr();
            int num6 = FormatMessage(dwFlags, ref lpSource, errorCode, 0, ref lpBuffer, nSize, &arguments);
            if (0 == num6)
            {
                throw new Exception("Failed to format message for error code " + errorCode + ". ");
            }
            return lpBuffer;
        }

        private void InitPromptstruct(ref CRYPTPROTECT_PROMPTSTRUCT ps)
        {
            ps.cbSize = Marshal.SizeOf(typeof(CRYPTPROTECT_PROMPTSTRUCT));
            ps.dwPromptFlags = 0;
            ps.hwndApp = NullPtr;
            ps.szPrompt = null;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
        internal struct CRYPTPROTECT_PROMPTSTRUCT
        {
            public int cbSize;
            public int dwPromptFlags;
            public IntPtr hwndApp;
            public string szPrompt;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
        internal struct DATA_BLOB
        {
            public int cbData;
            public IntPtr pbData;
        }
    }
}

