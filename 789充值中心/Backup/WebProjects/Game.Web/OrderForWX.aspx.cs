using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
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
namespace Game.Web
{
    public partial class OrderForWX : System.Web.UI.Page
    {
        //加密和解密的秘钥outOrder_no=wxzf20140708072914<br/>sign=87fb64428b967c6ac455c71330e35d08cded340c0599c0b9fbbfcef1ec3d8623
        public  string pwd = ConfigurationManager.AppSettings["weixin_pwd"];
        //商户agCode
        string agCode = ConfigurationManager.AppSettings["weixin_agCode"];
        //商户编号
        string mId = ConfigurationManager.AppSettings["weixin_mId"];
        //通知地址weixin_notify
        string notify_url = ConfigurationManager.AppSettings["weixin_notify"];
        //支付地址
        string url = ConfigurationManager.AppSettings["weixin_url"];
        weixinDES weixindes = new weixinDES();
        private AccountsFacade accountsFacade = new AccountsFacade();
        private TreasureFacade treasureFacade = new TreasureFacade();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["wxPayUser"]) && !string.IsNullOrEmpty(Request["wxPayMoney"]))
            {
                //用户名
                string pay_username = Request["wxPayUser"];
                //支付金额(单位以分为单位，插入数据库的不边，提交到支付的要*100）
                string PayMoney = Request["wxPayMoney"];
                //订单号
                string outOrder_no = "wxzf" + DateTime.Now.ToString("yyyyMMddhhmmss");
                //生成订单
                int uid = accountsFacade.GetAccountsId(pay_username);
                OnLineOrder onlineOrder = new OnLineOrder();
                onlineOrder.Accounts = pay_username;
                onlineOrder.UserID = uid;
                onlineOrder.OrderAmount = decimal.Parse(PayMoney);
                onlineOrder.OrderID = outOrder_no;
                onlineOrder.OrderStatus = 0;
                onlineOrder.ShareID = 20;
                onlineOrder.CardTotal = 1;
                onlineOrder.CardTypeID = 1;//卡类充值
                onlineOrder.TelPhone = "";
                onlineOrder.IPAddress = GameRequest.GetUserIP();
                Message msg = treasureFacade.RequestOrder(onlineOrder);
                if (!msg.Success)
                {
                    Response.Redirect("/Tips.aspx?msg=" + msg.Content);
                }
                else
                {   //明文数据
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

                        Response.Redirect(message);
                    }
                    else
                    {
                        Response.Redirect("/Tips.aspx?msg=" + message);

                    }

                }
            }
               //当输入信息不正确是跳到该页面
            else
            {

                Response.Redirect("/Tips.aspx?msg=生成订单出错。请重试");

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