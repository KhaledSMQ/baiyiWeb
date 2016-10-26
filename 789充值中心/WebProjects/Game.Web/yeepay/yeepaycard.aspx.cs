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
using Game.Web.Pay.YbBuy;
using com.yeepay.cmbn;
namespace Game.Web.yeepay
{
    public partial class yeepaycard : System.Web.UI.Page
    {
        #region  易宝支付所需的参数

        protected string p1_MerId;

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

        protected string origin_p1_MerId;
        protected string origin_p2_Order;
        protected string origin_p8_Url;
        protected string origin_hmac;
        #endregion


        #region 格式化回调的数据
        protected string getReturnValue(string r1_code, string r6_code, string rq_returnmsg, string hmac)
        {

            return string.Format("r0_Cmd=ChargeCardDirect\nr1_Code={0}\nr6_Order={1}\nrq_ReturnMsg={2}\nhmac={3}", r1_code, r6_code, rq_returnmsg, hmac);

        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            if (!string.IsNullOrEmpty(FormatQueryString.GetQueryString("p1_MerId")) && !string.IsNullOrEmpty(FormatQueryString.GetQueryString("pz1_userRegTime")))
            {
                #region  这是我的获取第三方接入过来的提交参数

                origin_p1_MerId = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("p1_MerId"), System.Text.Encoding.GetEncoding("gb2312"));
                origin_p2_Order = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("p2_Order"), System.Text.Encoding.GetEncoding("gb2312"));

                p3_Amt = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("p3_Amt"), System.Text.Encoding.GetEncoding("gb2312"));
                p4_verifyAmt = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("p4_verifyAmt"), System.Text.Encoding.GetEncoding("gb2312"));
                p5_Pid = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("p5_Pid"), System.Text.Encoding.GetEncoding("gb2312"));
                p6_Pcat = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("p6_Pcat"), System.Text.Encoding.GetEncoding("gb2312"));
                p7_Pdesc = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("p7_Pdesc"), System.Text.Encoding.GetEncoding("gb2312"));

                origin_p8_Url = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("p8_Url"), System.Text.Encoding.GetEncoding("gb2312"));

                pa_MP = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("pa_MP"), System.Text.Encoding.GetEncoding("gb2312"));
                pa7_cardAmt = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("pa7_cardAmt"), System.Text.Encoding.GetEncoding("gb2312"));
                pa8_cardNo = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("pa8_cardNo"), System.Text.Encoding.GetEncoding("gb2312"));
                pa9_cardPwd = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("pa9_cardPwd"), System.Text.Encoding.GetEncoding("gb2312"));
                pd_FrpId = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("pd_FrpId"), System.Text.Encoding.GetEncoding("gb2312"));
                pr_NeedResponse = "1";
                pz_userId = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("pz_userId"), System.Text.Encoding.GetEncoding("gb2312"));
                pz1_userRegTime = System.Web.HttpUtility.UrlDecode(FormatQueryString.GetQueryString("pz1_userRegTime"), System.Text.Encoding.GetEncoding("gb2312"));

                origin_hmac = FormatQueryString.GetQueryString("hmac");
                #endregion

                #region  这是我修改提交的参数
                p1_MerId = ConfigurationManager.AppSettings["p1_MerId"].ToString();
                p8_Url = ConfigurationManager.AppSettings["p8_Url"].ToString();
                p2_Order = "789" + origin_p2_Order;
                #endregion


                #region  业务逻辑的处理




                #endregion
                SZXResult result = SZX.AnnulCard(p2_Order, p3_Amt, p4_verifyAmt, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, pa_MP, pa7_cardAmt, pa8_cardNo, pa9_cardPwd, pd_FrpId, pr_NeedResponse, pz_userId, pz1_userRegTime);
                #region 提交的返回值处理
                //得到的返回的数值
                string call_hmac = result.Hmac;
                string call_r0_cmd = result.R0_Cmd;
                string call_r1_code = result.R1_Code;
                string call_r6_order = result.R6_Order;
                string call_rq_returnmsg = result.Rq_ReturnMsg;
                string call_req_result = result.ReqResult;
                string real_call_req_result = "";
                if (result.R1_Code.Equals("1"))
                {
                    real_call_req_result = getReturnValue(call_r1_code, origin_p2_Order, call_rq_returnmsg, origin_hmac);
                }
                else
                {
                    real_call_req_result = getReturnValue(call_r1_code, origin_p2_Order, call_rq_returnmsg, call_hmac);

                }
                Response.Write(real_call_req_result);

                #endregion


                //p1_MerId = FormatQueryString.GetQueryString("p1_MerId");
                //p2_Order = FormatQueryString.GetQueryString("p2_Order");
                //p3_Amt = FormatQueryString.GetQueryString("p3_Amt");
                //p4_verifyAmt = FormatQueryString.GetQueryString("p4_verifyAmt");
                //p5_Pid = FormatQueryString.GetQueryString("p5_Pid");
                //p6_Pcat = FormatQueryString.GetQueryString("p6_Pcat");
                //p7_Pdesc = FormatQueryString.GetQueryString("p7_Pdesc");
                //p8_Url = FormatQueryString.GetQueryString("p8_Url");
                //pa_MP = FormatQueryString.GetQueryString("pa_MP");
                //pa7_cardAmt = FormatQueryString.GetQueryString("pa7_cardAmt");
                //pa8_cardNo = FormatQueryString.GetQueryString("pa8_cardNo");
                //pa9_cardPwd = FormatQueryString.GetQueryString("pa9_cardPwd");
                //pd_FrpId = FormatQueryString.GetQueryString("pd_FrpId");
                //pr_NeedResponse="1";
                //pz_userId = FormatQueryString.GetQueryString("pz_userId");
                //pz1_userRegTime = FormatQueryString.GetQueryString("pz1_userRegTime");
            }
        }
    }
}
