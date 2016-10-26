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
using Game.Facade;
using Game.Entity.Treasure;
using Game.Kernel;
using com.yeepay.cmbn;
using Game.Utils;
namespace Game.Web.Pay
{
    public partial class OrderCard : System.Web.UI.Page
    {

        private AccountsFacade accountsFacade = new AccountsFacade();
        private TreasureFacade treasureFacade = new TreasureFacade();
        protected string p1_MerId = ConfigurationManager.AppSettings["merhantId"].ToString();
        protected string p2_Order;
        protected string p3_Amt;
        protected string p4_verifyAmt;
        protected string p4_Cur;
        protected string p5_Pid;
        protected string p6_Pcat;

        protected string p7_Pdesc;
        protected string p8_Url;
        protected string p9_SAF;
        protected string pa_MP;
        protected string pd_FrpId;
        protected string pa7_cardAmt;
        protected string pa8_cardNo;
        protected string pa9_cardPwd;


        protected string pr_NeedResponse;
        protected string hmac;
        protected string pz_userId;
        protected string pz1_userRegTime;

        protected string reqURL_onLine = ConfigurationManager.AppSettings["authorizationURL"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            // 设置 Response编码格式为GB2312
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            if (!string.IsNullOrEmpty(Request.Form["Cuserspay"]) && !string.IsNullOrEmpty(Request.Form["inputMoney"]))
            {
                string pay_username = Request.Form["Cuserspay"];
                string PayMoney = Request.Form["inputMoney"];
                string payBank = Request.Form["CpayBank"];
                string rtypeBank = Request.Form["yx_bank"];

                string payTel = string.IsNullOrEmpty(Request.Form["txt_telphone"])==true? "" : Request.Form["txt_telphone"];

                if (rtypeBank != "BANKSEL")
                {
                    payBank = rtypeBank;
                }
              
                p3_Amt = Convert.ToString(decimal.Parse(PayMoney));
                p4_verifyAmt = "false";
                p5_Pid = "金币";
                p6_Pcat = "1";
                p7_Pdesc = "1";
                p8_Url = ConfigurationManager.AppSettings["CallBackC"].ToString();
                pa_MP = pay_username;
                pa7_cardAmt = Request.Form["inputMoney"];
                pa8_cardNo = Request.Form["pa8_cardNo"];
                pa9_cardPwd = Request["pa9_cardPwd"];
                pd_FrpId = payBank;
                pr_NeedResponse = "1";
                pz_userId = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                pz1_userRegTime = DateTime.Now.ToString();

                p2_Order = "CYC" + DateTime.Now.ToString("yyyyMMddhhmmss");
                //非银行卡专业版正式使用
                try
                {
                    int payType = 0;
                    switch (payBank)
                    {
                        case "JUNNET":
                            payType = 2;
                            break;
                        case "SNDACARD":
                            payType = 3;
                            break;
                        case "SZX":
                            payType = 4;
                            break;
                        case "ZHENGTU":
                            payType = 5;
                            break;
                        case "QQCARD":
                            payType = 6;
                            break;
                        case "UNICOM":
                            payType = 7;
                            break;
                        case "JIUYOU":
                            payType = 8;
                            break;
                        case "NETEASE":
                            payType = 9;
                            break;
                        case "WANMEI":
                            payType = 10;
                            break;
                        case "SOHU":
                            payType = 11;
                            break;
                        case "TELECOM":
                            payType = 12;
                            break;
                        case "TIANXIA":
                            payType = 13;
                            break;
                        case "TIANHONG":
                            payType = 14;
                            break;
                        case "YPCARD":
                            payType = 16;
                            break;
                        case "ZONGYOU":
                            payType = 17;
                            break;

                    }
                    
                    DataSet ds = new DataSet();
                    int uid = accountsFacade.GetAccountsId(pay_username);
                    OnLineOrder onlineOrder = new OnLineOrder();
                    onlineOrder.Accounts = pay_username;
                    onlineOrder.UserID = uid;
                    onlineOrder.OrderAmount = decimal.Parse(PayMoney);
                    onlineOrder.OrderID = p2_Order;
                    onlineOrder.OrderStatus = 0;
                    onlineOrder.ShareID = payType;
                    onlineOrder.CardTotal = 1;
                    onlineOrder.CardTypeID = payType;//卡类充值
                    onlineOrder.TelPhone = payTel;
                    onlineOrder.IPAddress = GameRequest.GetUserIP();
                    Message msg = treasureFacade.RequestOrder(onlineOrder);


                    if (!msg.Success)
                    {
                        Response.Redirect("/Tips.aspx?msg=" + msg.Content);
                    }
                    else
                    {
                        //非银行卡专业版正式使用
                        SZXResult result = SZX.AnnulCard(onlineOrder.OrderID, p3_Amt, p4_verifyAmt, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url,
                        pa_MP, pa7_cardAmt, pa8_cardNo, pa9_cardPwd, pd_FrpId, pr_NeedResponse, pz_userId, pz1_userRegTime);
                        if (result.R1_Code == "1")
                        {
                            
                        Response.Redirect("/WiteNetCard.html?OID=" + onlineOrder.OrderID);

                        }
                        else
                        {
                           Response.Redirect("/WiteNetCard.html?OID=" + onlineOrder.OrderID);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }

            }
        }


    }
}