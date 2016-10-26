namespace Game.Utils
{
    using System;
    using System.Web;

    public class GameRequest
    {
        private static readonly string[] _WebSearchList = new string[] { 
            "google", "isaac", "surveybot", "baiduspider", "yahoo", "yisou", "3721", "qihoo", "daqi", "ia_archiver", "p.arthur", "fast-webcrawler", "java", "microsoft-atl-native", "turnitinbot", "webgather", 
            "sleipnir", "msn", "sogou", "lycos", "tom", "iask", "soso", "sina", "baidu", "gougou", "zhongsou"
         };

        private GameRequest()
        {
        }

        public static string GetCurrentFullHost()
        {
            HttpRequest request = HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
            {
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            }
            return request.Url.Host;
        }

        public static float GetFloat(string strName, float defValue)
        {
            if (GetQueryFloat(strName, defValue) == defValue)
            {
                return GetFormFloat(strName, defValue);
            }
            return GetQueryFloat(strName, defValue);
        }

        public static float GetFormFloat(string strName, float defValue)
        {
            return TypeParse.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
        }

        public static int GetFormInt(string strName, int defValue)
        {
            return GetFormInt(Request, strName, defValue);
        }

        public static int GetFormInt(HttpRequest request, string strName, int defValue)
        {
            return TypeParse.StrToInt(request.Form[strName], defValue);
        }

        public static string GetFormString(string strName)
        {
            return GetFormString(Request, strName);
        }

        public static string GetFormString(HttpRequest request, string strName)
        {
            if ((request == null) || (request.Form[strName] == null))
            {
                return string.Empty;
            }
            return request.Form[strName];
        }

        public static string GetHost()
        {
            if (HttpContext.Current == null)
            {
                return string.Empty;
            }
            return HttpContext.Current.Request.Url.Host;
        }

        public static int GetInt(string strName, int defValue)
        {
            if (GetQueryInt(strName, defValue) == defValue)
            {
                return GetFormInt(strName, defValue);
            }
            return GetQueryInt(strName, defValue);
        }

        public static string GetPageName()
        {
            string[] strArray = HttpContext.Current.Request.Url.AbsolutePath.Split(new char[] { '/' });
            return strArray[strArray.Length - 1].ToLower();
        }

        public static int GetParamCount()
        {
            return (HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count);
        }

        public static float GetQueryFloat(string strName, float defValue)
        {
            return TypeParse.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
        }

        public static int GetQueryInt(string strName, int defValue)
        {
            return GetQueryInt(Request, strName, defValue);
        }

        public static int GetQueryInt(HttpRequest request, string strName, int defValue)
        {
            return TypeParse.StrToInt(request.QueryString[strName], defValue);
        }

        public static string GetQueryString(string strName)
        {
            return GetQueryString(Request, strName);
        }

        public static string GetQueryString(HttpRequest request, string strName)
        {
            if ((request == null) || (request.QueryString[strName] == null))
            {
                return string.Empty;
            }
            return request.QueryString[strName];
        }

        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        public static string GetServerDomain()
        {
            string ipval = HttpContext.Current.Request.Url.Host.ToLower();
            if ((ipval.Split(new char[] { '.' }).Length < 3) || Validate.IsIP(ipval))
            {
                return ipval;
            }
            string str2 = ipval.Remove(0, ipval.IndexOf(".") + 1);
            if (((str2.StartsWith("com.") || str2.StartsWith("net.")) || str2.StartsWith("org.")) || str2.StartsWith("gov."))
            {
                return ipval;
            }
            return str2;
        }

        public static string GetServerString(string strName)
        {
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.ServerVariables[strName].ToString();
        }

        public static string GetString(string strName)
        {
            if ("".Equals(GetQueryString(strName)))
            {
                return GetFormString(strName);
            }
            return GetQueryString(strName);
        }

        public static string GetString(HttpRequest request, string strName)
        {
            if ("".Equals(GetQueryString(request, strName)))
            {
                return GetFormString(request, strName);
            }
            return GetQueryString(request, strName);
        }

        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        public static string GetUrlReferrer()
        {
            Uri urlReferrer = HttpContext.Current.Request.UrlReferrer;
            if (urlReferrer == null)
            {
                return string.Empty;
            }
            return Convert.ToString(urlReferrer);
        }

        public static string GetUserBrowser()
        {
            string str = "Unknown";
            if (Request == null)
            {
                return str;
            }
            string userAgent = Request.UserAgent;
            switch (userAgent)
            {
                case null:
                case "":
                    return str;
            }
            userAgent = userAgent.ToLower();
            HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            if (((((userAgent.IndexOf("firefox") >= 0) || (userAgent.IndexOf("firebird") >= 0)) || ((userAgent.IndexOf("myie") >= 0) || (userAgent.IndexOf("opera") >= 0))) || (userAgent.IndexOf("netscape") >= 0)) || (userAgent.IndexOf("msie") >= 0))
            {
                return (browser.Browser + browser.Version);
            }
            return "Unknown";
        }

        public static string GetUserIP()
        {
            if (HttpContext.Current == null)
            {
                return string.Empty;
            }
            string ipval = string.Empty;
            ipval = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            switch (ipval)
            {
                case null:
                case "":
                    ipval = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    break;
            }
            if ((ipval == null) || (ipval == string.Empty))
            {
                ipval = HttpContext.Current.Request.UserHostAddress;
            }
            if (!(((ipval != null) && (ipval != string.Empty)) && Validate.IsIP(ipval)))
            {
                return "0.0.0.0";
            }
            return ipval;
        }

        public static string GetUserOsname()
        {
            string str = "Unknown";
            if (Request != null)
            {
                string userAgent = Request.UserAgent;
                switch (userAgent)
                {
                    case null:
                    case "":
                        return str;
                }
                if (userAgent.Contains("NT 6.1"))
                {
                    str = "Windows 7";
                }
                else
                {
                    if (userAgent.Contains("NT 6.0"))
                    {
                        return "Windows Vista/Server 2008";
                    }
                    if (userAgent.Contains("NT 5.2"))
                    {
                        return "Windows Server 2003";
                    }
                    if (userAgent.Contains("NT 5.1"))
                    {
                        return "Windows XP";
                    }
                    if (userAgent.Contains("NT 5"))
                    {
                        return "Windows 2000";
                    }
                    if (userAgent.Contains("NT 4"))
                    {
                        return "Windows NT4";
                    }
                    if (userAgent.Contains("Me"))
                    {
                        return "Windows Me";
                    }
                    if (userAgent.Contains("98"))
                    {
                        return "Windows 98";
                    }
                    if (userAgent.Contains("95"))
                    {
                        return "Windows 95";
                    }
                    if (userAgent.Contains("Mac"))
                    {
                        return "Mac";
                    }
                    if (userAgent.Contains("Unix"))
                    {
                        return "UNIX";
                    }
                    if (userAgent.Contains("Linux"))
                    {
                        str = "Linux";
                    }
                    else if (userAgent.Contains("SunOS"))
                    {
                        str = "SunOS";
                    }
                }
            }
            return str;
        }

        public static bool IsBrowserGet()
        {
            string[] strArray = new string[] { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string str = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < strArray.Length; i++)
            {
                if (str.IndexOf(strArray[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsCrossSitePost()
        {
            return (!HttpContext.Current.Request.HttpMethod.Equals("POST") || IsCrossSitePost(GetUrlReferrer(), HttpContext.Current.Request.Url.Host));
        }

        public static bool IsCrossSitePost(string urlReferrer, string host)
        {
            if (urlReferrer.Length < 7)
            {
                return true;
            }
            string str = urlReferrer.Remove(0, 7);
            if (str.IndexOf(":") > -1)
            {
                str = str.Substring(0, str.IndexOf(":"));
            }
            else
            {
                str = str.Substring(0, str.IndexOf('/'));
            }
            return (str != host);
        }

        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }

        public static bool IsRobots()
        {
            return IsSearchEnginesGet();
        }

        public static bool IsSearchEnginesGet()
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            if ((userAgent == null) || (string.Empty == userAgent))
            {
                return true;
            }
            userAgent = userAgent.ToLower();
            for (int i = 0; i < _WebSearchList.Length; i++)
            {
                if (-1 != userAgent.IndexOf(_WebSearchList[i]))
                {
                    return true;
                }
            }
            return GetUserBrowser().Equals("Unknown");
        }

        public static void SaveRequestFile(string path)
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpContext.Current.Request.Files[0].SaveAs(path);
            }
        }

        public static bool IsPostFromAnotherDomain
        {
            get
            {
                if (HttpContext.Current.Request.HttpMethod == "GET")
                {
                    return false;
                }
                return (GetUrlReferrer().IndexOf(GetServerDomain()) == -1);
            }
        }

        public static HttpRequest Request
        {
            get
            {
                HttpContext current = HttpContext.Current;
                if ((current == null) || (current.Request == null))
                {
                    return null;
                }
                return current.Request;
            }
        }
    }
}

