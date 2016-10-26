namespace Game.Utils
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public sealed class IPQuery
    {
        private static bool fileIsExsit = true;
        private static string filePath = "";
        private static PHCZIP pcz = new PHCZIP();

        static IPQuery()
        {
            filePath = TextUtility.GetFullPath(Utility.GetIPDbFilePath);
            fileIsExsit = FileManager.Exists(filePath, FsoMethod.File);
            if (!fileIsExsit)
            {
                throw new FrameworkExcption("IPdataFileNotExists");
            }
            pcz.SetDbFilePath(filePath);
        }

        public static string GetAddressWithIP(string IPValue)
        {
            string addressWithIP = pcz.GetAddressWithIP(IPValue.Trim());
            if (fileIsExsit)
            {
                if (addressWithIP.IndexOf("IANA") >= 0)
                {
                    return "";
                }
                return addressWithIP;
            }
            return null;
        }

        public class CZ_INDEX_INFO
        {
            public uint IpEnd = 0;
            public uint IpSet = 0;
            public uint Offset = 0;
        }

        public class PHCZIP
        {
            protected bool bFilePathInitialized;
            protected string FilePath;
            protected FileStream FileStrm;
            protected uint Index_Count;
            protected uint Index_End;
            protected uint Index_Set;
            protected IPQuery.CZ_INDEX_INFO Search_End;
            protected uint Search_Index_End;
            protected uint Search_Index_Set;
            protected IPQuery.CZ_INDEX_INFO Search_Mid;
            protected IPQuery.CZ_INDEX_INFO Search_Set;

            public PHCZIP()
            {
                this.bFilePathInitialized = false;
            }

            public PHCZIP(string dbFilePath)
            {
                this.bFilePathInitialized = false;
                this.SetDbFilePath(dbFilePath);
            }

            public void Dispose()
            {
                if (this.bFilePathInitialized)
                {
                    this.bFilePathInitialized = false;
                    this.FileStrm.Close();
                }
            }

            public string GetAddressWithIP(string IPValue)
            {
                if (!this.bFilePathInitialized)
                {
                    return "";
                }
                this.Initialize();
                uint num = this.IPToUInt32(IPValue);
                while (true)
                {
                    this.Search_Set = this.IndexInfoAtPos(this.Search_Index_Set);
                    this.Search_End = this.IndexInfoAtPos(this.Search_Index_End);
                    if ((num >= this.Search_Set.IpSet) && (num <= this.Search_Set.IpEnd))
                    {
                        return this.ReadAddressInfoAtOffset(this.Search_Set.Offset);
                    }
                    if ((num >= this.Search_End.IpSet) && (num <= this.Search_End.IpEnd))
                    {
                        return this.ReadAddressInfoAtOffset(this.Search_End.Offset);
                    }
                    this.Search_Mid = this.IndexInfoAtPos((this.Search_Index_End + this.Search_Index_Set) / 2);
                    if ((num >= this.Search_Mid.IpSet) && (num <= this.Search_Mid.IpEnd))
                    {
                        return this.ReadAddressInfoAtOffset(this.Search_Mid.Offset);
                    }
                    if (num < this.Search_Mid.IpSet)
                    {
                        this.Search_Index_End = (this.Search_Index_End + this.Search_Index_Set) / 2;
                    }
                    else
                    {
                        this.Search_Index_Set = (this.Search_Index_End + this.Search_Index_Set) / 2;
                    }
                }
            }

            private uint GetOffset()
            {
                return BitConverter.ToUInt32(new byte[] { (byte) this.FileStrm.ReadByte(), (byte) this.FileStrm.ReadByte(), (byte) this.FileStrm.ReadByte(), 0 }, 0);
            }

            protected byte GetTag()
            {
                return (byte) this.FileStrm.ReadByte();
            }

            protected uint GetUInt32()
            {
                byte[] buffer = new byte[4];
                this.FileStrm.Read(buffer, 0, 4);
                return BitConverter.ToUInt32(buffer, 0);
            }

            protected IPQuery.CZ_INDEX_INFO IndexInfoAtPos(uint Index_Pos)
            {
                IPQuery.CZ_INDEX_INFO cz_index_info = new IPQuery.CZ_INDEX_INFO();
                this.FileStrm.Seek((long) (this.Index_Set + (7 * Index_Pos)), SeekOrigin.Begin);
                cz_index_info.IpSet = this.GetUInt32();
                cz_index_info.Offset = this.GetOffset();
                this.FileStrm.Seek((long) cz_index_info.Offset, SeekOrigin.Begin);
                cz_index_info.IpEnd = this.GetUInt32();
                return cz_index_info;
            }

            public void Initialize()
            {
                this.Search_Index_Set = 0;
                this.Search_Index_End = this.Index_Count - 1;
            }

            public uint IPToUInt32(string IpValue)
            {
                int num2;
                string[] strArray = IpValue.Split(new char[] { '.' });
                int upperBound = strArray.GetUpperBound(0);
                if (upperBound != 3)
                {
                    strArray = new string[4];
                    for (num2 = 1; num2 <= (3 - upperBound); num2++)
                    {
                        strArray[upperBound + num2] = "0";
                    }
                }
                byte[] buffer = new byte[4];
                for (num2 = 0; num2 <= 3; num2++)
                {
                    if (this.IsNumeric(strArray[num2]))
                    {
                        buffer[3 - num2] = (byte) (Convert.ToInt32(strArray[num2]) & 0xff);
                    }
                }
                return BitConverter.ToUInt32(buffer, 0);
            }

            protected bool IsNumeric(string str)
            {
                return ((str != null) && Regex.IsMatch(str, @"^-?\d+$"));
            }

            private string ReadAddressInfoAtOffset(uint Offset)
            {
                string str = "";
                string str2 = "";
                uint offset = 0;
                byte tag = 0;
                this.FileStrm.Seek((long) (Offset + 4), SeekOrigin.Begin);
                tag = this.GetTag();
                if (tag == 1)
                {
                    this.FileStrm.Seek((long) this.GetOffset(), SeekOrigin.Begin);
                    if (this.GetTag() == 2)
                    {
                        offset = this.GetOffset();
                        str2 = this.ReadArea();
                        this.FileStrm.Seek((long) offset, SeekOrigin.Begin);
                        str = this.ReadString();
                    }
                    else
                    {
                        this.FileStrm.Seek(-1L, SeekOrigin.Current);
                        str = this.ReadString();
                        str2 = this.ReadArea();
                    }
                }
                else if (tag == 2)
                {
                    offset = this.GetOffset();
                    str2 = this.ReadArea();
                    this.FileStrm.Seek((long) offset, SeekOrigin.Begin);
                    str = this.ReadString();
                }
                else
                {
                    this.FileStrm.Seek(-1L, SeekOrigin.Current);
                    str = this.ReadString();
                    str2 = this.ReadArea();
                }
                return (str + " " + str2);
            }

            protected string ReadArea()
            {
                switch (this.GetTag())
                {
                    case 1:
                    case 2:
                        this.FileStrm.Seek((long) this.GetOffset(), SeekOrigin.Begin);
                        return this.ReadString();
                }
                this.FileStrm.Seek(-1L, SeekOrigin.Current);
                return this.ReadString();
            }

            protected string ReadString()
            {
                uint index = 0;
                byte[] bytes = new byte[0x100];
                bytes[index] = (byte) this.FileStrm.ReadByte();
                while (bytes[index] != 0)
                {
                    index++;
                    bytes[index] = (byte) this.FileStrm.ReadByte();
                }
                return Encoding.Default.GetString(bytes).TrimEnd(new char[1]);
            }

            public bool SetDbFilePath(string dbFilePath)
            {
                if (dbFilePath == "")
                {
                    return false;
                }
                try
                {
                    this.FileStrm = new FileStream(dbFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                catch
                {
                    return false;
                }
                if (this.FileStrm.Length < 8L)
                {
                    this.FileStrm.Close();
                    return false;
                }
                this.FileStrm.Seek(0L, SeekOrigin.Begin);
                this.Index_Set = this.GetUInt32();
                this.Index_End = this.GetUInt32();
                this.Index_Count = ((this.Index_End - this.Index_Set) / 7) + 1;
                this.bFilePathInitialized = true;
                return true;
            }
        }
    }
}

