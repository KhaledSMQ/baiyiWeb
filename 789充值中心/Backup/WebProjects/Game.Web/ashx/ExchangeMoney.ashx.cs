using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Game.Facade;
using Game.Utils;
using Game.Kernel;

namespace Game.Web.ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ExchangeMoney : IHttpHandler
    {
        TreasureFacade tf = new TreasureFacade();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string username = context.Request["Accounts"];
                decimal money = decimal.Parse(context.Request["ChangeMoney"]);
                string ip = GameRequest.GetUserIP();
                Message msg = tf.ChangeMoney(username, money, ip);
                if (msg.Success)
                {
                    context.Response.Write("1");

                }
                else
                {

                    context.Response.Write(msg.Content);
                }
            }
            catch (Exception ex)
            {

                context.Response.Write("出现异常，请重试");
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
