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
using payapi_mobile_demo;
using System.Text;
using System.Collections.Generic;
using Game.Entity.Treasure;
using Game.Utils;
using Game.Kernel;
using Game.Utils.Utils;
using System.IO;

namespace Game.Web
{
    public partial class notify_weixin : System.Web.UI.Page
    {
        public string pwd = ConfigurationManager.AppSettings["weixin_pwd"];
        weixinDES weixindes = new weixinDES();
        protected void Page_Load(object sender, EventArgs e)
        {   
            TreasureFacade treasureFacade = new TreasureFacade();
            string outOrder_no = Request["outOrder_no"].ToString();
            string sign = Request["sign"].ToString();
            string realsign = weixindes.Decrypt(sign, pwd);
            if (("?outOrder_no=" + outOrder_no).Equals(realsign))
            {
                ShareDetialInfo detailInfo = new ShareDetialInfo();
                detailInfo.OrderID = outOrder_no;
                detailInfo.IPAddress = Utility.UserIP;
                OnLineOrder onlineorder = treasureFacade.GetOnlineOrder(outOrder_no);
                if (onlineorder != null)
                {
                    detailInfo.PayAmount = onlineorder.CardPrice;
                    Message msg = treasureFacade.FilliedOnline(detailInfo, 0);
                    if (!msg.Success)
                    {
                        Response.Write("Fail");

                    }
                    else
                    {

                        Response.Write("Success");
                    }
                }
                else
                {
                    Response.Write("Fail");

                }
            }
            else
            {

                Response.Write("Fail");
            }
        }
        public static bool WriteFile(string str, string path, string htmlfile)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(path + htmlfile, false, System.Text.Encoding.UTF8);
                sw.Write(str);
                sw.Flush();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
                HttpContext.Current.Response.End();
            }
            finally
            {
                sw.Close();
            }
            return true;
        }
    }
}