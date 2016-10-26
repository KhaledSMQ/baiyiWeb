using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Game.Facade;
using Game.Kernel;

namespace Game.Web.ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class checkRegUserName : IHttpHandler
    {

        private AccountsFacade accountsFacade = new AccountsFacade();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string logName = context.Request.Params["logName"];
            Message umsg = this.accountsFacade.IsAccountsExist(logName);
            if (!umsg.Success)
            {
                context.Response.Write(umsg.Content);
            }
            else
            {
                context.Response.Write("success");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
