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
using System.Collections.Generic;
using Com.Alipay;
using Game.Entity.Treasure;
using Game.Kernel;
using Game.Facade;
using Game.Utils;
namespace Game.Web.Pay
{
    public partial class ZFB : System.Web.UI.Page
    {
        private AccountsFacade accountsFacade = new AccountsFacade();
        private TreasureFacade treasureFacade = new TreasureFacade();
        protected void Page_Load(object sender, EventArgs e)
        {
            ////////////////////////////////////////////请求参数////////////////////////////////////////////
            //支付类型
            string payment_type = "1";
            //必填，不能修改
            //服务器异步通知页面路径
            string notify_url = ConfigurationManager.AppSettings["ailpayNotify"];
            //需http://格式的完整路径，不能加?id=123这类自定义参数

            //页面跳转同步通知页面路径
            string return_url = ConfigurationManager.AppSettings["ailpayCallback"];
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            //卖家支付宝帐户
            string seller_email = ConfigurationManager.AppSettings["ailpayAccount"];
            //必填

            //商户订单号
            string out_trade_no = "zfb" + DateTime.Now.ToString("yyyyMMddhhmmss");
            //商户网站订单系统中唯一订单号，必填

            //订单名称
            string subject = "幸运豆";
            //必填

            //付款金额
            string aa = Request["p3_Amt1"];
            string total_fee = Convert.ToString(decimal.Parse(Request["p3_Amt1"].Equals("0.125")==true ? Request["otherzfb"] : Request["p3_Amt1"]));
            //必填

            //订单描述

            string body = "幸运豆";
            //商品展示地址
            string show_url = "";
            //需以http://开头的完整路径，例如：http://www.xxx.com/myorder.html

            //防钓鱼时间戳
            string anti_phishing_key = "";
            //若要使用请调用类文件submit中的query_timestamp函数

            //客户端的IP地址
            string exter_invoke_ip = "";
            //非局域网的外网IP地址，如：221.0.0.1


            ////////////////////////////////////////////////////////////////////////////////////////////////

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

            //建立请求
          
            int uid = accountsFacade.GetAccountsId(Request.Form["zfbuserspay"]);
            OnLineOrder onlineOrder = new OnLineOrder();
            onlineOrder.Accounts = Request["zfbuserspay"];
            onlineOrder.UserID = uid;
            onlineOrder.OrderAmount = decimal.Parse(Request["p3_Amt1"].Equals("0.125") ? Request["otherzfb"] : Request["p3_Amt1"]);
            onlineOrder.OrderID = out_trade_no;
            onlineOrder.OrderStatus = 0;
            onlineOrder.ShareID = 20;
            onlineOrder.CardTotal = 1;
            onlineOrder.CardTypeID = 1;//支付宝充值
            onlineOrder.TelPhone = "";
            onlineOrder.IPAddress = GameRequest.GetUserIP();
            //onlineOrder.Otype = payBank;

            Message msg = treasureFacade.RequestOrder(onlineOrder);
            if (!msg.Success)
            {

                Response.Redirect("/Tips.aspx?msg=" + msg.Content);
                //reqURL_onLine = "PayBank.aspx";
                //  Label1.Text = msg.Content;
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('" + msg.Content + "');location.href='PayIndex.aspx';</script>");

            }
            else
            {
                //建立请求
                string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
                Response.Write(sHtmlText);


            }

        }
    }
}
