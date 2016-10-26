using Game.Entity.Accounts;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tenpayApp;
namespace Game.Web
{
    public partial class orderForTenpay : System.Web.UI.Page
    {
        /// <summary>
        /// 我自己需要的参数
        /// </summary>
        protected string tenpayUser;
        protected string tenpayMoney;
        protected string tenpayBank;
        protected string tenpayOtherMoney;
        protected string tenpayOrder;
        TreasureFacade treasurefacade = new TreasureFacade();
        AccountsFacade accountsfacade = new AccountsFacade();

        protected void Page_Load(object sender, EventArgs e)
        {
            tenpayUser = Request.Form["tenPayUser"] ?? string.Empty;
            tenpayMoney = Request.Form["tenpayMoney"] ?? string.Empty;
            tenpayBank = Request.Form["tenpayBankname"] ?? string.Empty;
            tenpayOtherMoney = Request.Form["selfMoney"] ?? string.Empty;
            if (tenpayMoney.Equals("0"))
            {
                tenpayMoney = "100";
            }
            if (!string.IsNullOrEmpty(tenpayOtherMoney))
            {
                tenpayMoney = tenpayOtherMoney;
            }
            if (!string.IsNullOrEmpty(tenpayUser) && !string.IsNullOrEmpty(tenpayMoney))
            {
                AccountsInfo accountsInfo = accountsfacade.GetAccountsByAccontsName(tenpayUser);
                if (accountsInfo == null)
                {
                    HttpCookie UserCookie2 = new HttpCookie("ErrorMsg");
                    UserCookie2["error"] = HttpUtility.UrlDecode("用户名不存在");
                    UserCookie2.Expires = DateTime.Now.AddMinutes(7);
                    Response.Cookies.Add(UserCookie2);
                    Response.Redirect("/showpayInfo.html");
                }

                try
                {
                    int.Parse(tenpayMoney);
                }
                catch (Exception)
                {
                    HttpCookie UserCookie2 = new HttpCookie("ErrorMsg");
                    UserCookie2["error"] = HttpUtility.UrlDecode("充值金额不对");
                    UserCookie2.Expires = DateTime.Now.AddMinutes(7);
                    Response.Cookies.Add(UserCookie2);
                    Response.Redirect("/showpayInfo.html");
                    return;
                }
                tenpayOrder = "CFT-" + DateTime.Now.ToString("yyyyMMddhhmmss");  //用户订单

                OnLineOrder online = new OnLineOrder();
                online.Accounts = tenpayUser;
                online.UserID = accountsInfo.UserID;
                online.OrderAmount = decimal.Parse((Convert.ToInt32(tenpayMoney)).ToString());
                online.OrderID = tenpayOrder;
                online.OrderStatus = 0;
                online.ShareID = 20;
                online.CardTotal = 1;
                online.CardTypeID = 1;
                online.TelPhone = "";
                online.IPAddress = GameRequest.GetUserIP();
                Message msg = treasurefacade.RequestOrder(online);
                if (msg.Success)
                {
                    //创建RequestHandler实例
                    RequestHandler reqHandler = new RequestHandler(Context);
                    //初始化
                    reqHandler.init();
                    //设置密钥
                    reqHandler.setKey(TenpayUtil.tenpay_key);
                    reqHandler.setGateUrl("https://gw.tenpay.com/gateway/pay.htm");

                    //-----------------------------
                    //设置支付参数
                    //-----------------------------
                    reqHandler.setParameter("partner", TenpayUtil.bargainor_id);		        //商户号
                    reqHandler.setParameter("out_trade_no", tenpayOrder);		//商家订单号
                    reqHandler.setParameter("total_fee", (Convert.ToDouble(tenpayMoney) * 100).ToString());			        //商品金额,以分为单位
                    reqHandler.setParameter("return_url", TenpayUtil.tenpay_return);		    //交易完成后跳转的URL
                    reqHandler.setParameter("notify_url", TenpayUtil.tenpay_notify);		    //接收财付通通知的URL
                    reqHandler.setParameter("body", "789金币购买");	                    //商品描述
                    reqHandler.setParameter("bank_type", tenpayBank);		    //银行类型(中介担保时此参数无效)
                    reqHandler.setParameter("spbill_create_ip", Page.Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP
                    reqHandler.setParameter("fee_type", "1");                    //币种，1人民币
                    reqHandler.setParameter("subject", "789金币购买");              //商品名称(中介交易时必填)


                    //系统可选参数
                    reqHandler.setParameter("sign_type", "MD5");
                    reqHandler.setParameter("service_version", "1.0");
                    reqHandler.setParameter("input_charset", "UTF-8");
                    reqHandler.setParameter("sign_key_index", "1");

                    //业务可选参数

                    reqHandler.setParameter("attach", "");                      //附加数据，原样返回
                    reqHandler.setParameter("product_fee", "0");                 //商品费用，必须保证transport_fee + product_fee=total_fee
                    reqHandler.setParameter("transport_fee", "0");               //物流费用，必须保证transport_fee + product_fee=total_fee
                    reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));            //订单生成时间，格式为yyyymmddhhmmss
                    reqHandler.setParameter("time_expire", "");                 //订单失效时间，格式为yyyymmddhhmmss
                    reqHandler.setParameter("buyer_id", "");                    //买方财付通账号
                    reqHandler.setParameter("goods_tag", "");                   //商品标记
                    reqHandler.setParameter("trade_mode", "1");                 //交易模式，1即时到账(默认)，2中介担保，3后台选择（买家进支付中心列表选择）
                    reqHandler.setParameter("transport_desc", "");              //物流说明
                    reqHandler.setParameter("trans_type", "1");                  //交易类型，1实物交易，2虚拟交易
                    reqHandler.setParameter("agentid", "");                     //平台ID
                    reqHandler.setParameter("agent_type", "");                  //代理模式，0无代理(默认)，1表示卡易售模式，2表示网店模式
                    reqHandler.setParameter("seller_id", "");                   //卖家商户号，为空则等同于partner

                    //获取请求带参数的url
                    string requestUrl = reqHandler.getRequestURL();

                    //Get的实现方式
                    // string a_link = "<a target=\"_blank\" href=\"" + requestUrl + "\">" + "财付通支付" + "</a>";
                    Response.Redirect(requestUrl);


                    //post实现方式

                    /* Response.Write("<form method=\"post\" action=\""+ reqHandler.getGateUrl() + "\" >\n");
                     Hashtable ht = reqHandler.getAllParameters();
                     foreach(DictionaryEntry de in ht) 
                     {
                         Response.Write("<input type=\"hidden\" name=\"" + de.Key + "\" value=\"" + de.Value + "\" >\n");
                     }
                     Response.Write("<input type=\"submit\" value=\"财付通支付\" >\n</form>\n");*/


                    //获取debug信息,建议把请求和debug信息写入日志，方便定位问题
                    string debuginfo = reqHandler.getDebugInfo();
                    Response.Write("<br/>requestUrl:" + requestUrl + "<br/>");
                    Response.Write("<br/>debuginfo:" + debuginfo + "<br/>");
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
            else
            {


            }
        }
    }
}
