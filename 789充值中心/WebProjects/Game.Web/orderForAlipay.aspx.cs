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
using System.Configuration;
using Game.Entity.Accounts;
namespace Game.Web
{
    public partial class orderForAlipay : System.Web.UI.Page
    {

        /// <summary>
        /// 自己需要的参数和方法
        /// </summary>
        protected string payZfbUser;
        protected string payZfbMoney;
        protected string payOtherMoney;
        private AccountsFacade accountsfacade = new AccountsFacade();
        private TreasureFacade treasurefacade = new TreasureFacade();

        protected void Page_Load(object sender, EventArgs e)
        {
            Request.Cookies.Remove("ErrorMsg");

            payZfbUser = Request.Form["payZfbUser"] ?? string.Empty;
            payZfbMoney = Request.Form["payZfbMoney"] ?? string.Empty;
            payOtherMoney = Request.Form["selfMoney"] ?? string.Empty;
            if (payZfbMoney.Equals("0"))
            {
                payZfbMoney = "100";
            }
            if (!string.IsNullOrEmpty(payOtherMoney))
            {
                payZfbMoney = payOtherMoney;
            }
            if (!string.IsNullOrEmpty(payZfbUser) && !string.IsNullOrEmpty(payZfbMoney))
            {
                AccountsInfo accountsInfo = accountsfacade.GetAccountsByAccontsName(payZfbUser);
                if (accountsInfo == null)
                {
                    HttpCookie UserCookie2 = new HttpCookie("ErrorMsg");
                    UserCookie2["error"] = HttpUtility.UrlEncode("用户名不存在");
                    UserCookie2.Expires = DateTime.Now.AddMinutes(7);
                    Response.Cookies.Add(UserCookie2);
                    Response.Redirect("/showpayInfo.html");
                }

                try
                {
                    int.Parse(payZfbMoney);
                }
                catch (Exception)
                {
                    return;
                }

                //商户订单号
                string out_trade_no = "ZFB-" + DateTime.Now.ToString("yyyyMMddhhmmss");


                HttpCookie UserCookie = new HttpCookie("PayOrder");
                UserCookie["order"] = out_trade_no;
                UserCookie["orderMoney"] = Request["payZfbMoney"];
                UserCookie["orderUser"] = HttpUtility.UrlEncode(payZfbUser);
                UserCookie.Expires = DateTime.Now.AddMinutes(7);
                Response.Cookies.Add(UserCookie);

                OnLineOrder onlineOrder = new OnLineOrder();
                onlineOrder.Accounts = payZfbUser;
                onlineOrder.UserID = accountsInfo.UserID;
                onlineOrder.OrderAmount = decimal.Parse(Request["payZfbMoney"]);
                onlineOrder.OrderID = out_trade_no;
                onlineOrder.OrderStatus = 0;
                onlineOrder.ShareID = 20;
                onlineOrder.CardTotal = 1;
                onlineOrder.CardTypeID = 1;//支付宝充值
                onlineOrder.TelPhone = "";
                onlineOrder.IPAddress = GameRequest.GetUserIP();
                Message msg = treasurefacade.RequestOrder(onlineOrder);
                if (msg.Success)
                {
                    //支付类型
                    string payment_type = "1";
                    //服务器异步通知页面路径
                    string notify_url = ConfigurationManager.AppSettings["ailpayNotify"];
                    //页面跳转同步通知页面路径
                    string return_url = ConfigurationManager.AppSettings["ailpayCallback"];
                    //卖家支付宝帐户
                    string seller_email = ConfigurationManager.AppSettings["ailpayAccount"];
                    //订单名称
                    string subject = ConfigurationManager.AppSettings["shoppingInfo"];
                    //付款金额
                    string total_fee = Convert.ToString(decimal.Parse(payZfbMoney));
                    //订单描述
                    string body = ConfigurationManager.AppSettings["shoppingInfo"];
                    //商品展示地址
                    string show_url = "";
                    //防钓鱼时间戳
                    string anti_phishing_key = "";
                    //若要使用请调用类文件submit中的query_timestamp函数
                    //客户端的IP地址
                    string exter_invoke_ip = "";
                    //把请求参数打包成数组
                    SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
                    sParaTemp.Add("partner", Config.Partner);
                    sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
                    sParaTemp.Add("service", "create_direct_pay_by_user");
                    sParaTemp.Add("payment_type", payment_type);
                    sParaTemp.Add("notify_url", notify_url);
                    sParaTemp.Add("return_url", return_url);
                    sParaTemp.Add("seller_email", seller_email);
                    sParaTemp.Add("out_trade_no", out_trade_no);
                    sParaTemp.Add("subject", subject);
                    sParaTemp.Add("total_fee", total_fee);
                    sParaTemp.Add("body", body);
                    sParaTemp.Add("show_url", show_url);
                    sParaTemp.Add("anti_phishing_key", anti_phishing_key);
                    sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

                    string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
                    Response.Write(sHtmlText);
                }
                else
                {
                    HttpCookie UserCookie2 = new HttpCookie("ErrorMsg");
                    UserCookie2["error"] =HttpUtility.UrlEncode(msg.Content);
                    UserCookie2.Expires = DateTime.Now.AddMinutes(7);
                    Response.Cookies.Add(UserCookie2);
                    Response.Redirect("/showpayInfo.html");
                }
            }
            else
            {


            }
        }
    }
}
