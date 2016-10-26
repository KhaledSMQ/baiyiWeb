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
using com.yeepay.cmbn;
using Game.Entity.Treasure;
using Game.Utils;
using Game.Kernel;

namespace Game.Web.Pay
{
    public partial class CallCard : System.Web.UI.Page
    {
        TreasureFacade treasureFacade = new TreasureFacade();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SZX.logURL(Request.RawUrl);
                // 校验返回数据包

                SZXCallbackResult result = SZX.VerifyCallback(Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("r0_Cmd"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("r1_Code"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("p1_MerId"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("p2_Order"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("p3_Amt"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("p4_FrpId"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("p5_CardNo"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("p6_confirmAmount"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("p7_realAmount"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("p8_cardStatus"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("p9_MP"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("pb_BalanceAmt"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("pc_BalanceAct"), Game.Web.Pay.YbBuy.FormatQueryString.GetQueryString("hmac"));

                if (string.IsNullOrEmpty(result.ErrMsg))
                {
                    // 使用应答机制时 必须回写success
                    Response.Write("SUCCESS");
                    //在接收到支付结果通知后，判断是否进行过业务逻辑处理，不要重复进行业务逻辑处理
                    Logic(result);



                }
                else
                {
                    HmacError(result);
                }
            }
        }
        protected void Logic(SZXCallbackResult result)
        {
            if (result.R1_Code == "1")
            {
                ShareDetialInfo detailInfo = new ShareDetialInfo();
                TreasureFacade treasureFacade = new TreasureFacade();

                detailInfo.OrderID = result.P2_Order;
                detailInfo.IPAddress = Utility.UserIP;
                detailInfo.PayAmount = decimal.Parse(result.P7_realAmount);
                Message  msg= treasureFacade.FilliedOnline(detailInfo, 0);
                Response.Write(detailInfo.PayAmount);
                Response.Write(msg.Content);
                Response.Write("支付成功");
            }
            else
            {
                Response.Write("支付失败");
            }
        }
        protected void HmacError(SZXCallbackResult result)
        {
              Response.Write("交易签名无效!，交易失败，请重新充值。");
        }
    }
}
