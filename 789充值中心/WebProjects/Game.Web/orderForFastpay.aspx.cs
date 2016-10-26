using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSA.Class;
using Game.Utils;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Kernel;
using payapi_mobile_demo;
using System.Configuration;
using Game.Entity.Accounts;
namespace Game.Web
{
    public partial class orderForFastpay : System.Web.UI.Page
    {
        protected string fastYeepayUser;
        protected string fastYeepayMoney;
        protected string payOtherMoney;
        AccountsFacade accountsfacade = new AccountsFacade();
        TreasureFacade treasurefacade = new TreasureFacade();

        protected void Page_Load(object sender, EventArgs e)
        {
            Request.Cookies.Remove("ErrorMsg");
            fastYeepayUser = Request.Form["fastYeepayUser"] ?? string.Empty;
            fastYeepayMoney = Request.Form["fastYeepayMoney"] ?? string.Empty;
            payOtherMoney = Request.Form["selfMoney"] ?? string.Empty;
            if (fastYeepayMoney.Equals("0"))
            {
                fastYeepayMoney = "100";
            }
            if (!string.IsNullOrEmpty(payOtherMoney))
            {
                fastYeepayMoney = payOtherMoney;
            }
            if (fastYeepayMoney.IndexOf(',') != -1)
            {
                fastYeepayMoney = fastYeepayMoney.Substring(0,fastYeepayMoney.Length-1);
            }
            if (!string.IsNullOrEmpty(fastYeepayUser) && !string.IsNullOrEmpty(fastYeepayMoney))
            {
                AccountsInfo accountsInfo = accountsfacade.GetAccountsByAccontsName(fastYeepayUser);
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
                    int.Parse(fastYeepayMoney);
                }
                catch (Exception)
                {
                    return;
                }
                //订单号
                string orderid = "KJZF-"+ DateTime.Now.ToString("yyyyMMddhhmmss");

                HttpCookie UserCookie = new HttpCookie("PayOrder");
                UserCookie["order"] = orderid;
                UserCookie["orderMoney"] = fastYeepayMoney;
                UserCookie["orderUser"] = HttpUtility.UrlEncode(fastYeepayUser);
                UserCookie.Expires = DateTime.Now.AddMinutes(7);
                Response.Cookies.Add(UserCookie);

                OnLineOrder online = new OnLineOrder();
                online.Accounts = fastYeepayUser;
                online.UserID = accountsInfo.UserID;
                online.OrderAmount = decimal.Parse((Convert.ToInt32(fastYeepayMoney)).ToString());
                online.OrderID = orderid;
                online.OrderStatus = 0;
                online.ShareID = 20;
                online.CardTotal = 1;
                online.CardTypeID = 1;
                online.TelPhone = "";
                online.IPAddress = GameRequest.GetUserIP();
                Message msg = treasurefacade.RequestOrder(online);
                if (msg.Success)
                {
                    //一键支付URL前缀
                    string apiprefix = APIURLConfig.payWebPrefix;

                    //网页支付地址
                    string pcPayURI = APIURLConfig.pcwebURI;

                    //商户账户编号
                    string merchantAccount = Config.merchantAccount;

                    //商户公钥（该商户公钥需要在易宝一键支付商户后台报备）
                    string merchantPublickey = Config.merchantPublickey;

                    //商户私钥（商户公钥对应的私钥）
                    string merchantPrivatekey = Config.merchantPrivatekey;

                    //易宝支付分配的公钥（进入商户后台公钥管理，报备商户的公钥后分派的字符串）
                    string yibaoPublickey = Config.yibaoPublickey;

                    //随机生成商户AESkey
                    string merchantAesKey = payapi_mobile_demo.AES.GenerateAESKey();

                    int amount = Convert.ToInt32(fastYeepayMoney) * 100;//支付金额为分
                    int currency = 156;
                    string identityid = DateTime.Now.ToString("yyyyMMddhhmmss");//用户身份标识
                    int identitytype = 0;
                    string other = "00-23-5A-15-99-42";//mac地址
                    string productcatalog = "1";//商品类别码，商户支持的商品类别码由易宝支付运营人员根据商务协议配置
                    string productdesc = "789游戏中心";
                    string productname = "金币";
                    DateTime t1 = DateTime.Now;
                    DateTime t2 = new DateTime(1970, 1, 1);
                    double t = t1.Subtract(t2).TotalSeconds;
                    int transtime = (int)t;
                    string userip = GameRequest.GetUserIP();
                    //商户提供的商户后台系统异步支付回调地址
                    string callbackurl = ConfigurationManager.AppSettings["fastYeepayNotify"].ToString();
                    //商户提供的商户前台系统异步支付回调地址
                    string fcallbackurl = ConfigurationManager.AppSettings["fastYeepayCallback"].ToString();
                    //用户浏览器ua
                    string userua = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1";
                    SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
                    sd.Add("merchantaccount", merchantAccount);
                    sd.Add("amount", amount);
                    sd.Add("currency", currency);
                    sd.Add("identityid", identityid);
                    sd.Add("identitytype", identitytype);
                    sd.Add("orderid", orderid);
                    sd.Add("other", other);
                    sd.Add("productcatalog", productcatalog);
                    sd.Add("productdesc", productdesc);
                    sd.Add("productname", productname);
                    sd.Add("transtime", transtime);
                    sd.Add("userip", userip);
                    sd.Add("callbackurl", callbackurl);
                    sd.Add("fcallbackurl", fcallbackurl);
                    sd.Add("userua", userua);
                    //生成RSA签名
                    string sign = EncryptUtil.handleRSA(sd, merchantPrivatekey);
                    //Console.WriteLine("生成的签名为：" + sign);
                    sd.Add("sign", sign);
                    //将网页支付对象转换为json字符串
                    string wpinfo_json = Newtonsoft.Json.JsonConvert.SerializeObject(sd);
                    string datastring = payapi_mobile_demo.AES.Encrypt(wpinfo_json, merchantAesKey);
                    //将商户merchantAesKey用RSA算法加密
                    string encryptkey = RSAFromPkcs8.encryptData(merchantAesKey, yibaoPublickey, "UTF-8");
                    //打开浏览器访问一键支付网页支付链接地址，请求方式为get
                    string postParams = "data=" + HttpUtility.UrlEncode(datastring) + "&encryptkey=" + HttpUtility.UrlEncode(encryptkey) + "&merchantaccount=" + merchantAccount;
                    string url = apiprefix + pcPayURI + "?" + postParams;
                    Response.Redirect(url);
                }
                else
                {
                    HttpCookie UserCookie2 = new HttpCookie("ErrorMsg");
                    UserCookie2["error"] = HttpUtility.UrlEncode(msg.Content);
                    UserCookie2.Expires = DateTime.Now.AddMinutes(7);
                    Response.Cookies.Add(UserCookie2);
                    Response.Redirect("/showpayInfo.html");
                }
            }
        }
    }
}