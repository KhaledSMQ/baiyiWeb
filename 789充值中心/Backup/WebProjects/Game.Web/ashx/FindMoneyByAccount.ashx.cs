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
    public class FindMoneyByAccount : IHttpHandler
    {
        TreasureFacade tf = new TreasureFacade();
        AccountsFacade af = new AccountsFacade();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string username=context.Request["Accounts"];
            Message umsg = this.af.IsAccountsExist(username);
            if (!umsg.Success)
            {


                string sqlquery = string.Format("select MoneyPre from [QPTreasureDB].[dbo].ChangeMoneyPre where UserID=(select UserID from [QPAccountsDB].[dbo].AccountsInfo where Accounts='" + username + "')");
                string count = tf.GetMoneyCountByAccount(sqlquery).ToString();
                decimal money = decimal.Parse(count);
                context.Response.Write(money.ToString());
            }
            else
            {

                context.Response.Write("0");
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
