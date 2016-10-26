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
using Game.Entity.Treasure;
using Game.Facade;
using Game.Web.Pay.YbBuy;
using Game.Utils;
using Game.Kernel;

namespace Game.Web.Pay
{
    public partial class CallBank : System.Web.UI.Page
    {
       TreasureFacade treasureFacade = new TreasureFacade();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 校验返回数据包
                BuyCallbackResult result = Buy.VerifyCallback(Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("p1_MerId"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("r0_Cmd"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("r1_Code"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("r2_TrxId"),
                    Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("r3_Amt"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("r4_Cur"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("r5_Pid"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("r6_Order"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("r7_Uid"),
                    Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("r8_MP"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("r9_BType"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("rp_PayDate"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("hmac"));

                if (string.IsNullOrEmpty(result.ErrMsg))
                {
                   
                    if (result.R1_Code == "1")
                    {
                        if (result.R9_BType == "1")
                        {
                            // 写充值记录
                            ShareDetialInfo detailInfo = new ShareDetialInfo();
                            detailInfo.OrderID =result.R6_Order;
                            detailInfo.IPAddress = Utility.UserIP;
                            detailInfo.PayAmount = decimal.Parse(result.R3_Amt);
                            Message msg = treasureFacade.FilliedOnline(detailInfo, 0);
                            // 下面的错误提示和成功提示要POST出去。这个判断力。用户访问不到，充值成功与否都得给用户提示。
                            // 卡密的页面也是如此。都要想办法给用户展示，最好这个页面影藏显示去。别给用户看。
                            // 你们测试的时候自己看下。
                            if (!msg.Success)
                            {
                                Response.Redirect("/WiteCard.html?OID=" + result.R6_Order);
                                Response.End();
                            }
                            else
                            {
                                Response.Redirect("/WiteCard.html?OID=" + result.R6_Order);

                            }
                            //  callback方式:浏览器重定向
                            // Response.Write("支付成功!<br>商品ID:" + result.R5_Pid + "<br>商户订单号:" + result.R6_Order + "<br>支付金额:" + result.R3_Amt + "<br>易宝支付交易流水号:" + result.R2_TrxId + "<BR>");

                        }
                        else if (result.R9_BType == "2")
                        {
                            // * 如果是服务器返回则需要回应一个特定字符串'SUCCESS',且在'SUCCESS'之前不可以有任何其他字符输出,保证首先输出的是'SUCCESS'字符串
                            // 这里用户可以看见。想办法不要用户看见。但这句绝对不能取消
                            Response.Write("SUCCESS");
                            ShareDetialInfo detailInfo = new ShareDetialInfo();
                            detailInfo.OrderID = result.R6_Order;
                            detailInfo.IPAddress = Utility.UserIP;
                            detailInfo.PayAmount = decimal.Parse(result.R3_Amt);
                            treasureFacade.FilliedOnline(detailInfo, 0);
                            Response.Redirect("/WiteCard.html?OID=" + result.R6_Order);
                        }
                    }
                    else
                    {
                        Response.Write("支付失败!请重试<a href='/'>点击返回</a>");
                    }
                }
                else
                {
                    Response.Write("交易签名无效!请重试<a href='/pay/payIndex.aspx'>点击返回</a>");
                }
            }
        }
    }
}
