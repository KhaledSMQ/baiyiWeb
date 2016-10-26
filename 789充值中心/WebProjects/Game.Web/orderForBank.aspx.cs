using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.yeepay.icc;
using System.Configuration;
using Game.Entity.Treasure;
using Game.Utils;
using Game.Kernel;
using Game.Facade;
using System.Data;
using Game.Entity.Accounts;
namespace Game.Web
{
    public partial class orderForBank : System.Web.UI.Page
    {
        /// <summary>
        /// 易宝银行卡充值需要的参数
        /// </summary>
        protected string p1_MerId = ConfigurationManager.AppSettings["yeepayBankAndCardMerId"];

        protected string p2_Order;
        protected string p3_Amt;
        protected string p4_Cur;
        protected string p5_Pid;
        protected string p6_Pcat;

        protected string p7_Pdesc;
        protected string p8_Url;
        protected string p9_SAF;
        protected string pa_MP;
        protected string pd_FrpId;

        protected string pr_NeedResponse;
        protected string hmac;

        protected string reqURL_onLine = ConfigurationManager.AppSettings["yeepayPayUrl"];

        // 商户编号
        private static string merchantId = ConfigurationManager.AppSettings["yeepayBankAndCardMerId"];

        // 商户密钥
        private static string keyValue = ConfigurationManager.AppSettings["yeepayKeyValue"];
        /// <summary>
        /// 我自己需要的参数
        /// </summary>
        protected string payBankInfo;
        protected string payBankUser;
        protected string payBankMoney;
        protected string payBankName;
        protected string payOtherMoney;

