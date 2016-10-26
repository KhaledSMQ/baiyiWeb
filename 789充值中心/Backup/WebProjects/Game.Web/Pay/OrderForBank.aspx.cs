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

using Game.Utils;
using Game.Facade;
using Game.Kernel;
using Game.Entity.Treasure;
using System.Text;
namespace Game.Web.Pay
{
    public partial class OrderForBank : System.Web.UI.Page
    {

        private AccountsFacade accountsFacade = new AccountsFacade();
        private TreasureFacade treasureFacade = new TreasureFacade();
        protected string p1_MerId = ConfigurationManager.AppSettings["merhantId"].ToString();

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

        protected string reqURL_onLine = ConfigurationManager.AppSettings["authorizationURLB"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            // 设置 Response编码格式为GB2312

            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            if (!string.IsNullOrEmpty(Request["userspay"]) && !string.IsNullOrEmpty(Request["p3_Amt"]))
            {
                string pay_username = Request["userspay"];
                string PayMoney = Request["p3_Amt"].Equals("0.125")==true?Request["otherbank"]:Request["p3_Amt"];
                string rtypeBank = Request["bank"];
                string payBank = Request["bank"];
                string payTel = string.IsNullOrEmpty(Request["txt_telphone"]) == true ? "" : Request["txt_telphone"];
                string onlyBank = "WY";
                if (rtypeBank != "BANKSEL")
                {
                    payBank = rtypeBank;  //其他
                }
                if (PayMoney == "0")
                {
                    PayMoney = string.IsNullOrEmpty(Request["p3_Amt2"]) == true ? "10" : Request["p3_Amt2"];
                }

                switch (payBank)
                {
                    case "ICBC-NET":
                        onlyBank = "GS";
                        break;
                    case "ABC-NET":
                        onlyBank = "NH";
                        break;
                    case "CMBCHINA-NET":
                        onlyBank = "ZS";
                        break;
                    case "BOCO-NET":
                        onlyBank = "JT";
                        break;
                    case "GDB-NET":
                        onlyBank = "GF";
                        break;
                    case "GNXS-NET":
                        onlyBank = "GZ";
                        break;
                    case "HXB-NET":
                        onlyBank = "HX";
                        break;
                    case "CCB-NET":
                        onlyBank = "JH";
                        break;
                    case "CEB-NET":
                        onlyBank = "GD";
                        break;
                    case "CMBC-NET":
                        onlyBank = "MS";
                        break;
                    case "NJCB-NET":
                        onlyBank = "NJ";
                        break;
                    case "CBHB-NET":
                        onlyBank = "BH";
                        break;
                    case "SPDB-NET":
                        onlyBank = "PF";
                        break;
                    case "SDB-NET":
                        onlyBank = "SZ";
                        break;
                    case "CIB-NET":
                        onlyBank = "XY";
                        break;
                    case "BCCB-NET":
                        onlyBank = "BJ";
                        break;
                    case "BOC-NET":
                        onlyBank = "ZG";
                        break;
                    case "ECITIC-NET":
                        onlyBank = "ZX";
                        break;
                    default:
                        onlyBank = "WY";
                        break;

                }
                p3_Amt = PayMoney;//价格"0.01"  PayMoney
                p4_Cur = "CNY";
                p5_Pid = "金币";
                p6_Pcat = "1";
                p7_Pdesc = "1";
                p8_Url = ConfigurationManager.AppSettings["CallBack"].ToString();
                p9_SAF = "0";
                pa_MP = pay_username;
                pd_FrpId = payBank;
                pr_NeedResponse = "1";
                p2_Order = onlyBank+"-YCQP" + DateTime.Now.ToString("yyyyMMddhhmmss");
                DataSet ds = new DataSet();
                int uid = accountsFacade.GetAccountsId(pay_username);
                OnLineOrder onlineOrder = new OnLineOrder();
                onlineOrder.Accounts = pay_username;
                onlineOrder.UserID = uid;
                onlineOrder.OrderAmount = decimal.Parse(PayMoney);
                onlineOrder.OrderID = p2_Order;
                onlineOrder.OrderStatus = 0;
                onlineOrder.ShareID = 19;
                onlineOrder.CardTotal = 1;
                onlineOrder.CardTypeID = 1;//易宝充值
                onlineOrder.TelPhone = payTel;
                onlineOrder.IPAddress = GameRequest.GetUserIP();
                //onlineOrder.Otype = payBank;

                Message msg = treasureFacade.RequestOrder(onlineOrder);
                if (!msg.Success)
                {
                    Response.Redirect("/Tips.aspx?msg="+msg.Content);
                   // reqURL_onLine = "Tips.aspx";
                  //  Label1.Text = msg.Content;
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('" + msg.Content + "');location.href='PayIndex.aspx';</script>");

                }
                else
                {
                    hmac = Web.Pay.YbBuy.Buy.CreateBuyHmac(onlineOrder.OrderID, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, p9_SAF, pa_MP, pd_FrpId, pr_NeedResponse);

                }
            }
        }
    }
}

