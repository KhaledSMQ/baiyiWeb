using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.yeepay.icc;
using com.yeepay.utils;
using Game.Entity.Treasure;
using Game.Utils;
using Game.Facade;
namespace Game.Web
{
    public partial class callbackBankAndCard : System.Web.UI.Page
    {
        TreasureFacade treasureFacade = new TreasureFacade();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 校验返回数据包
                Buy.logstr(FormatQueryString.GetQueryString("r6_Order"), Request.Url.Query, "");
                BuyCallbackResult result = Buy.VerifyCallback(FormatQueryString.GetQueryString("p1_MerId"), FormatQueryString.GetQueryString("r0_Cmd"), FormatQueryString.GetQueryString("r1_Code"), FormatQueryString.GetQueryString("r2_TrxId"),
                    FormatQueryString.GetQueryString("r3_Amt"), FormatQueryString.GetQueryString("r4_Cur"), FormatQueryString.GetQueryString("r5_Pid"), FormatQueryString.GetQueryString("r6_Order"), FormatQueryString.GetQueryString("r7_Uid"),
                    FormatQueryString.GetQueryString("r8_MP"), FormatQueryString.GetQueryString("r9_BType"), FormatQueryString.GetQueryString("rp_PayDate"), FormatQueryString.GetQueryString("hmac"));

                if (string.IsNullOrEmpty(result.ErrMsg))
                {
                    //在接收到支付结果通知后，判断是否进行过业务逻辑处理，不要重复进行业务逻辑处理
                    if (result.R1_Code == "1")
                    {
                        if (result.R9_BType == "1")
                        {
                            Request.Cookies.Remove("ErrorMsg");

                            Response.Redirect("/showPayInfo.html");
                            //  callback方式:浏览器重定向
                          //  Response.Write("支付成功!" +
                           //     "<br>接口类型:" + result.R0_Cmd +
                          //      "<br>返回码:" + result.R1_Code +
                                //"<br>商户号:" + result.P1_MerId +
                          //      "<br>交易流水号:" + result.R2_TrxId +
                           //     "<br>商户订单号:" + result.R6_Order +

                         //       "<br>交易金额:" + result.R3_Amt +
                          //      "<br>交易币种:" + result.R4_Cur +
                         //       "<br>订单完成时间:" + result.Rp_PayDate +
                          //      "<br>回调方式:" + result.R9_BType +
                          //      "<br>错误信息:" + result.ErrMsg + "<BR>");
                        }
                        else if (result.R9_BType == "2")
                        {
                            // * 如果是服务器返回则需要回应一个特定字符串'SUCCESS',且在'SUCCESS'之前不可以有任何其他字符输出,保证首先输出的是'SUCCESS'字符串
                            Response.Write("SUCCESS");
                            ShareDetialInfo detailInfo = new ShareDetialInfo();
                            detailInfo.OrderID = result.R6_Order;
                            detailInfo.IPAddress = Utility.UserIP;
                            detailInfo.PayAmount = decimal.Parse(result.R3_Amt);
                            treasureFacade.FilliedOnline(detailInfo, 0);
                        }
                    }
                    else
                    {
                        HttpCookie UserCookie2 = new HttpCookie("ErrorMsg");
                        UserCookie2["error"] = HttpUtility.UrlEncode(result.ErrMsg);
                        UserCookie2.Expires = DateTime.Now.AddMinutes(7);
                        Response.Cookies.Add(UserCookie2);
                        Response.Redirect("/showPayInfo.html");
                    }
                }
                else
                {
                    HttpCookie UserCookie2 = new HttpCookie("ErrorMsg");
                    UserCookie2["error"] =HttpUtility.UrlEncode( "交易签名无效");
                    UserCookie2.Expires = DateTime.Now.AddMinutes(7);
                    Response.Cookies.Add(UserCookie2);
                    Response.Redirect("/showPayInfo.html");
                }
            }
        }
    }
}