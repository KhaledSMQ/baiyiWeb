namespace Game.Utils
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;

    public class FTP
    {
        private string bucket;
        private long bytes_total;
        private IPEndPoint data_ipEndPoint;
        private Socket data_sock;
        public string errormessage;
        private FileStream file;
        private long file_size;
        private Socket listening_sock;
        private IPEndPoint main_ipEndPoint;
        private Socket main_sock;
        private string messages;
        public string pass;
        private bool passive_mode;
        public int port;
        private int response;
        private string responseStr;
        public string server;
        public int timeout;
        public string user;

        public FTP()
        {
            this.server = null;
            this.user = null;
            this.pass = null;
            this.port = 0x15;
            this.passive_mode = true;
            this.main_sock = null;
            this.main_ipEndPoint = null;
            this.listening_sock = null;
            this.data_sock = null;
            this.data_ipEndPoint = null;
            this.file = null;
            this.bucket = "";
            this.bytes_total = 0L;
            this.timeout = 0x2710;
            this.messages = "";
            this.errormessage = "";
        }

        public FTP(string server, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = 0x15;
            this.passive_mode = true;
            this.main_sock = null;
            this.main_ipEndPoint = null;
            this.listening_sock = null;
            this.data_sock = null;
            this.data_ipEndPoint = null;
            this.file = null;
            this.bucket = "";
            this.bytes_total = 0L;
            this.timeout = 0x2710;
            this.messages = "";
            this.errormessage = "";
        }

        public FTP(string server, int port, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = port;
            this.passive_mode = true;
            this.main_sock = null;
            this.main_ipEndPoint = null;
            this.listening_sock = null;
            this.data_sock = null;
            this.data_ipEndPoint = null;
            this.file = null;
            this.bucket = "";
            this.bytes_total = 0L;
            this.timeout = 0x2710;
            this.messages = "";
            this.errormessage = "";
        }

        public FTP(string server, int port, string user, string pass, int mode)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = port;
            this.passive_mode = mode <= 1;
            this.main_sock = null;
            this.main_ipEndPoint = null;
            this.listening_sock = null;
            this.data_sock = null;
            this.data_ipEndPoint = null;
            this.file = null;
            this.bucket = "";
            this.bytes_total = 0L;
            this.timeout = 0x2710;
            this.messages = "";
            this.errormessage = "";
        }

        public FTP(string server, int port, string user, string pass, int mode, int timeout_sec)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = port;
            this.passive_mode = mode <= 1;
            this.main_sock = null;
            this.main_ipEndPoint = null;
            this.listening_sock = null;
            this.data_sock = null;
            this.data_ipEndPoint = null;
            this.file = null;
            this.bucket = "";
            this.bytes_total = 0L;
            this.timeout = (timeout_sec <= 0) ? 0x7fffffff : (timeout_sec * 0x3e8);
            this.messages = "";
            this.errormessage = "";
        }

        public bool ChangeDir(string path)
        {
            this.Connect();
            this.SendCommand("CWD " + path);
            this.ReadResponse();
            if (this.response != 250)
            {
                this.errormessage = this.errormessage + this.responseStr;
                return false;
            }
            return true;
        }

        private void CloseDataSocket()
        {
            if (this.data_sock != null)
            {
                if (this.data_sock.Connected)
                {
                    this.data_sock.Close();
                }
                this.data_sock = null;
            }
            this.data_ipEndPoint = null;
        }

        public bool Connect()
        {
            if (this.server == null)
            {
                this.errormessage = this.errormessage + "No server has been set.\r\n";
            }
            if (this.user == null)
            {
                this.errormessage = this.errormessage + "No server has been set.\r\n";
            }
            if ((this.main_sock == null) || !this.main_sock.Connected)
            {
                try
                {
                    this.main_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    this.main_ipEndPoint = new IPEndPoint(Dns.GetHostEntry(this.server).AddressList[0], this.port);
                    this.main_sock.Connect(this.main_ipEndPoint);
                }
                catch (Exception exception)
                {
                    this.errormessage = this.errormessage + exception.Message;
                    return false;
                }
                this.ReadResponse();
                if (this.response != 220)
                {
                    this.Fail();
                }
                this.SendCommand("USER " + this.user);
                this.ReadResponse();
                int response = this.response;
                if ((response != 230) && (response == 0x14b))
                {
                    if (this.pass == null)
                    {
                        this.Disconnect();
                        this.errormessage = this.errormessage + "No password has been set.";
                        return false;
                    }
                    this.SendCommand("PASS " + this.pass);
                    this.ReadResponse();
                    if (this.response != 230)
                    {
                        this.Fail();
                        return false;
                    }
                }
            }
            return true;
        }

        public void Connect(string server, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.Connect();
        }

        public void Connect(string server, int port, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = port;
            this.Connect();
        }

        private void ConnectDataSocket()
        {
            if (this.data_sock == null)
            {
                try
                {
                    this.data_sock = this.listening_sock.Accept();
                    this.listening_sock.Close();
                    this.listening_sock = null;
                    if (this.data_sock == null)
                    {
                        throw new Exception("Winsock error: " + Convert.ToString(Marshal.GetLastWin32Error()));
                    }
                }
                catch (Exception exception)
                {
                    this.errormessage = this.errormessage + "Failed to connect for data transfer: " + exception.Message;
                }
            }
        }

        private DateTime ConvertFTPDateToDateTime(string input)
        {
            if (input.Length < 14)
            {
                throw new ArgumentException("Input Value for ConvertFTPDateToDateTime method was too short.");
            }
            int year = Convert.ToInt16(input.Substring(0, 4));
            int month = Convert.ToInt16(input.Substring(4, 2));
            int day = Convert.ToInt16(input.Substring(6, 2));
            int hour = Convert.ToInt16(input.Substring(8, 2));
            int minute = Convert.ToInt16(input.Substring(10, 2));
            return new DateTime(year, month, day, hour, minute, Convert.ToInt16(input.Substring(12, 2)));
        }

        public void Disconnect()
        {
            this.CloseDataSocket();
            if (this.main_sock != null)
            {
                if (this.main_sock.Connected)
                {
                    this.SendCommand("QUIT");
                    this.main_sock.Close();
                }
                this.main_sock = null;
            }
            if (this.file != null)
            {
                this.file.Close();
            }
            this.main_ipEndPoint = null;
            this.file = null;
        }

        public long DoDownload()
        {
            long num;
            byte[] buffer = new byte[0x200];
            try
            {
                num = this.data_sock.Receive(buffer, buffer.Length, SocketFlags.None);
                if (num <= 0L)
                {
                    this.CloseDataSocket();
                    this.file.Close();
                    this.file = null;
                    this.ReadResponse();
                    switch (this.response)
                    {
                        case 0xe2:
                        case 250:
                            this.SetBinaryMode(false);
                            return num;
                    }
                    this.errormessage = this.errormessage + this.responseStr;
                    return -1L;
                }
                this.file.Write(buffer, 0, (int) num);
                this.bytes_total += num;
            }
            catch (Exception exception)
            {
                this.CloseDataSocket();
                this.file.Close();
                this.file = null;
                this.ReadResponse();
                this.SetBinaryMode(false);
                this.errormessage = this.errormessage + exception.Message;
                return -1L;
            }
            return num;
        }

        public long DoUpload()
        {
            long num;
            byte[] buffer = new byte[0x200];
            try
            {
                num = this.file.Read(buffer, 0, buffer.Length);
                this.bytes_total += num;
                this.data_sock.Send(buffer, (int) num, SocketFlags.None);
                if (num <= 0L)
                {
                    this.file.Close();
                    this.file = null;
                    this.CloseDataSocket();
                    this.ReadResponse();
                    switch (this.response)
                    {
                        case 0xe2:
                        case 250:
                            this.SetBinaryMode(false);
                            return num;
                    }
                    this.errormessage = this.errormessage + this.responseStr;
                    return -1L;
                }
            }
            catch (Exception exception)
            {
                this.file.Close();
                this.file = null;
                this.CloseDataSocket();
                this.ReadResponse();
                this.SetBinaryMode(false);
                this.errormessage = this.errormessage + exception.Message;
                return -1L;
            }
            return num;
        }

        private void Fail()
        {
            this.Disconnect();
            this.errormessage = this.errormessage + this.responseStr;
        }

        private void FillBucket()
        {
            byte[] buffer = new byte[0x200];
            int num2 = 0;
            while (this.main_sock.Available < 1)
            {
                Thread.Sleep(50);
                num2 += 50;
                if (num2 > this.timeout)
                {
                    this.Disconnect();
                    this.errormessage = this.errormessage + "Timed out waiting on server to respond.";
                    return;
                }
            }
            while (this.main_sock.Available > 0)
            {
                long num = this.main_sock.Receive(buffer, 0x200, SocketFlags.None);
                this.bucket = this.bucket + Encoding.ASCII.GetString(buffer, 0, (int) num);
                Thread.Sleep(50);
            }
        }

        public DateTime GetFileDate(string fileName)
        {
            return this.ConvertFTPDateToDateTime(this.GetFileDateRaw(fileName));
        }

        public string GetFileDateRaw(string fileName)
        {
            this.Connect();
            this.SendCommand("MDTM " + fileName);
            this.ReadResponse();
            if (this.response != 0xd5)
            {
                this.errormessage = this.errormessage + this.responseStr;
                return "";
            }
            return this.responseStr.Substring(4);
        }

        public long GetFileSize(string filename)
        {
            this.Connect();
            this.SendCommand("SIZE " + filename);
            this.ReadResponse();
            if (this.response != 0xd5)
            {
                this.errormessage = this.errormessage + this.responseStr;
            }
            return long.Parse(this.responseStr.Substring(4));
        }

        private string GetLineFromBucket()
        {
            string str = "";
            int index = this.bucket.IndexOf('\n');
            if (index < 0)
            {
                while (index < 0)
                {
                    this.FillBucket();
                    index = this.bucket.IndexOf('\n');
                }
            }
            str = this.bucket.Substring(0, index);
            this.bucket = this.bucket.Substring(index + 1);
            return str;
        }

        public string GetWorkingDirectory()
        {
            string str;
            this.Connect();
            this.SendCommand("PWD");
            this.ReadResponse();
            if (this.response != 0x101)
            {
                this.errormessage = this.errormessage + this.responseStr;
            }
            try
            {
                str = this.responseStr.Substring(this.responseStr.IndexOf("\"", 0) + 1);
                str = str.Substring(0, str.LastIndexOf("\"")).Replace("\"\"", "\"");
            }
            catch (Exception exception)
            {
                this.errormessage = this.errormessage + exception.Message;
                return null;
            }
            return str;
        }

        public ArrayList List()
        {
            byte[] buffer = new byte[0x200];
            string str = "";
            long num = 0L;
            int num2 = 0;
            ArrayList list = new ArrayList();
            this.Connect();
            this.OpenDataSocket();
            this.SendCommand("LIST");
            this.ReadResponse();
            switch (this.response)
            {
                case 0x7d:
                case 150:
                    this.ConnectDataSocket();
                    while (this.data_sock.Available < 1)
                    {
                        Thread.Sleep(50);
                        num2 += 50;
                        if (num2 > (this.timeout / 10))
                        {
                            break;
                        }
                    }
                    break;

                default:
                    this.CloseDataSocket();
                    throw new Exception(this.responseStr);
            }
            while (this.data_sock.Available > 0)
            {
                num = this.data_sock.Receive(buffer, buffer.Length, SocketFlags.None);
                str = str + Encoding.ASCII.GetString(buffer, 0, (int) num);
                Thread.Sleep(50);
            }
            this.CloseDataSocket();
            this.ReadResponse();
            if (this.response != 0xe2)
            {
                throw new Exception(this.responseStr);
            }
            foreach (string str2 in str.Split(new char[] { '\n' }))
            {
                if (!((str2.Length <= 0) || Regex.Match(str2, "^total").Success))
                {
                    list.Add(str2.Substring(0, str2.Length - 1));
                }
            }
            return list;
        }

        public ArrayList ListDirectories()
        {
            ArrayList list = new ArrayList();
            foreach (string str in this.List())
            {
                if ((str.Length > 0) && ((str[0] == 'd') || (str.ToUpper().IndexOf("<DIR>") >= 0)))
                {
                    list.Add(str);
                }
            }
            return list;
        }

        public ArrayList ListFiles()
        {
            ArrayList list = new ArrayList();
            foreach (string str in this.List())
            {
                if ((str.Length > 0) && ((str[0] != 'd') && (str.ToUpper().IndexOf("<DIR>") < 0)))
                {
                    list.Add(str);
                }
            }
            return list;
        }

        public void MakeDir(string dir)
        {
            this.Connect();
            this.SendCommand("MKD " + dir);
            this.ReadResponse();
            switch (this.response)
            {
                case 250:
                case 0x101:
                    break;

                default:
                    this.errormessage = this.errormessage + this.responseStr;
                    break;
            }
        }

        private void OpenDataSocket()
        {
            Exception exception;
            if (this.passive_mode)
            {
                string[] strArray;
                this.Connect();
                this.SendCommand("PASV");
                this.ReadResponse();
                if (this.response != 0xe3)
                {
                    this.Fail();
                }
                try
                {
                    int startIndex = this.responseStr.IndexOf('(') + 1;
                    int length = this.responseStr.IndexOf(')') - startIndex;
                    strArray = this.responseStr.Substring(startIndex, length).Split(new char[] { ',' });
                }
                catch (Exception)
                {
                    this.Disconnect();
                    this.errormessage = this.errormessage + "Malformed PASV response: " + this.responseStr;
                    return;
                }
                if (strArray.Length < 6)
                {
                    this.Disconnect();
                    this.errormessage = this.errormessage + "Malformed PASV response: " + this.responseStr;
                }
                else
                {
                    string hostNameOrAddress = string.Format("{0}.{1}.{2}.{3}", new object[] { strArray[0], strArray[1], strArray[2], strArray[3] });
                    int port = (int.Parse(strArray[4]) << 8) + int.Parse(strArray[5]);
                    try
                    {
                        this.CloseDataSocket();
                        this.data_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        this.data_ipEndPoint = new IPEndPoint(Dns.GetHostEntry(hostNameOrAddress).AddressList[0], port);
                        this.data_sock.Connect(this.data_ipEndPoint);
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        this.errormessage = this.errormessage + "Failed to connect for data transfer: " + exception.Message;
                    }
                }
            }
            else
            {
                this.Connect();
                try
                {
                    this.CloseDataSocket();
                    this.listening_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    string str2 = this.main_sock.LocalEndPoint.ToString();
                    int index = str2.IndexOf(':');
                    if (index < 0)
                    {
                        this.errormessage = this.errormessage + "Failed to parse the local address: " + str2;
                    }
                    else
                    {
                        string ipString = str2.Substring(0, index);
                        IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(ipString), 0);
                        this.listening_sock.Bind(localEP);
                        str2 = this.listening_sock.LocalEndPoint.ToString();
                        index = str2.IndexOf(':');
                        if (index < 0)
                        {
                            this.errormessage = this.errormessage + "Failed to parse the local address: " + str2;
                        }
                        int num5 = int.Parse(str2.Substring(index + 1));
                        this.listening_sock.Listen(1);
                        string command = string.Format("PORT {0},{1},{2}", ipString.Replace('.', ','), num5 / 0x100, num5 % 0x100);
                        this.SendCommand(command);
                        this.ReadResponse();
                        if (this.response != 200)
                        {
                            this.Fail();
                        }
                    }
                }
                catch (Exception exception3)
                {
                    exception = exception3;
                    this.errormessage = this.errormessage + "Failed to connect for data transfer: " + exception.Message;
                }
            }
        }

        public void OpenDownload(string filename)
        {
            this.OpenDownload(filename, filename, false);
        }

        public void OpenDownload(string filename, bool resume)
        {
            this.OpenDownload(filename, filename, resume);
        }

        public void OpenDownload(string remote_filename, string localfilename)
        {
            this.OpenDownload(remote_filename, localfilename, false);
        }

        public void OpenDownload(string remote_filename, string local_filename, bool resume)
        {
            Exception exception;
            this.Connect();
            this.SetBinaryMode(true);
            this.bytes_total = 0L;
            try
            {
                this.file_size = this.GetFileSize(remote_filename);
            }
            catch
            {
                this.file_size = 0L;
            }
            if (resume && System.IO.File.Exists(local_filename))
            {
                try
                {
                    this.file = new FileStream(local_filename, FileMode.Open);
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    this.file = null;
                    throw new Exception(exception.Message);
                }
                this.SendCommand("REST " + this.file.Length);
                this.ReadResponse();
                if (this.response != 350)
                {
                    throw new Exception(this.responseStr);
                }
                this.file.Seek(this.file.Length, SeekOrigin.Begin);
                this.bytes_total = this.file.Length;
            }
            else
            {
                try
                {
                    this.file = new FileStream(local_filename, FileMode.Create);
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    this.file = null;
                    throw new Exception(exception.Message);
                }
            }
            this.OpenDataSocket();
            this.SendCommand("RETR " + remote_filename);
            this.ReadResponse();
            switch (this.response)
            {
                case 0x7d:
                case 150:
                    this.ConnectDataSocket();
                    break;

                default:
                    this.file.Close();
                    this.file = null;
                    this.errormessage = this.errormessage + this.responseStr;
                    break;
            }
        }

        public bool OpenUpload(string filename)
        {
            return this.OpenUpload(filename, filename, false);
        }

        public bool OpenUpload(string filename, bool resume)
        {
            return this.OpenUpload(filename, filename, resume);
        }

        public bool OpenUpload(string filename, string remotefilename)
        {
            return this.OpenUpload(filename, remotefilename, false);
        }

        public bool OpenUpload(string filename, string remote_filename, bool resume)
        {
            this.Connect();
            this.SetBinaryMode(true);
            this.OpenDataSocket();
            this.bytes_total = 0L;
            try
            {
                this.file = new FileStream(filename, FileMode.Open);
            }
            catch (Exception exception)
            {
                this.file = null;
                this.errormessage = this.errormessage + exception.Message;
                return false;
            }
            this.file_size = this.file.Length;
            if (resume)
            {
                long fileSize = this.GetFileSize(remote_filename);
                this.SendCommand("REST " + fileSize);
                this.ReadResponse();
                if (this.response == 350)
                {
                    this.file.Seek(fileSize, SeekOrigin.Begin);
                }
            }
            this.SendCommand("STOR " + remote_filename);
            this.ReadResponse();
            switch (this.response)
            {
                case 0x7d:
                case 150:
                    this.ConnectDataSocket();
                    return true;
            }
            this.file.Close();
            this.file = null;
            this.errormessage = this.errormessage + this.responseStr;
            return false;
        }

        private void ReadResponse()
        {
            this.messages = "";
            while (true)
            {
                string lineFromBucket = this.GetLineFromBucket();
                if (Regex.Match(lineFromBucket, "^[0-9]+ ").Success)
                {
                    this.responseStr = lineFromBucket;
                    this.response = int.Parse(lineFromBucket.Substring(0, 3));
                    return;
                }
                this.messages = this.messages + Regex.Replace(lineFromBucket, "^[0-9]+-", "") + "\n";
            }
        }

        public void RemoveDir(string dir)
        {
            this.Connect();
            this.SendCommand("RMD " + dir);
            this.ReadResponse();
            if (this.response != 250)
            {
                this.errormessage = this.errormessage + this.responseStr;
            }
        }

        public void RemoveFile(string filename)
        {
            this.Connect();
            this.SendCommand("DELE " + filename);
            this.ReadResponse();
            if (this.response != 250)
            {
                this.errormessage = this.errormessage + this.responseStr;
            }
        }

        public void RenameFile(string oldfilename, string newfilename)
        {
            this.Connect();
            this.SendCommand("RNFR " + oldfilename);
            this.ReadResponse();
            if (this.response != 350)
            {
                this.errormessage = this.errormessage + this.responseStr;
            }
            else
            {
                this.SendCommand("RNTO " + newfilename);
                this.ReadResponse();
                if (this.response != 250)
                {
                    this.errormessage = this.errormessage + this.responseStr;
                }
            }
        }

        private void SendCommand(string command)
        {
            byte[] bytes = Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());
            if ((command.Length > 3) && (command.Substring(0, 4) == "PASS"))
            {
                this.messages = "\rPASS xxx";
            }
            else
            {
                this.messages = "\r" + command;
            }
            try
            {
                this.main_sock.Send(bytes, bytes.Length, SocketFlags.None);
            }
            catch (Exception exception)
            {
                try
                {
                    this.Disconnect();
                    this.errormessage = this.errormessage + exception.Message;
                }
                catch
                {
                    this.main_sock.Close();
                    this.file.Close();
                    this.main_sock = null;
                    this.main_ipEndPoint = null;
                    this.file = null;
                }
            }
        }

        private void SetBinaryMode(bool mode)
        {
            if (mode)
            {
                this.SendCommand("TYPE I");
            }
            else
            {
                this.SendCommand("TYPE A");
            }
            this.ReadResponse();
            if (this.response != 200)
            {
                this.Fail();
            }
        }

        public long BytesTotal
        {
            get
            {
                return this.bytes_total;
            }
        }

        public long FileSize
        {
            get
            {
                return this.file_size;
            }
        }

        public bool IsConnected
        {
            get
            {
                return ((this.main_sock != null) && this.main_sock.Connected);
            }
        }

        public string Messages
        {
            get
            {
                string messages = this.messages;
                this.messages = "";
                return messages;
            }
        }

        public bool MessagesAvailable
        {
            get
            {
                return (this.messages.Length > 0);
            }
        }

        public bool PassiveMode
        {
            get
            {
                return this.passive_mode;
            }
            set
            {
                this.passive_mode = value;
            }
        }

        public string ResponseString
        {
            get
            {
                return this.responseStr;
            }
        }
    }
}

