using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Alipay;
using Game.Entity.Treasure;
using Game.Kernel;
using Game.Facade;
using Game.Utils;
using System.Collections.Specialized;
namespace Game.Web
{
    public partial class callbackForAlipy : System.Web.UI.Page
    {
        TreasureFacade treasurefacade = new TreasureFacade();

        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request["notify_id"], Request["sign"]);
                if (verifyResult)//验证成功
                {
                    //商户订单号
                    string out_trade_no = Request["out_trade_no"];

                    //支付宝交易号
                    string trade_no = Request["trade_no"];

                    //交易状态
                    string trade_status = Request["trade_status"];

                    //交易金额
                    string total_fee = Request["total_fee"];

                    if (Request["trade_status"] == "TRADE_FINISHED")
                    {
                        Response.Write("yes");
                        Request.Cookies.Remove("ErrorMsg");
                        Response.Redirect("/showPayInfo.html");
                    }
                    else if (Request["trade_status"] == "TRADE_SUCCESS")
                    {
                        Response.Write("yes");

                        Request.Cookies.Remove("ErrorMsg");
                        Response.Redirect("/showPayInfo.html");
                    }
                    else
                    {

                    }             
                }
                else//验证失败
                {
                    HttpCookie UserCookie2 = new HttpCookie("ErrorMsg");
                    UserCookie2["error"] = HttpUtility.UrlEncode("验证失败");
                    UserCookie2.Expires = DateTime.Now.AddMinutes(7);
                    Response.Cookies.Add(UserCookie2);
                    Response.Redirect("/showPayInfo.html");
                }
            }
            else
            {
                HttpCookie UserCookie2 = new HttpCookie("ErrorMsg");
                UserCookie2["error"] = HttpUtility.UrlEncode("无通知参数");
                UserCookie2.Expires = DateTime.Now.AddMinutes(7);
                Response.Cookies.Add(UserCookie2);
                Response.Redirect("/showPayInfo.html");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            coll = Request.Form;
            String[] requestItem = coll.AllKeys;
            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }
            return sArray;
        }
    }
}