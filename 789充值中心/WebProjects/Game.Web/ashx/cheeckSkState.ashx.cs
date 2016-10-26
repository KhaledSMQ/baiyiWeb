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
            string strAccounts = context.Request.Form["txtAccounts"]??string.Empty;
            string serialID = context.Request.Form["txtSerialID"]??string.Empty;
            string password =context.Request.Form["txtPassword"]??string.Empty;

            if (string.IsNullOrEmpty(strAccounts) || string.IsNullOrEmpty(serialID) || string.IsNullOrEmpty(password))
            {
                context.Response.Write("-1");
            }
            ShareDetialInfo detailInfo = new ShareDetialInfo();
            detailInfo.SerialID = serialID;
            detailInfo.OperUserID = 0;
            detailInfo.Accounts = strAccounts;
            detailInfo.ShareID = 1;             //实卡充值服务标识
            detailInfo.IPAddress = Utility.UserIP;
            #region 充值
            Message umsg = treasureFacade.FilledLivcard(detailInfo, TextEncrypt.EncryptPassword(password));
            if (umsg.Success)
            {
                context.Response.Write("0");
            }
            else
            {
                context.Response.Write(umsg.Content);
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