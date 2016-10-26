namespace Game.Kernel
{
    using Game.Utils;
    using System;
    using System.Text;
    using System.Web;

    public abstract class WebBaseHandler : IHttpHandler
    {
        protected WebBaseHandler()
        {
        }

        protected HttpResponse GetResponse(HttpContext context)
        {
            if (context == null)
            {
                return null;
            }
            return context.Response;
        }

        protected abstract void OutputResponse(HttpContext context);
        public void ProcessRequest(HttpContext context)
        {
            this.OutputResponse(context);
        }

        protected void ResponseJavaScripts(HttpContext context, StringBuffer jsText)
        {
            this.ResponseJavaScripts(context, jsText.ToString());
        }

        protected void ResponseJavaScripts(HttpContext context, string jsText)
        {
            HttpResponse response = this.GetResponse(context);
            response.Clear();
            response.ContentType = "text/javascript";
            response.Cache.SetExpires(DateTime.Now.AddHours(1.0));
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetValidUntilExpires(false);
            response.Write("document.write('" + jsText.Replace("'", @"\'").Replace("\r\n", "") + "');");
            response.End();
        }

        protected void ResponseJavaScripts(HttpContext context, StringBuilder jsText)
        {
            this.ResponseJavaScripts(context, jsText.ToString());
        }

        protected void ResponseJavaScriptsForFile(HttpContext context, StringBuffer jsText)
        {
            this.ResponseJavaScriptsForFile(context, jsText.ToString());
        }

        protected void ResponseJavaScriptsForFile(HttpContext context, string jsText)
        {
            HttpResponse response = this.GetResponse(context);
            response.Clear();
            response.ContentType = "text/javascript";
            response.Cache.SetExpires(DateTime.Now.AddHours(1.0));
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetValidUntilExpires(false);
            response.Write(jsText);
            response.End();
        }

        protected void ResponseJavaScriptsForFile(HttpContext context, StringBuilder jsText)
        {
            this.ResponseJavaScriptsForFile(context, jsText.ToString());
        }

        protected void ResponseJSON(HttpContext context, string text)
        {
            HttpResponse response = this.GetResponse(context);
            response.Clear();
            response.AddHeader("P3P", "CP=CURa ADMa DEVa PSAo PSDo OUR BUS UNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR");
            response.ContentType = "application/json";
            response.Expires = 0;
            response.Cache.SetNoStore();
            response.Write(text);
            response.End();
        }

        protected void ResponseText(HttpContext context, StringBuffer textBuilder)
        {
            this.ResponseText(context, textBuilder.ToString());
        }

        protected void ResponseText(HttpContext context, string text)
        {
            HttpResponse response = this.GetResponse(context);
            response.Clear();
            response.ContentType = "text/plain";
            response.Cache.SetExpires(DateTime.Now.AddHours(1.0));
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetValidUntilExpires(false);
            response.Write(text);
            response.End();
        }

        protected void ResponseText(HttpContext context, StringBuilder textBuilder)
        {
            this.ResponseText(context, textBuilder.ToString());
        }

        protected void ResponseXML(HttpContext context, StringBuffer xmlnode)
        {
            this.ResponseXML(context, xmlnode.ToString());
        }

        protected void ResponseXML(HttpContext context, string xml)
        {
            HttpResponse response = this.GetResponse(context);
            response.Clear();
            response.ContentType = "Text/XML";
            response.Expires = 0;
            response.Cache.SetNoStore();
            response.Write(xml);
            response.End();
        }

        protected void ResponseXML(HttpContext context, StringBuilder xmlnode)
        {
            this.ResponseXML(context, xmlnode.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}

