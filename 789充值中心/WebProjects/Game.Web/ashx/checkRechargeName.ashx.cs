using Game.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Web.ashx
{
    /// <summary>
    /// checkRechargeName 的摘要说明
    /// </summary>
    public class checkRechargeName : IHttpHandler
    {
        AccountsFacade accountsfacade = new AccountsFacade();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string accountsName=context.Request.Form["rechargeUser"]??string.Empty;

            if(!string.IsNullOrEmpty(accountsName)){
            string sqlQuery = string.Format("select UserID from QPAccountsDB.dbo.AccountsInfo where Accounts='{0}'",accountsName);
            string userid = accountsfacade.GetObjectBySql(sqlQuery) == null ? "" : accountsfacade.GetObjectBySql(sqlQuery).ToString();
            if (!string.IsNullOrEmpty(userid))
            {
                context.Response.Write("0");
            }
            else
            {
                context.Response.Write("-2");
            }
            }
            else{
                context.Response.Write("-1");
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