namespace Game.Utils
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    public sealed class JavaScript
    {
        private static int keyForScriptCount = 0;
        public const string tagBegin = "<script type=\"text/javascript\">\r\n<!--\r\n";
        public const string tagEnd = "\r\n//-->\r\n</script>";

        private JavaScript()
        {
        }

        public static bool Alert(string displayInfo)
        {
            return InsertScriptToPage(GetAlertScript(displayInfo));
        }

        public static bool Alert(string key, string displayInfo)
        {
            return InsertScriptToPage(key, GetAlertScript(displayInfo));
        }

        public static bool AlertAndCloseWindow(string displayInfo)
        {
            return InsertScriptToPage(GetAlertAndCloseWindowScript(displayInfo));
        }

        public static bool AlertAndCloseWindow(string key, string displayInfo)
        {
            return InsertScriptToPage(key, GetAlertAndCloseWindowScript(displayInfo));
        }

        public static bool AlertAndDo(string whatToDo, string displayInfo)
        {
            return InsertScriptToPage(GetAlertAndDoScript(whatToDo, displayInfo));
        }

        public static bool AlertAndDo(string whatToDo, string key, string displayInfo)
        {
            return InsertScriptToPage(key, GetAlertAndDoScript(whatToDo, displayInfo));
        }

        public static bool AlertAndRedirect(string pageUrl, string displayInfo)
        {
            return InsertScriptToPage(GetAlertAndRedirectScript(pageUrl, displayInfo));
        }

        public static bool AlertAndRedirect(string pageUrl, string key, string displayInfo)
        {
            return InsertScriptToPage(key, GetAlertAndRedirectScript(pageUrl, displayInfo));
        }

        public static bool CloseWindow()
        {
            return InsertScriptToPage(CloseWindowScript);
        }

        public static bool CloseWindowNoConfirm()
        {
            return InsertScriptToPage(CloseWindowNoConfirmScript);
        }

        public static bool CloseWindowWithRefreshOpener()
        {
            return InsertScriptToPage(CloseWindowWithRefreshOpenerScript);
        }

        public static bool ConfirmAndDo(string whatToDo, string displayInfo)
        {
            return InsertScriptToPage(GetConfirmAndDoScript(whatToDo, displayInfo));
        }

        public static bool ConfirmAndDo(string whatToDo, string key, string displayInfo)
        {
            return InsertScriptToPage(key, GetConfirmAndDoScript(whatToDo, displayInfo));
        }

        public static bool ConfirmAndRedirect(string pageUrl, string displayInfo)
        {
            return InsertScriptToPage(GetConfirmAndRedirectScript(pageUrl, displayInfo));
        }

        public static bool ConfirmAndRedirect(string pageUrl, string key, string displayInfo)
        {
            return InsertScriptToPage(key, GetConfirmAndRedirectScript(pageUrl, displayInfo));
        }

        public static string GetAddFavoriteScript(string url, string title)
        {
            return ("window.external.AddFavorite('" + url + "','" + title + "');return true;");
        }

        public static string GetAlertAndCloseWindowScript(string displayInfo)
        {
            displayInfo = displayInfo.Replace(@"\", @"\\");
            displayInfo = displayInfo.Replace("\n", @"\n");
            displayInfo = displayInfo.Replace("'", @"\'");
            return (("alert('" + displayInfo + "');") + CloseWindowScript);
        }

        public static string GetAlertAndDoScript(string whatToDo, string displayInfo)
        {
            displayInfo = displayInfo.Replace(@"\", @"\\");
            displayInfo = displayInfo.Replace("\n", @"\n");
            displayInfo = displayInfo.Replace("'", @"\'");
            return (("alert('" + displayInfo + "');\n") + whatToDo);
        }

        public static string GetAlertAndRedirectScript(string pageUrl, string displayInfo)
        {
            displayInfo = displayInfo.Replace(@"\", @"\\");
            displayInfo = displayInfo.Replace("\n", @"\n");
            displayInfo = displayInfo.Replace("'", @"\'");
            pageUrl = pageUrl.Replace("'", @"\'");
            string str = "window.location.replace('" + pageUrl + "');";
            return (("alert('" + displayInfo + "');") + str);
        }

        public static string GetAlertScript(string displayInfo)
        {
            displayInfo = displayInfo.Replace(@"\", @"\\");
            displayInfo = displayInfo.Replace("\n", @"\n");
            displayInfo = displayInfo.Replace("'", @"\'");
            return ("alert('" + displayInfo + "');");
        }

        public static string GetConfirmAndDoScript(string whatToDo, string displayInfo)
        {
            displayInfo = displayInfo.Replace(@"\", @"\\");
            displayInfo = displayInfo.Replace("\n", @"\n");
            displayInfo = displayInfo.Replace("'", @"\'");
            return ("var retValue=window.confirm('" + displayInfo + "');if(retValue){\n" + whatToDo + "\n}");
        }

        public static string GetConfirmAndRedirectScript(string pageUrl, string displayInfo)
        {
            displayInfo = displayInfo.Replace(@"\", @"\\");
            displayInfo = displayInfo.Replace("\n", @"\n");
            displayInfo = displayInfo.Replace("'", @"\'");
            pageUrl = pageUrl.Replace("'", @"\'");
            return ("var retValue=window.confirm('" + displayInfo + "');if(retValue){window.location.replace('" + pageUrl + "');}");
        }

        public static string GetOpenCentralWindowScript(string url, string windowName, int height, int width)
        {
            windowName = windowName.Replace(@"\", @"\\");
            windowName = windowName.Replace("\n", @"\n");
            windowName = windowName.Replace("'", @"\'");
            url = url.Replace("'", @"\'");
            StringBuilder builder = new StringBuilder();
            builder.Append("var lef = (window.screen.width-");
            builder.Append(width);
            builder.Append(")/2;\n");
            builder.Append("var to = (window.screen.height-");
            builder.Append(height);
            builder.Append(")/2;\n");
            builder.Append("var PeterLee=window.open('");
            builder.Append(url);
            builder.Append("','");
            builder.Append(windowName);
            builder.Append("','");
            builder.Append("width=");
            builder.Append(width);
            builder.Append(",height=");
            builder.Append(height);
            builder.Append(",left=' + lef + ',top=' + to);\nPeterLee.focus();");
            return builder.ToString();
        }

        public static string GetOpenCentralWindowScript(string url, string windowName, int height, int width, bool isWithToolbar, bool isWithMenubar, bool resizable, bool isWithLocation, bool isWithStatusbar, bool isWithScrollbars, bool isFront, int autoCloseInSeconds)
        {
            windowName = windowName.Replace(@"\", @"\\");
            windowName = windowName.Replace("\n", @"\n");
            windowName = windowName.Replace("'", @"\'");
            url = url.Replace("'", @"\'");
            StringBuilder builder = new StringBuilder();
            builder.Append("var lef = (window.screen.width-");
            builder.Append(width);
            builder.Append(")/2;\n");
            builder.Append("var to = (window.screen.height-");
            builder.Append(height);
            builder.Append(")/2;\n");
            builder.Append("var PeterLee=window.open('");
            builder.Append(url);
            builder.Append("','");
            builder.Append(windowName);
            builder.Append("','");
            if (isWithToolbar)
            {
                builder.Append("toolbar=yes,");
            }
            if (isWithMenubar)
            {
                builder.Append("menubar=yes,");
            }
            if (resizable)
            {
                builder.Append("resizable=yes,");
            }
            if (isWithStatusbar)
            {
                builder.Append("status=yes,");
            }
            if (isWithScrollbars)
            {
                builder.Append("scrollbars=yes,");
            }
            if (isWithLocation)
            {
                builder.Append("location=yes,");
            }
            builder.Append("width=");
            builder.Append(width);
            builder.Append(",height=");
            builder.Append(height);
            builder.Append(",left=' + lef + ',top=' + to);\n");
            if (isFront)
            {
                builder.Append("PeterLee.focus();\n");
            }
            else
            {
                builder.Append("PeterLee.blur();\n");
            }
            if (autoCloseInSeconds > 0)
            {
                try
                {
                    autoCloseInSeconds *= 0x3e8;
                }
                catch
                {
                    return null;
                }
                builder.Append("setTimeout('PeterLee.close();',");
                builder.Append(autoCloseInSeconds);
                builder.Append(");");
            }
            return builder.ToString();
        }

        public static string GetOpenFullScreenWindowScript(string url, string windowName)
        {
            windowName = windowName.Replace(@"\", @"\\");
            windowName = windowName.Replace("\n", @"\n");
            windowName = windowName.Replace("'", @"\'");
            url = url.Replace("'", @"\'");
            StringBuilder builder = new StringBuilder();
            builder.Append("var PeterLee=window.open('");
            builder.Append(url);
            builder.Append("','");
            builder.Append(windowName);
            builder.Append("','fullscreen=yes');");
            builder.Append("\nPeterLee.focus();");
            return builder.ToString();
        }

        public static string GetOpenWindowScript(string url, string windowName, int height, int width, int left, int top, bool isWithToolbar, bool isWithMenubar, bool resizable, bool isWithLocation, bool isWithStatusbar, bool isWithScrollbars, bool isFront, bool isFullScreen, int autoCloseInSeconds)
        {
            windowName = windowName.Replace(@"\", @"\\");
            windowName = windowName.Replace("\n", @"\n");
            windowName = windowName.Replace("'", @"\'");
            url = url.Replace("'", @"\'");
            StringBuilder builder = new StringBuilder();
            builder.Append("var PeterLee=window.open('");
            builder.Append(url);
            builder.Append("','");
            builder.Append(windowName);
            builder.Append("','");
            if (isWithToolbar)
            {
                builder.Append("toolbar=yes,");
            }
            if (isWithMenubar)
            {
                builder.Append("menubar=yes,");
            }
            if (resizable)
            {
                builder.Append("resizable=yes,");
            }
            if (isWithStatusbar)
            {
                builder.Append("status=yes,");
            }
            if (isWithScrollbars)
            {
                builder.Append("scrollbars=yes,");
            }
            if (isWithLocation)
            {
                builder.Append("location=yes,");
            }
            if (isFullScreen)
            {
                builder.Append("fullscreen=yes,");
            }
            builder.Append("width=");
            builder.Append(width);
            builder.Append(",height=");
            builder.Append(height);
            builder.Append(",left=");
            builder.Append(left);
            builder.Append(",top=");
            builder.Append(top);
            builder.Append("');\n");
            if (isFront)
            {
                builder.Append("PeterLee.focus();\n");
            }
            else
            {
                builder.Append("PeterLee.blur();\n");
            }
            if (autoCloseInSeconds > 0)
            {
                try
                {
                    autoCloseInSeconds *= 0x3e8;
                }
                catch
                {
                    return null;
                }
                builder.Append("setTimeout('PeterLee.close();',");
                builder.Append(autoCloseInSeconds);
                builder.Append(");");
            }
            return builder.ToString();
        }

        public static string GetRedirectScript(string url)
        {
            url = url.Replace("'", @"\'");
            return ("top.location.replace('" + url + "');");
        }

        public static string GetReturnConfirmScript(string displayInfo)
        {
            displayInfo = displayInfo.Replace(@"\", @"\\");
            displayInfo = displayInfo.Replace("\n", @"\n");
            displayInfo = displayInfo.Replace("'", @"\'");
            return ("if(!confirm('" + displayInfo + "')) return false;");
        }

        public static string GetSetAsHomePageScript(string url)
        {
            return ("this.style.behavior='url(#default#homepage)';this.setHomePage('" + url + "')");
        }

        public static string GetSetTitleScript(string title)
        {
            title = title.Replace('"', '\'');
            return ("document.title=\"" + title + "\";");
        }

        public static string GetShowModalDialogScript(string url, int width, int height, bool isWithStatusbar, bool isWithHelp, bool resizable)
        {
            url = url.Replace("'", @"\'");
            string str = ((("window.showModalDialog('" + url + "',window,'") + "dialogHeight:" + height) + "px;dialogWidth:" + width) + "px;center:yes;";
            if (isWithStatusbar)
            {
                str = str + "status:yes;";
            }
            else
            {
                str = str + "status:no;";
            }
            if (isWithHelp)
            {
                str = str + "help:yes;";
            }
            else
            {
                str = str + "help:no;";
            }
            if (resizable)
            {
                str = str + "resizable:yes;";
            }
            else
            {
                str = str + "resizable:no;";
            }
            return (str + "');");
        }

        public static string GetShowModelessDialogScript(string url, int width, int height, bool isWithStatusbar, bool isWithHelp, bool resizable)
        {
            url = url.Replace("'", @"\'");
            string str = ((("window.showModelessDialog('" + url + "',window,'") + "dialogHeight:" + height) + "px;dialogWidth:" + width) + "px;center:yes;";
            if (isWithStatusbar)
            {
                str = str + "status:yes;";
            }
            else
            {
                str = str + "status:no;";
            }
            if (isWithHelp)
            {
                str = str + "help:yes;";
            }
            else
            {
                str = str + "help:no;";
            }
            if (resizable)
            {
                str = str + "resizable:yes;";
            }
            else
            {
                str = str + "resizable:no;";
            }
            return (str + "');");
        }

        public static string GetShowPopUpScript(out string showPopUpFunctionName, out string hidePopUpFunctionName, string backGroundColor, string borderStyle, string innerHTML, int height, int width)
        {
            innerHTML = innerHTML.Replace("\n", "<br/>");
            innerHTML = innerHTML.Replace(@"\", @"\\");
            innerHTML = innerHTML.Replace("'", @"\'");
            string str = "PopUp" + keyForScript;
            object obj2 = (((((string.Empty + "var " + str + " = window.createPopup();\n") + "function Show" + str + "(lef,to){\n") + "var popupBody = " + str + ".document.body;\n") + "popupBody.style.backgroundColor = '" + backGroundColor + "';\n") + "popupBody.style.border = '" + borderStyle + "';\n") + "popupBody.innerHTML = '" + innerHTML + "';\n";
            string str2 = ((string.Concat(new object[] { obj2, str, ".show(lef,to,", width, ",", height, ",document.body);\n" }) + "}\n") + "function Hide" + str + "(){") + str + ".hide();}\n";
            showPopUpFunctionName = "Show" + str + "(event.clientX,event.clientY)";
            hidePopUpFunctionName = "Hide" + str + "()";
            return str2;
        }

        public static string GetShowPopUpScript(out string showPopUpFunctionName, out string hidePopUpFunctionName, string backGroundColor, string borderStyle, string innerHTML, int height, int width, int left, int top)
        {
            innerHTML = innerHTML.Replace("\n", "<br/>");
            innerHTML = innerHTML.Replace(@"\", @"\\");
            innerHTML = innerHTML.Replace("'", @"\'");
            string str = "PopUp" + keyForScript;
            object obj2 = (((((string.Empty + "var " + str + " = window.createPopup();\n") + "function Show" + str + "(){\n") + "var popupBody = " + str + ".document.body;\n") + "popupBody.style.backgroundColor = '" + backGroundColor + "';\n") + "popupBody.style.border = '" + borderStyle + "';\n") + "popupBody.innerHTML = '" + innerHTML + "';\n";
            string str2 = ((string.Concat(new object[] { obj2, str, ".show(", left, ",", top, ",", width, ",", height, ",document.body);\n" }) + "}\n") + "function Hide" + str + "(){") + str + ".hide();}\n";
            showPopUpFunctionName = "Show" + str + "()";
            hidePopUpFunctionName = "Hide" + str + "()";
            return str2;
        }

        public static bool GoBack()
        {
            return InsertScriptToPage(GoBackScript);
        }

        public static bool GoBackWithRefresh()
        {
            return InsertScriptToPage(GoBackWithRefreshScript);
        }

        public static bool InsertScriptToPage(string javaScript)
        {
            return InsertScriptToPage(keyForScript, javaScript, true);
        }

        public static bool InsertScriptToPage(string script, bool addScriptTag)
        {
            return InsertScriptToPage(keyForScript, script, addScriptTag);
        }

        public static bool InsertScriptToPage(string key, string javaScript)
        {
            return InsertScriptToPage(key, javaScript, true);
        }

        public static bool InsertScriptToPage(string key, string script, bool addScriptTag)
        {
            if (((string.Empty == key) || (key == null)) || (null == script))
            {
                throw new ArgumentException("Invalid Argument");
            }
            try
            {
                ClientScriptManager clientScript = ((Page) HttpContext.Current.Handler).ClientScript;
                if (!clientScript.IsClientScriptBlockRegistered(key))
                {
                    clientScript.RegisterClientScriptBlock(typeof(string), key, script, addScriptTag);
                    return true;
                }
                return false;
            }
            catch
            {
                return true;
            }
        }

        public static bool KillErrors()
        {
            return InsertScriptToPage(KillErrorsScript);
        }

        public static bool KillErrors(string key)
        {
            return InsertScriptToPage(key, KillErrorsScript);
        }

        public static bool NoBeFramed()
        {
            return InsertScriptToPage(NoBeFramedScript);
        }

        public static bool NoBeFramed(string key)
        {
            return InsertScriptToPage(key, NoBeFramedScript);
        }

        public static bool NoSaved()
        {
            return InsertScriptToPage(NoSavedHTML, false);
        }

        public static bool NoSaved(string key)
        {
            return InsertScriptToPage(key, NoSavedHTML, false);
        }

        public static bool OpenCentralWindow(string url, string windowName, int height, int width)
        {
            return InsertScriptToPage(GetOpenCentralWindowScript(url, windowName, height, width));
        }

        public static bool OpenCentralWindow(string key, string url, string windowName, int height, int width)
        {
            return InsertScriptToPage(key, GetOpenCentralWindowScript(url, windowName, height, width));
        }

        public static bool OpenCentralWindow(string url, string windowName, int height, int width, bool isWithToolbar, bool isWithMenubar, bool resizable, bool isWithLocation, bool isWithStatusbar, bool isWithScrollbars, bool isFront, int autoCloseInSeconds)
        {
            return InsertScriptToPage(GetOpenCentralWindowScript(url, windowName, height, width, isWithToolbar, isWithMenubar, resizable, isWithLocation, isWithStatusbar, isWithScrollbars, isFront, autoCloseInSeconds));
        }

        public static bool OpenCentralWindow(string key, string url, string windowName, int height, int width, bool isWithToolbar, bool isWithMenubar, bool resizable, bool isWithLocation, bool isWithStatusbar, bool isWithScrollbars, bool isFront, int autoCloseInSeconds)
        {
            return InsertScriptToPage(key, GetOpenCentralWindowScript(url, windowName, height, width, isWithToolbar, isWithMenubar, resizable, isWithLocation, isWithStatusbar, isWithScrollbars, isFront, autoCloseInSeconds));
        }

        public static bool OpenFullScreenWindow(string url, string windowName)
        {
            return InsertScriptToPage(GetOpenFullScreenWindowScript(url, windowName));
        }

        public static bool OpenFullScreenWindow(string key, string url, string windowName)
        {
            return InsertScriptToPage(key, GetOpenFullScreenWindowScript(url, windowName));
        }

        public static bool OpenWindow(string url, string windowName, int height, int width)
        {
            return OpenCentralWindow(url, windowName, height, width);
        }

        public static bool OpenWindow(string key, string url, string windowName, int height, int width)
        {
            return OpenCentralWindow(key, url, windowName, height, width);
        }

        public static bool OpenWindow(string url, string windowName, int height, int width, int left, int top, bool isWithToolbar, bool isWithMenubar, bool resizable, bool isWithLocation, bool isWithStatusbar, bool isWithScrollbars, bool isFront, bool isFullScreen, int autoCloseInSeconds)
        {
            return InsertScriptToPage(GetOpenWindowScript(url, windowName, height, width, left, top, isWithToolbar, isWithMenubar, resizable, isWithLocation, isWithStatusbar, isWithScrollbars, isFront, isFullScreen, autoCloseInSeconds));
        }

        public static bool OpenWindow(string key, string url, string windowName, int height, int width, int left, int top, bool isWithToolbar, bool isWithMenubar, bool resizable, bool isWithLocation, bool isWithStatusbar, bool isWithScrollbars, bool isFront, bool isFullScreen, int autoCloseInSeconds)
        {
            return InsertScriptToPage(key, GetOpenWindowScript(url, windowName, height, width, left, top, isWithToolbar, isWithMenubar, resizable, isWithLocation, isWithStatusbar, isWithScrollbars, isFront, isFullScreen, autoCloseInSeconds));
        }

        public static bool Redirect(string url)
        {
            return InsertScriptToPage(GetRedirectScript(url));
        }

        public static bool ReloadPageForNav4OnResized()
        {
            return InsertScriptToPage(ReloadPageForNav4OnResizedScript);
        }

        public static bool ReloadPageForNav4OnResized(string key)
        {
            return InsertScriptToPage(key, ReloadPageForNav4OnResizedScript);
        }

        public static bool SetTitle(string title)
        {
            return InsertScriptToPage(GetSetTitleScript(title));
        }

        public static bool SetTitle(string key, string title)
        {
            return InsertScriptToPage(key, GetSetTitleScript(title));
        }

        public static bool ShowModalDialog(string url, int width, int height)
        {
            return InsertScriptToPage(GetShowModalDialogScript(url, width, height, false, false, false));
        }

        public static bool ShowModalDialog(string key, string url, int width, int height)
        {
            return InsertScriptToPage(key, GetShowModalDialogScript(url, width, height, false, false, false));
        }

        public static bool ShowModalDialog(string url, int width, int height, bool isWithStatusbar, bool isWithHelp, bool resizable)
        {
            return InsertScriptToPage(GetShowModalDialogScript(url, width, height, isWithStatusbar, isWithHelp, resizable));
        }

        public static bool ShowModalDialog(string key, string url, int width, int height, bool isWithStatusbar, bool isWithHelp, bool resizable)
        {
            return InsertScriptToPage(key, GetShowModalDialogScript(url, width, height, isWithStatusbar, isWithHelp, resizable));
        }

        public static bool ShowModelessDialog(string url, int width, int height)
        {
            return InsertScriptToPage(GetShowModelessDialogScript(url, width, height, false, false, false));
        }

        public static bool ShowModelessDialog(string key, string url, int width, int height)
        {
            return InsertScriptToPage(key, GetShowModelessDialogScript(url, width, height, false, false, false));
        }

        public static bool ShowModelessDialog(string url, int width, int height, bool isWithStatusbar, bool isWithHelp, bool resizable)
        {
            return InsertScriptToPage(GetShowModelessDialogScript(url, width, height, isWithStatusbar, isWithHelp, resizable));
        }

        public static bool ShowModelessDialog(string key, string url, int width, int height, bool isWithStatusbar, bool isWithHelp, bool resizable)
        {
            return InsertScriptToPage(key, GetShowModelessDialogScript(url, width, height, isWithStatusbar, isWithHelp, resizable));
        }

        public static string CloseWindowNoConfirmScript
        {
            get
            {
                return "window.openner=null;window.top.close();";
            }
        }

        public static string CloseWindowScript
        {
            get
            {
                return "window.top.close();";
            }
        }

        public static string CloseWindowWithRefreshOpenerScript
        {
            get
            {
                return "window.opener.location.reload();window.top.close();";
            }
        }

        public static string GoBackScript
        {
            get
            {
                return "history.back()";
            }
        }

        public static string GoBackWithRefreshScript
        {
            get
            {
                return "top.location.replace(history.go(-1));";
            }
        }

        public static string keyForScript
        {
            get
            {
                if (keyForScriptCount > 0x2710)
                {
                    keyForScriptCount = 0;
                }
                keyForScriptCount++;
                return (DateTime.Now.ToString("hhmmmss") + keyForScriptCount.ToString("D5"));
            }
        }

        public static string KillErrorsScript
        {
            get
            {
                return "\nfunction killErrors() \r\n{\r\n\treturn true;\r\n}\r\n window.onerror = killErrors;\n";
            }
        }

        public static string LastModifiedScript
        {
            get
            {
                return "document.write( document.lastModified );";
            }
        }

        public static string NoBeFramedScript
        {
            get
            {
                return "if(top.location != self.location) top.location = self.location;";
            }
        }

        public static string NoSavedHTML
        {
            get
            {
                return "<noscript><iframe src=\"*.html\"></iframe></noscript>";
            }
        }

        public static string ReferrerScript
        {
            get
            {
                return "document.write(document.referrer);";
            }
        }

        public static string ReloadPageForNav4OnResizedScript
        {
            get
            {
                return "function MM_reloadPage(init) {\r\nif (init==true) with (navigator) {if ((appName==\"Netscape\")&&(parseInt(appVersion)==4)) {\r\ndocument.MM_pgW=innerWidth; document.MM_pgH=innerHeight; onresize=MM_reloadPage; }}\r\nelse if (innerWidth!=document.MM_pgW || innerHeight!=document.MM_pgH) location.reload();\r\n}\r\n}MM_reloadPage(true);";
            }
        }
    }
}

