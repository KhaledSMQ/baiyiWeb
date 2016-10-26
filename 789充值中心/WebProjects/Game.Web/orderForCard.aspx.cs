using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.yeepay.icc;
using com.yeepay.utils;
using System.Configuration;
using Game.Entity.Treasure;
using Game.Utils;
using Game.Kernel;
using Game.Facade;
using System.Data;
using Game.Entity.Accounts;
namespace Game.Web
{
    public partial class orderForCard : System.Web.UI.Page
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

        /// <summary>
        /// 我自己需要的参数
        /// </summary>
        protected string payCardInfo;
        protected string payCardUser;
        protected string payCardMoney;
        protected string payCardName;

        /// <summary>
        /// 我自己需要的方法
        /// </summary>
        TreasureFacade treasurefacade = new TreasureFacade();
        AccountsFacade accountsfacade = new AccountsFacade();
        protected void Page_Load(object sender, EventArgs e)
        {
            Request.Cookies.Remove("ErrorMsg");
            //充值开始
            // 设置 Response编码格式为GB2312
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");

            payCardUser = Request.Form["yeepayCardUser"] ?? string.Empty;
            payCardMoney = Request.Form["yeepayCardMoney"] ?? string.Empty;
            payCardName = Request.Form["yeepayCardName"] ?? string.Empty;
            if (string.IsNullOrEmpty(payCardMoney))
            {
                payCardMoney = "10";
            }
            if (!string.IsNullOrEmpty(payCardUser) && !string.IsNullOrEmpty(payCardMoney) && !string.IsNullOrEmpty(payCardName))
            {
                AccountsInfo accountsInfo = accountsfacade.GetAccountsByAccontsName(payCardUser);
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
                    int.Parse(payCardMoney);
                }
                catch (Exception)
                {
                    return;
                }

                switch (payCardName)
                {
                    case "JUNNET-NET":   //骏网一卡通
                        payCardInfo = "2";
                        break;

                    case "SNDACARD-NET":  //盛大卡
                        payCardInfo = "3";
                        break;

                    case "SZX-NET":   //神州行
                        payCardInfo = "4";
                        break;

                    case "ZHENGTU-NET":  //征途卡
                        payCardInfo = "5";
                        break;

                    case "QQCARD-NET":  //Q币卡
                        payCardInfo = "6";
                        break;

                    case "UNICOM-NET"://联通卡
                        payCardInfo = "7";
                        break;

                    case "JIUYOU-NET": //久游卡
                        payCardInfo = "8";
                        break;

                    case "YPCARD-NET":   //易宝e卡通
                        payCardInfo = "9";
                        break;

                    case "NETEASE-NET":  // 网易卡 
                        payCardInfo = "10";
                        break;

                    case "WANMEI-NET":   //完美卡
                        payCardInfo = "11";
                        break;

                    case "SOHU-NET":  //搜狐卡
                        payCardInfo = "12";
                        break;

                    case "TELECOM-NET":  //电信卡
                        payCardInfo = "13";
                        break;

                    case "ZONGYOU-NET":    //纵游一卡通
                        payCardInfo = "14";
                        break;

                    case "TIANXIA-NET":   //天下一卡通
                        payCardInfo = "15";
                        break;

                    case "TIANHONG-NET":   //天宏一卡通
                        payCardInfo = "16";
                        break;

                    case "BESTPAY-NET":   //翼支付
                        payCardInfo = "17";
                        break;

                    default:
                        payCardInfo = "18";
                        break;
                }

                p2_Order ="DKCZ-" + DateTime.Now.ToString("yyyyMMddhhmmss");  //用户订单

                HttpCookie UserCookie = new HttpCookie("PayOrder");
                UserCookie["order"] = p2_Order;
                UserCookie["orderMoney"] = payCardMoney;
                UserCookie["orderUser"] = HttpUtility.UrlEncode(payCardUser);
                UserCookie.Expires = DateTime.Now.AddMinutes(7);
                Response.Cookies.Add(UserCookie);

                OnLineOrder onlineOrder = new OnLineOrder();
                onlineOrder.Accounts = payCardUser;
                onlineOrder.UserID = accountsInfo.UserID;
                onlineOrder.OrderAmount = decimal.Parse(payCardMoney);
                onlineOrder.OrderID = p2_Order;
                onlineOrder.OrderStatus = 0;
                onlineOrder.ShareID = Convert.ToInt32(payCardInfo);
                onlineOrder.CardTotal = 1;
                onlineOrder.CardTypeID = Convert.ToInt32(payCardInfo);//游戏点卡充值
                onlineOrder.TelPhone = "";
                onlineOrder.IPAddress = GameRequest.GetUserIP();
                Message msg = treasurefacade.RequestOrder(onlineOrder);
                if (msg.Success)
                {
                    //p3_Amt交易金额  精确两位小数，最小值为0.01,为持卡人实际要支付的金额.                 
                    p3_Amt = payCardMoney;
                    //交易币种,固定值"CNY".
                    p4_Cur = "CNY";
                    //商品名称
                    p5_Pid = ConfigurationManager.AppSettings["shoppingInfo"];// Request.Form["yeepayBankp5_Pid"];
                    //商品种类
                    p6_Pcat = "1";
                    //商品描述
                    p7_Pdesc = "1";
                    //商户接收支付成功数据的地址,支付成功后易宝支付会向该地址发送两次成功通知.
                    p8_Url = ConfigurationManager.AppSettings["yeepayBankAndCardCallback"].ToString();
                    //送货地址
                    //为“1”: 需要用户将送货地址留在易宝支付系统;为“0”: 不需要，默认为 ”0”.
                    p9_SAF = "0";
                    //商户扩展信息
                    //商户可以任意填写1K 的字符串,支付成功时将原样返回.	
                    pa_MP = "";
                    //银行编码
                    //默认为""，到易宝支付网关.若不需显示易宝支付的页面，直接跳转到各银行、神州行支付、骏网一卡通等支付页面，该字段可依照附录:银行列表设置参数值.
                    pd_FrpId = payCardName;
                    //默认为"1": 需要应答机制;
                    pr_NeedResponse = "1";
                    hmac = Buy.CreateBuyHmac(p2_Order, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, p9_SAF, pa_MP, pd_FrpId, pr_NeedResponse);
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