using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Game.Entity.GameWeb;
using Game.Entity.Platform;
using Game.Entity.Accounts;
using Game.Facade;
using Game.Kernel;
using Game.Utils;

namespace Game.Web.ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler1 : IHttpHandler
    {
        private AccountsFacade accountsFacade = new AccountsFacade();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string Accounts = context.Request.Params["Ilogname"];
            string NickName = context.Request.Params["InickName"];
            //string Compellation = context.Request.Params["Iname"];
            string LogonPass = context.Request.Params["Ipwd"];
            //string PassPortID = context.Request.Params["Iid"];
            string Spreader = context.Request.Params["Ispre"];
            UserInfo user = new UserInfo();
            user.Accounts = Accounts;
            user.Compellation = "";
            //user.FaceID = Convert.ToInt16(hfFaceID.Value.Trim());
            // user.Gender = Convert.ToByte(ddlGender.SelectedValue);
            user.InsurePass = TextEncrypt.EncryptPassword(LogonPass);
            user.LastLogonDate = DateTime.Now;
            user.LastLogonIP = GameRequest.GetUserIP();
            user.LogonPass = TextEncrypt.EncryptPassword(LogonPass);
            user.NickName = NickName;
            user.PassPortID = "";
            user.RegisterDate = DateTime.Now;
            user.RegisterIP = GameRequest.GetUserIP();

            string strType = "";
            if (System.Web.HttpContext.Current.Request.Cookies["asd"] != null)
            {
                strType = System.Web.HttpContext.Current.Request.Cookies["asd"].Value.ToString();
            }

            int nType = 8;
            if (strType == "2")
            {
                nType = 2;
            }
            else if (strType == "5")
            {
                nType = 5;
            }

            Message msg = accountsFacade.Register(user, Spreader, nType);
            if (msg.Success)
            {
                UserInfo ui = msg.EntityList[0] as UserInfo;
                ui.LogonPass = TextEncrypt.EncryptPassword(LogonPass);
                Fetch.SetUserCookie(ui.ToUserTicketInfo());
                context.Response.Write("success");
            }
            else
            {
                context.Response.Write(msg.Content);
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