        /// <summary>
        /// 我自己需要的方法
        /// </summary>
        TreasureFacade treasurefacade = new TreasureFacade();
        AccountsFacade accountsfacade = new AccountsFacade();
        protected void Page_Load(object sender, EventArgs e)
        {
            //充值开始
            Request.Cookies.Remove("ErrorMsg");
            // 设置 Response编码格式为GB2312
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");

            payBankUser = Request.Form["yeepayBankUser"]??string.Empty;
            payBankMoney = Request.Form["yeepayBankMoney"] ?? string.Empty;
            payBankName = Request.Form["yeepayBankName"] ?? string.Empty;
            payOtherMoney = Request.Form["selfMoney"] ?? string.Empty;
            if (payBankMoney.Equals("0"))
            {
                payBankMoney = "100";
            }
            if (!string.IsNullOrEmpty(payOtherMoney))
            {
                payBankMoney = payOtherMoney;
            }
            if (!string.IsNullOrEmpty(payBankUser) && !string.IsNullOrEmpty(payBankMoney) && !string.IsNullOrEmpty(payBankName))
            {
                AccountsInfo accountsInfo = accountsfacade.GetAccountsByAccontsName(payBankUser);
                if (accountsInfo == null)
                {
                    HttpCookie UserCookie2 = new HttpCookie("ErrorMsg");
                    UserCookie2["error"] =HttpUtility.UrlDecode("用户名不存在");
                    UserCookie2.Expires = DateTime.Now.AddMinutes(7);
                    Response.Cookies.Add(UserCookie2);
                    Response.Redirect("/showpayInfo.html");
                }

                try
                {
                    int.Parse(payBankMoney);
                }
                catch (Exception)
                {
                    return;
                }

                switch (payBankName)
                {
                    case "ICBC-NET-B2C":   //工商银行
                        payBankInfo = "GSYH";
                        break;

                    case "CMBCHINA-NET-B2C":  //招商银行
                        payBankInfo = "ZSYH";
                        break;

                    case "ABC-NET-B2C":   //中国农业银行
                        payBankInfo = "NYYH";
                        break;

                    case "CCB-NET-B2C":  //建设银行
                        payBankInfo = "JSYH";
                        break;

                    case "BCCB-NET-B2C":  //北京银行
                        payBankInfo = "BJYH";
                        break;

                    case "BOCO-NET-B2C"://交通银行
                        payBankInfo = "JTYH";
                        break;

                    case "CIB-NET-B2C": //兴业银行
                        payBankInfo = "XYYH";
                        break;

                    case "NJCB-NET-B2C":   //南京银行
                        payBankInfo = "NJYH";
                        break;

                    case "CMBC-NET-B2C":  //中国民生银行
                        payBankInfo = "MSYH";
                        break;

                    case "CEB-NET-B2C":   //光大银行
                        payBankInfo = "GDYH";
                        break;

                    case "BOC-NET-B2C":  //中国银行
                        payBankInfo = "ZGYH";
                        break;

                    case "PINGANBANK-NET":  //平安银行
                        payBankInfo = "PAYH";
                        break;

                    case "CBHB-NET-B2C":    //渤海银行
                        payBankInfo = "BHYH";
                        break;

                    case "ECITIC-NET-B2C":   //中兴银行
                        payBankInfo="ZXYH";
                        break;

                    case "SDB-NET-B2C":   //深圳发展银行
                        payBankInfo = "SZFZYH";
                        break;

                    case "GDB-NET-B2C":   //广发银行
                        payBankInfo = "GFYH";
                        break;

                    case "SPDB-NET-B2C":   //上海浦东发展银行
                        payBankInfo = "SHPDYH";
                        break;

                    case "POST-NET-B2C":   //中国邮政
                        payBankInfo = "ZGYZYH";
                        break;

                    case "HXB-NET-B2C":   //华夏银行
                        payBankInfo = "HXYH";
                        break;

                    case "CZ-NET-B2C":   //浙商银行
                        payBankInfo = "ZSYH";
                        break;

                    default:
                        payBankInfo = "WY";  
                        break;
                }

                p2_Order = payBankInfo + "-" + DateTime.Now.ToString("yyyyMMddhhmmss");  //用户订单


                HttpCookie UserCookie = new HttpCookie("PayOrder");
                UserCookie["order"] = p2_Order;
                UserCookie["orderMoney"] = payBankMoney;
                UserCookie["orderUser"] = payBankUser;
                UserCookie.Expires = DateTime.Now.AddMinutes(7);
                Response.Cookies.Add(UserCookie);

                OnLineOrder onlineOrder = new OnLineOrder();
                onlineOrder.Accounts = payBankUser;
                onlineOrder.UserID = accountsInfo.UserID;
                onlineOrder.OrderAmount = decimal.Parse(payBankMoney);
                onlineOrder.OrderID = p2_Order;
                onlineOrder.OrderStatus = 0;
                onlineOrder.ShareID = 20;
                onlineOrder.CardTotal = 1;
                onlineOrder.CardTypeID = 1;//易宝充值
                onlineOrder.TelPhone = "";
                onlineOrder.IPAddress = GameRequest.GetUserIP();
                Message msg = treasurefacade.RequestOrder(onlineOrder);
                if (msg.Success)
                {
                    //p3_Amt交易金额  精确两位小数，最小值为0.01,为持卡人实际要支付的金额.                 
                    p3_Amt =payBankMoney;
                    //交易币种,固定值"CNY".
                    p4_Cur = "CNY";
                    //商品名称
                    p5_Pid = ConfigurationManager.AppSettings["shoppingInfo"];// Request.Form["yeepayBankp5_Pid"];
                    //商品种类
                    p6_Pcat ="1";
                    //商品描述
                    p7_Pdesc = "1";
                    //商户接收支付成功数据的地址,支付成功后易宝支付会向该地址发送两次成功通知.
                    p8_Url = ConfigurationManager.AppSettings["yeepayBankAndCardCallback"].ToString();
                    //送货地址
                    //为“1”: 需要用户将送货地址留在易宝支付系统;为“0”: 不需要，默认为 ”0”.
                    p9_SAF = "0";
                    //商户扩展信息
                    //商户可以任意填写1K 的字符串,支付成功时将原样返回.	
                    pa_MP ="sdfdf";
                    //银行编码
                    //默认为""，到易宝支付网关.若不需显示易宝支付的页面，直接跳转到各银行、神州行支付、骏网一卡通等支付页面，该字段可依照附录:银行列表设置参数值.
                    pd_FrpId = payBankName;
                    //默认为"1": 需要应答机制;
                    pr_NeedResponse = "1";

                    string s = Buy.GetMerId();

                    hmac = Buy.CreateBuyHmac(p2_Order, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, p9_SAF, pa_MP, pd_FrpId, pr_NeedResponse);
                }
                else
                {
                    HttpCookie UserCookie2 = new HttpCookie("ErrorMsg");
                    UserCookie2["error"] =HttpUtility.UrlDecode(msg.Content);
                    UserCookie2.Expires = DateTime.Now.AddMinutes(7);
                    Response.Cookies.Add(UserCookie2);
                    Response.Redirect("/showpayInfo.html");
                }

            }
        }
    }
}