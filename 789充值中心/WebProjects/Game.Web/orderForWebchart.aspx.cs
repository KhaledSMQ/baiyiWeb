using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using Game.Utils.Utils;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using Game.Entity.Treasure;
using System.Configuration;
using Game.Entity.Accounts;
namespace Game.Web
{
    public partial class orderForWebchart : System.Web.UI.Page
    {
        //加密和解密的秘钥outOrder_no=wxzf20140708072914<br/>sign=87fb64428b967c6ac455c71330e35d08cded340c0599c0b9fbbfcef1ec3d8623

        /// <summary>
        /// 自己需要的参数和方法
        /// </summary>
        protected string webchartUser;
        protected string webchartMoney;
        protected string payOtherMoney;
        private AccountsFacade accountsfacade = new AccountsFacade();
        private TreasureFacade treasurefacade = new TreasureFacade();
       
        /// <summary>
        /// 系统必须的
        /// </summary>
        //商户密码
        public string pwd = ConfigurationManager.AppSettings["WebchartPwd"];
        //商户agCode
        string agCode = ConfigurationManager.AppSettings["WebchartCode"];
        //商户编号
        string mId = ConfigurationManager.AppSettings["WebchartMid"];
        //通知地址
        string notify_url = ConfigurationManager.AppSettings["WebchartNotify"];
        //支付地址
        string url = ConfigurationManager.AppSettings["WebChartUrl"];

        weixinDES weixindes = new weixinDES();

        protected void Page_Load(object sender, EventArgs e)
        {
            Request.Cookies.Remove("ErrorMsg");
            webchartUser = Request.Form["webchartUser"] ?? string.Empty;
            webchartMoney = Request.Form["webchartMoney"] ?? string.Empty;
            payOtherMoney=Request.Form["selfMoney"]??string.Empty;
            if (webchartMoney.Equals("0"))
            {
                webchartMoney = "100";
            }
            if (!string.IsNullOrEmpty(payOtherMoney))
            {
                webchartMoney = payOtherMoney;
            }
            if (!string.IsNullOrEmpty(webchartUser) && !string.IsNullOrEmpty(webchartMoney))
            {
                AccountsInfo accountsInfo = accountsfacade.GetAccountsByAccontsName(webchartUser);
                if (accountsInfo == null)
                {
                    Response.Write("-3");
                    return;
                }
                try
                {
                    int.Parse(webchartMoney);
                }
                catch (Exception)
                {
                    return;
                }
                //用户名
                string pay_username = webchartUser;
                //支付金额(单位以分为单位，插入数据库的不边，提交到支付的要*100）
                string PayMoney = webchartMoney;
                //订单号
                string outOrder_no = "WXZF-" + DateTime.Now.ToString("yyyyMMddhhmmss");

                HttpCookie UserCookie = new HttpCookie("PayOrder");
                UserCookie["order"] = outOrder_no;
                UserCookie["orderMoney"] = PayMoney;
                UserCookie["orderUser"] =HttpUtility.UrlEncode(pay_username);
                UserCookie.Expires = DateTime.Now.AddMinutes(7);
                Response.Cookies.Add(UserCookie);

                OnLineOrder onlineOrder = new OnLineOrder();
                onlineOrder.Accounts = pay_username;
                onlineOrder.UserID = accountsInfo.UserID;
                onlineOrder.OrderAmount = decimal.Parse(PayMoney);
                onlineOrder.OrderID = outOrder_no;
                onlineOrder.OrderStatus = 0;
                onlineOrder.ShareID = 20;
                onlineOrder.CardTotal = 1;
                onlineOrder.CardTypeID = 1;//卡类充值
                onlineOrder.TelPhone = "";
                onlineOrder.IPAddress = GameRequest.GetUserIP();
                Message msg = treasurefacade.RequestOrder(onlineOrder);
                if (msg.Success)
                {
                    //明文数据
                    string param = "mId=" + mId + "&notify_url=" + notify_url + "&outOrder_no=" + outOrder_no + "&total_fee=" + (Convert.ToInt32(PayMoney) * 100).ToString();
                    //加密后生成的sign
                    string sign = weixindes.Encrypt(param, pwd);
                    //传递到支付那边的param
                    string realparam = "agCode=" + agCode + "&sign=" + sign;
                    //post支付信息过去，获取传递回来的数据，json格式
                    string ReturnVal = PostDataGetHtml(url, realparam);
                    //解析json  
                    JObject json = JObject.Parse(ReturnVal);
                    string result = json["result"].ToString();
                    result = result.Replace("\"", "");
                    string message = json["message"].ToString();
                    message = message.Replace("\"", "");
                    //解析result和message，只有当result=0000是才是订单成功，才允许支付，否则，则提交失败到tips.aspx页面
                    if (result.Equals("0000"))
                    {
                        Response.Write(outOrder_no+'*'+message);
                    }
                    else
                    {
                        Response.Write("-1");
                    }
                }
                else
                {
                    Response.Write(msg.Content);
                    
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// 后台手动调用POST
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="postData">传送的数据</param>
        /// <returns></returns>
        private string PostDataGetHtml(string uri, string postData)
        {
            try
            {

                byte[] data = Encoding.GetEncoding("gb2312").GetBytes(postData);
                Uri uRI = new Uri(uri);
                HttpWebRequest req = WebRequest.Create(uRI) as HttpWebRequest;
                req.CookieContainer = new CookieContainer();
                req.Method = "POST";
                req.KeepAlive = true;
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = data.Length;
                req.AllowAutoRedirect = true;
                Stream outStream = req.GetRequestStream();
                outStream.Write(data, 0, data.Length);
                outStream.Close();
                HttpWebResponse res = req.GetResponse() as HttpWebResponse;
                Stream inStream = res.GetResponseStream();
                StreamReader sr = new StreamReader(inStream, Encoding.GetEncoding("gb2312"));
                string htmlResult = sr.ReadToEnd();
                return htmlResult;
            }
            catch (Exception ex)
            {
                return "网络错误：" + ex.Message.ToString();
            }
        }
    }
}