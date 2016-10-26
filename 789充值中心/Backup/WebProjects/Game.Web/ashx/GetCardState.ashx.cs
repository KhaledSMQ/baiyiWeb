using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Game.Entity.Treasure;
using Game.Facade;

namespace Game.Web.ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetCardState : IHttpHandler
    {
        TreasureFacade tf = new TreasureFacade();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string orderID = context.Request.Params["OID"];
            OnLineOrder olo = tf.GetOnlineOrder(orderID);
            if (olo != null)
            {
                if (olo.OrderStatus == 2)
                {
                    context.Response.Write("succes");
                }
                else
                {
                    context.Response.Write("error");
                }
            }
            else
            {
                context.Response.Write("error");
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