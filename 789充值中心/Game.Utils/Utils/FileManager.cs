namespace Game.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;

    public abstract class FileManager
    {
        protected FileManager()
        {
        }

        public static void CopyDirectories(string srcDir, string desDir)
        {
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(srcDir);
                CopyDirectoryInfo(dInfo, srcDir, desDir);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        private static void CopyDirectoryInfo(DirectoryInfo dInfo, string srcDir, string desDir)
        {
            if (!Exists(desDir, FsoMethod.Folder))
            {
                Create(desDir, FsoMethod.Folder);
            }
            DirectoryInfo[] directories = dInfo.GetDirectories();
            foreach (DirectoryInfo info in directories)
            {
                CopyDirectoryInfo(info, info.FullName, desDir + info.FullName.Replace(srcDir, ""));
            }
            FileInfo[] files = dInfo.GetFiles();
            foreach (FileInfo info2 in files)
            {
                CopyFile(info2.FullName, desDir + info2.FullName.Replace(srcDir, ""));
            }
        }

        public static void CopyFile(string srcFile, string desFile)
        {
            try
            {
                File.Copy(srcFile, desFile, true);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        public static bool CopyFileStream(string srcFile, string desFile)
        {
            try
            {
                FileStream input = new FileStream(srcFile, FileMode.Open, FileAccess.Read);
                FileStream output = new FileStream(desFile, FileMode.Create, FileAccess.Write);
                BinaryReader reader = new BinaryReader(input);
                BinaryWriter writer = new BinaryWriter(output);
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                reader.BaseStream.Seek(0L, SeekOrigin.End);
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    writer.Write(reader.ReadByte());
                }
                reader.Close();
                writer.Close();
                input.Flush();
                input.Close();
                output.Flush();
                output.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Create(string file, FsoMethod method)
        {
            try
            {
                if (method == FsoMethod.File)
                {
                    WriteFile(file, "");
                }
                else if (method == FsoMethod.Folder)
                {
                    Directory.CreateDirectory(file);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        public static void Delete(string file, FsoMethod method)
        {
            try
            {
                if (method == FsoMethod.File)
                {
                    File.Delete(file);
                }
                if (method == FsoMethod.Folder)
                {
                    Directory.Delete(file, true);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        private static long[] DirInfo(DirectoryInfo directory)
        {
            long[] numArray = new long[3];
            long num = 0L;
            long num2 = 0L;
            long num3 = 0L;
            FileInfo[] files = directory.GetFiles();
            num3 += files.Length;
            foreach (FileInfo info in files)
            {
                num += info.Length;
            }
            DirectoryInfo[] directories = directory.GetDirectories();
            num2 += directories.Length;
            foreach (DirectoryInfo info2 in directories)
            {
                num += DirInfo(info2)[0];
                num2 += DirInfo(info2)[1];
                num3 += DirInfo(info2)[2];
            }
            numArray[0] = num;
            numArray[1] = num2;
            numArray[2] = num3;
            return numArray;
        }

        public static bool Exists(string file, FsoMethod method)
        {
            bool flag;
            try
            {
                if (method == FsoMethod.File)
                {
                    return File.Exists(file);
                }
                if (method == FsoMethod.Folder)
                {
                    return Directory.Exists(file);
                }
                flag = false;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
            return flag;
        }

        private static string[] GetDirectories(string directory)
        {
            return Directory.GetDirectories(directory);
        }

        public static DataTable GetDirectoryFilesList(string directory, FsoMethod method)
        {
            DataRow row;
            int num;
            DataTable table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("FullName");
            table.Columns.Add("ContentType");
            table.Columns.Add("Type");
            table.Columns.Add("Path");
            table.Columns.Add("LastWriteTime");
            table.Columns.Add("Length");
            if (method != FsoMethod.File)
            {
                for (num = 0; num < GetDirectories(directory).Length; num++)
                {
                    row = table.NewRow();
                    DirectoryInfo info = new DirectoryInfo(GetDirectories(directory)[num]);
                    row[0] = info.Name;
                    row[1] = info.FullName;
                    row[2] = "";
                    row[3] = 0;
                    row[4] = info.FullName.Replace(info.Name, "");
                    row[5] = info.LastWriteTime;
                    row[6] = "";
                    table.Rows.Add(row);
                }
            }
            if (method != FsoMethod.Folder)
            {
                for (num = 0; num < GetFiles(directory).Length; num++)
                {
                    row = table.NewRow();
                    FileInfo info2 = new FileInfo(GetFiles(directory)[num]);
                    row[0] = info2.Name;
                    row[1] = info2.FullName;
                    row[2] = info2.Extension.Replace(".", "");
                    row[3] = 1;
                    row[4] = info2.DirectoryName + @"\";
                    row[5] = info2.LastWriteTime;
                    row[6] = info2.Length;
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static IList<FolderInfo> GetDirectoryFilesListForObject(string directory, FsoMethod method)
        {
            return DataHelper.ConvertDataTableToObjects<FolderInfo>(GetDirectoryFilesList(directory, method));
        }

        public static long[] GetDirectoryInfo(string directory)
        {
            long[] numArray = new long[3];
            DirectoryInfo info = new DirectoryInfo(directory);
            return DirInfo(info);
        }

        private static DataTable GetDirectoryList(DirectoryInfo directoryInfo, FsoMethod method)
        {
            DataRow row;
            DataTable parent = new DataTable();
            parent.Columns.Add("Name");
            parent.Columns.Add("FullName");
            parent.Columns.Add("ContentType");
            parent.Columns.Add("Type");
            parent.Columns.Add("Path");
            parent.Columns.Add("LastWriteTime");
            parent.Columns.Add("Length");
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            foreach (DirectoryInfo info in directories)
            {
                if (method == FsoMethod.File)
                {
                    parent = Merge(parent, GetDirectoryList(info, method));
                }
                else
                {
                    row = parent.NewRow();
                    row[0] = info.Name;
                    row[1] = info.FullName;
                    row[2] = "";
                    row[3] = 0;
                    row[4] = info.FullName.Replace(info.Name, "");
                    row[5] = info.LastWriteTime;
                    row[6] = "";
                    parent.Rows.Add(row);
                    parent = Merge(parent, GetDirectoryList(info, method));
                }
            }
            if (method != FsoMethod.Folder)
            {
                FileInfo[] files = directoryInfo.GetFiles();
                foreach (FileInfo info2 in files)
                {
                    row = parent.NewRow();
                    row[0] = info2.Name;
                    row[1] = info2.FullName;
                    row[2] = info2.Extension.Replace(".", "");
                    row[3] = 1;
                    row[4] = info2.DirectoryName + @"\";
                    row[5] = info2.LastWriteTime;
                    row[6] = info2.Length;
                    parent.Rows.Add(row);
                }
            }
            return parent;
        }

        public static DataTable GetDirectoryList(string directory, FsoMethod method)
        {
            DataTable directoryList;
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                directoryList = GetDirectoryList(directoryInfo, method);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
            return directoryList;
        }

        public static IList<FolderInfo> GetDirectoryListForObject(string directory, FsoMethod method)
        {
            return DataHelper.ConvertDataTableToObjects<FolderInfo>(GetDirectoryList(directory, method));
        }

        private static string[] GetFiles(string directory)
        {
            return Directory.GetFiles(directory);
        }

        private static DataTable Merge(DataTable parent, DataTable child)
        {
            for (int i = 0; i < child.Rows.Count; i++)
            {
                DataRow row = parent.NewRow();
                for (int j = 0; j < parent.Columns.Count; j++)
                {
                    row[j] = child.Rows[i][j];
                }
                parent.Rows.Add(row);
            }
            return parent;
        }

        public static void Move(string srcFile, string desFile, FsoMethod method)
        {
            try
            {
                if (method == FsoMethod.File)
                {
                    File.Move(srcFile, desFile);
                }
                if (method == FsoMethod.Folder)
                {
                    Directory.Move(srcFile, desFile);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        public static string ReadFile(string file)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return ReadFile(file, encoding);
        }

        public static string ReadFile(string file, Encoding encoding)
        {
            string str = "";
            FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream, encoding);
            try
            {
                str = reader.ReadToEnd();
            }
            catch
            {
            }
            finally
            {
                stream.Flush();
                stream.Close();
                reader.Close();
            }
            return str;
        }

        public static byte[] ReadFileReturnBytes(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            FileStream input = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader reader = new BinaryReader(input);
            byte[] buffer = reader.ReadBytes((int) input.Length);
            input.Flush();
            input.Close();
            reader.Close();
            return buffer;
        }

        public static void WriteBuffToFile(byte[] buff, string filePath)
        {
            WriteBuffToFile(buff, 0, buff.Length, filePath);
        }

        public static void WriteBuffToFile(byte[] buff, int offset, int len, string filePath)
        {
            string directoryName = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            FileStream output = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(output);
            writer.Write(buff, offset, len);
            writer.Flush();
            writer.Close();
            output.Close();
        }

        public static void WriteFile(string file, string fileContent)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            WriteFile(file, fileContent, encoding);
        }

        public static void WriteFile(string file, string fileContent, bool append)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            WriteFile(file, fileContent, append, encoding);
        }

        public static void WriteFile(string file, string fileContent, Encoding encoding)
        {
            FileInfo info = new FileInfo(file);
            if (!Directory.Exists(info.DirectoryName))
            {
                Directory.CreateDirectory(info.DirectoryName);
            }
            FileStream stream = new FileStream(file, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream, encoding);
            try
            {
                writer.Write(fileContent);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
            finally
            {
                writer.Flush();
                stream.Flush();
                writer.Close();
                stream.Close();
            }
        }

        public static void WriteFile(string file, string fileContent, bool append, Encoding encoding)
        {
            FileInfo info = new FileInfo(file);
            if (!Directory.Exists(info.DirectoryName))
            {
                Directory.CreateDirectory(info.DirectoryName);
            }
            StreamWriter writer = new StreamWriter(file, append, encoding);
            try
            {
                writer.Write(fileContent);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
            finally
            {
                writer.Flush();
                writer.Close();
            }
        }
    }
}

