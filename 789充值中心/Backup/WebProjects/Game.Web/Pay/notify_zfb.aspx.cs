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
using Com.Alipay;
using System.Collections.Generic;
using Game.Entity.Treasure;
using Game.Kernel;
using Game.Facade;
using Game.Utils;
using System.Collections.Specialized;

namespace Game.Web.Pay
{
    public partial class notify_zfb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TreasureFacade treasureFacade = new TreasureFacade();

            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request["notify_id"], Request["sign"]);

                if (verifyResult)//验证成功
                {
                 
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //商户订单号

                    string out_trade_no = Request["out_trade_no"];

                    //支付宝交易号

                    string trade_no = Request["trade_no"];

                    //交易状态
                    string trade_status = Request["trade_status"];
                    string total_fee = Request["total_fee"];

                    if (Request["trade_status"] == "TRADE_FINISHED")
                    {

                        ShareDetialInfo detailInfo = new ShareDetialInfo();
                        detailInfo.OrderID = out_trade_no;
                        detailInfo.IPAddress = Utility.UserIP;
                        detailInfo.PayAmount = decimal.Parse(total_fee);
                        Message msg = treasureFacade.FilliedOnline(detailInfo, 0);
                        if (!msg.Success)
                        {
                            Response.Write(msg.Content);

                        }
                        else
                        {

                            Response.Write("支付成功");
                        }

                    }
                    else if (Request["trade_status"] == "TRADE_SUCCESS")
                    {
                        ShareDetialInfo detailInfo = new ShareDetialInfo();
                        detailInfo.OrderID = out_trade_no;
                        detailInfo.IPAddress = Utility.UserIP;
                        detailInfo.PayAmount = decimal.Parse(total_fee);
                        Message msg = treasureFacade.FilliedOnline(detailInfo, 0);
                        if (!msg.Success)
                        {
                            Response.Write(msg.Content);

                        }
                        else
                        {

                            Response.Write("支付成功");


                        }

                    }
                    else
                    {
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    Response.Write("success");//请不要修改或删除

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }
    }
}