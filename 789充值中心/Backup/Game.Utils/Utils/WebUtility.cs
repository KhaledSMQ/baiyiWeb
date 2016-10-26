namespace Game.Utils
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Web;

    public class WebUtility
    {
        public static void Download(string path, string saveName)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.ContentType = "application/octet-stream";
            response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(saveName, Encoding.UTF8));
            response.TransmitFile(path);
            response.End();
        }

        public static void DownloadFile(string path, string saveName)
        {
            Stream stream = null;
            byte[] buffer = new byte[0x2800];
            string str = path;
            string fileName = Path.GetFileName(str);
            try
            {
                stream = new FileStream(str, FileMode.Open, FileAccess.Read, FileShare.Read);
                HttpContext.Current.Response.Clear();
                long length = stream.Length;
                long num3 = 0L;
                if (HttpContext.Current.Request.Headers["Range"] != null)
                {
                    HttpContext.Current.Response.StatusCode = 0xce;
                    num3 = long.Parse(HttpContext.Current.Request.Headers["Range"].Replace("bytes=", "").Replace("-", ""));
                }
                if (num3 != 0L)
                {
                    string[] strArray = new string[] { "bytes ", num3.ToString(), "-", (length - 1L).ToString(), "/", length.ToString() };
                    HttpContext.Current.Response.AddHeader("Content-Range", string.Concat(strArray));
                }
                long num4 = length - num3;
                HttpContext.Current.Response.AddHeader("Content-Length", num4.ToString());
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(saveName, Encoding.UTF8));
                stream.Position = num3;
                length -= num3;
                while (length > 0L)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int count = stream.Read(buffer, 0, 0x2800);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, count);
                        HttpContext.Current.Response.Flush();
                        buffer = new byte[0x2800];
                        length -= count;
                    }
                    else
                    {
                        length = -1L;
                    }
                }
            }
            catch (Exception exception)
            {
                HttpContext.Current.Response.Write("Error : " + exception.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                HttpContext.Current.Response.End();
            }
        }

        public static bool DownloadFile(string _fullPath, string _fileName, long _speed)
        {
            HttpRequest request = HttpContext.Current.Request;
            HttpResponse response = HttpContext.Current.Response;
            try
            {
                FileStream input = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader reader = new BinaryReader(input);
                try
                {
                    response.AddHeader("Accept-Ranges", "bytes");
                    response.Buffer = false;
                    long length = input.Length;
                    long num2 = 0L;
                    int count = 0x2800;
                    double d = ((long) (0x3e8 * count)) / _speed;
                    int millisecondsTimeout = ((int) Math.Floor(d)) + 1;
                    if (request.Headers["Range"] != null)
                    {
                        response.StatusCode = 0xce;
                        num2 = Convert.ToInt64(request.Headers["Range"].Split(new char[] { '=', '-' })[1]);
                    }
                    response.AddHeader("Content-Length", (length - num2).ToString());
                    if (num2 != 0L)
                    {
                        response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", num2, length - 1L, length));
                    }
                    response.AddHeader("Connection", "Keep-Alive");
                    response.ContentType = "application/octet-stream";
                    response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, Encoding.UTF8));
                    reader.BaseStream.Seek(num2, SeekOrigin.Begin);
                    double num6 = (length - num2) / ((long) count);
                    int num7 = ((int) Math.Floor(num6)) + 1;
                    for (int i = 0; i < num7; i++)
                    {
                        if (response.IsClientConnected)
                        {
                            response.BinaryWrite(reader.ReadBytes(count));
                            Thread.Sleep(millisecondsTimeout);
                        }
                        else
                        {
                            i = num7;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    reader.Close();
                    input.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string LoadPageContent(string url)
        {
            byte[] bytes = new WebClient { Credentials = CredentialCache.DefaultCredentials }.DownloadData(url);
            return Encoding.GetEncoding("gb2312").GetString(bytes);
        }

        public static string LoadURLString(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            Stream responseStream = ((HttpWebResponse) request.GetResponse()).GetResponseStream();
            string str = "";
            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("gb2312"));
            char[] buffer = new char[0x100];
            int length = reader.Read(buffer, 0, 0x100);
            int num2 = 0;
            while (length > 0)
            {
                num2 += Encoding.UTF8.GetByteCount(buffer, 0, 0x100);
                string str2 = new string(buffer, 0, length);
                str = str + str2;
                length = reader.Read(buffer, 0, 0x100);
            }
            return str;
        }

        public static void RemoteGetFile(string url, string saveurl)
        {
            int num3;
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            int contentLength = (int) response.ContentLength;
            int bufferSize = 0x19000;
            byte[] buffer = new byte[bufferSize];
            FileManager.WriteFile(saveurl, "temp", Encoding.UTF8);
            FileStream stream2 = System.IO.File.Create(saveurl, bufferSize);
            do
            {
                num3 = responseStream.Read(buffer, 0, buffer.Length);
                stream2.Write(buffer, 0, num3);
            }
            while (num3 > 0);
            stream2.Flush();
            stream2.Close();
            responseStream.Flush();
            responseStream.Close();
        }

        public static string[] UploadFile(HttpPostedFile upfile, string limitType, int limitSize, bool autoName, string autoFilename, string savepath)
        {
            string str6;
            bool flag;
            string[] strArray = new string[5];
            string[] strArray2 = null;
            if (!string.IsNullOrEmpty(limitType))
            {
                strArray2 = TextUtility.SplitStrArray(limitType, ",");
            }
            int contentLength = upfile.ContentLength;
            string fileName = upfile.FileName;
            string originalStr = Path.GetFileName(fileName);
            switch (originalStr)
            {
                case null:
                case "":
                    strArray[0] = "无文件";
                    strArray[1] = "";
                    strArray[2] = "";
                    strArray[3] = "";
                    strArray[4] = "<span style='color:red;'>失败</span>";
                    return strArray;

                default:
                {
                    string contentType = upfile.ContentType;
                    string[] strArray3 = TextUtility.SplitStrArray(originalStr, ".");
                    string strA = strArray3[strArray3.Length - 1];
                    int length = (originalStr.Length - strA.Length) - 1;
                    string dateTimeLongString = "";
                    if (autoName)
                    {
                        dateTimeLongString = TextUtility.GetDateTimeLongString();
                    }
                    else if (TextUtility.EmptyTrimOrNull(autoFilename))
                    {
                        dateTimeLongString = originalStr.Substring(0, length);
                    }
                    else
                    {
                        dateTimeLongString = autoFilename;
                    }
                    str6 = dateTimeLongString + "." + strA;
                    strArray[0] = fileName;
                    strArray[1] = str6;
                    strArray[2] = contentType;
                    strArray[3] = contentLength.ToString();
                    flag = false;
                    if ((limitSize <= contentLength) && (limitSize != 0))
                    {
                        strArray[4] = "<span style='color:red;'>失败</span>，上传文件过大";
                        flag = false;
                    }
                    else if (strArray2 != null)
                    {
                        for (int i = 0; i <= (strArray2.Length - 1); i++)
                        {
                            if (string.Compare(strA, strArray2[i].ToString(), true) == 0)
                            {
                                flag = true;
                                break;
                            }
                            strArray[4] = "<span style='color:red;'>失败</span>，文件类型不允许上传";
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                    break;
                }
            }
            if (flag)
            {
                try
                {
                    string file = savepath + str6;
                    FileManager.WriteFile(file, "临时文件");
                    upfile.SaveAs(file);
                    strArray[4] = "成功";
                }
                catch (Exception exception)
                {
                    strArray[4] = "<span style='color:red;'>失败</span><!-- " + exception.Message + " -->";
                }
            }
            return strArray;
        }

        public static bool ValidateUri(string url)
        {
            Uri requestUri = new Uri(url);
            try
            {
                HttpWebResponse response = (HttpWebResponse) WebRequest.Create(requestUri).GetResponse();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }

        public static void WebClientGetFile(string[] urls, string[] saveurls)
        {
            WebClient client = new WebClient();
            for (int i = 0; i < urls.Length; i++)
            {
                try
                {
                    client.DownloadFile(urls[i], saveurls[i]);
                }
                catch
                {
                }
            }
            client.Dispose();
        }

        public static void WebClientGetFile(string url, string saveurl)
        {
            WebClient client = new WebClient();
            try
            {
                client.DownloadFile(url, saveurl);
            }
            catch
            {
            }
            client.Dispose();
        }
    }
}

