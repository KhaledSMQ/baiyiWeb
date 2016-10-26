using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Alipay;
using System.Collections.Generic;
using Game.Entity.Treasure;
using Game.Kernel;
using Game.Facade;
using Game.Utils;
using System.Collections.Specialized;
namespace Game.Web
{
    public partial class notifyForAlipy : System.Web.UI.Page
    {
        TreasureFacade treasurefacade = new TreasureFacade();

        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request["notify_id"], Request["sign"]);

                if (verifyResult)//验证成功
                {
                    //商户订单号
                    string out_trade_no = Request["out_trade_no"];

                    //支付宝交易号
                    string trade_no = Request["trade_no"];

                    //交易状态
                    string trade_status = Request["trade_status"];

                    //交易金额
                    string total_fee = Request["total_fee"];

                    if (Request["trade_status"] == "TRADE_FINISHED")
                    {
                        ShareDetialInfo detailInfo = new ShareDetialInfo();
                        detailInfo.OrderID = out_trade_no;
                        detailInfo.IPAddress = Utility.UserIP;
                        detailInfo.PayAmount = decimal.Parse(total_fee);
                        Message msg = treasurefacade.FilliedOnline(detailInfo, 0);
                        if (!msg.Success)
                        {
                            Response.Write(msg.Content);
                        }
                    }
                    else if (Request["trade_status"] == "TRADE_SUCCESS")
                    {
                        ShareDetialInfo detailInfo = new ShareDetialInfo();
                        detailInfo.OrderID = out_trade_no;
                        detailInfo.IPAddress = Utility.UserIP;
                        detailInfo.PayAmount = decimal.Parse(total_fee);
                        Message msg = treasurefacade.FilliedOnline(detailInfo, 0);
                        if (!msg.Success)
                        {
                            Response.Write(msg.Content);
                        }
                    }
                    else
                    {

                    }
                    Response.Write("success");//请不要修改或删除
                }
                else//验证失败
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            coll = Request.Form;
            String[] requestItem = coll.AllKeys;
            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }
            return sArray;
        }
    }
}