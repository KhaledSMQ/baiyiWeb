using Game.Entity.Treasure;
using Game.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Game.Web.ashx
{
    /// <summary>
    /// getOrderStatus 的摘要说明
    /// </summary>
    public class getOrderStatus : IHttpHandler,IReadOnlySessionState 
    {
        TreasureFacade treasurefacade = new TreasureFacade();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string order = context.Request.Cookies["PayOrder"] == null ? "0" : context.Request.Cookies["PayOrder"]["order"];
            string orderuser = context.Request.Cookies["PayOrder"] == null ? "0" : HttpUtility.UrlDecode(context.Request.Cookies["PayOrder"]["orderUser"]);
            string orderMoney = context.Request.Cookies["PayOrder"] == null ? "0" : context.Request.Cookies["PayOrder"]["orderMoney"];
            string errorMsg = context.Request.Cookies["ErrorMsg"] == null ? "" : HttpUtility.UrlDecode(context.Request.Cookies["ErrorMsg"]["error"]);
            if (string.IsNullOrEmpty(errorMsg))
            {
                errorMsg = "0";
            }
            if (!string.IsNullOrEmpty(order))
            {
                OnLineOrder onlineorder = treasurefacade.GetOnlineOrder(order);
                if (onlineorder == null)
                {
                    context.Response.Write(orderuser + '*' + orderMoney + '*' + '0' + '*' + '0' + '*' + errorMsg + '*' + order);              
                   
                }
                else
                {
                    context.Response.Write(onlineorder.Accounts + '*' + onlineorder.CardPrice + '*' + onlineorder.CardGold + '*' + onlineorder.OrderStatus + '*' + errorMsg + '*' + order);
                }
            }
            else
            {
                context.Response.Write(orderuser + '*' + orderMoney + '*' + '0' + '*' + '0' + '*' + errorMsg + '*' + order);         
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