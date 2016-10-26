using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Game.Kernel;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;

namespace Game.Web.ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class cheeckSkState : IHttpHandler
    {
        TreasureFacade treasureFacade = new TreasureFacade();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strAccounts = context.Request.Params["txtAccounts"];
            string strReAccounts = context.Request.Params["txtAccounts2"];
            string serialID = context.Request.Params["txtSerialID"];
            string password = TextEncrypt.EncryptPassword(context.Request.Params["txtPassword"]);

            if (strAccounts == "")
            {
                context.Response.Write("1");
                // RenderAlertInfo(true, "抱歉，请输入游戏账号。", 2);
                return;
            }
            if (strReAccounts != strAccounts)
            {
                context.Response.Write("2");
                //RenderAlertInfo(true, "抱歉，两次输入的帐号不一致。", 2);
                return;
            }
            if (serialID == "")
            {
                context.Response.Write("3");
               // RenderAlertInfo(true, "抱歉，请输入充值卡号。", 2);
                return;
            }
            if (password == "")
            {
                context.Response.Write("4");
               // RenderAlertInfo(true, "抱歉，请输入卡号密码。", 2);
                return;
            }

            //充值信息
            ShareDetialInfo detailInfo = new ShareDetialInfo();
            detailInfo.SerialID = serialID;
            detailInfo.OperUserID = 0;
            detailInfo.Accounts = strAccounts;
            detailInfo.ShareID = 1;             //实卡充值服务标识
            detailInfo.IPAddress = Utility.UserIP;

            #region 充值

            Message umsg = treasureFacade.FilledLivcard(detailInfo, password);
            if (umsg.Success)
            {
                context.Response.Write("0");
                //RenderAlertInfo(false, "实卡充值成功。", 2);
            }
            else
            {
                context.Response.Write(umsg.Content);
               // RenderAlertInfo(true, umsg.Content, 2);
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
            #endregion