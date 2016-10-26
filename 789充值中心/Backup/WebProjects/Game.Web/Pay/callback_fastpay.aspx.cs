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
using System.Text;
using System.Collections.Generic;
using payapi_mobile_demo;

namespace Game.Web.Pay
{
    public partial class callback_fastpay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            TreasureFacade treasureFacade = new TreasureFacade();
            string yb_data = Request["data"].ToString();
            string yb_encryptkey = Request["encryptkey"].ToString();

           // Response.Write(yb_data+"<br/>"+yb_encryptkey);
            Boolean ok = EncryptUtil.checkDecryptAndSign(yb_data, yb_encryptkey, Config.yibaoPublickey, Config.merchantPrivatekey);
            if (ok)
            {
                string AESKey = RSA.Class.RSAFromPkcs8.decryptData(yb_encryptkey, Config.merchantPrivatekey, "UTF-8");
                string realData = payapi_mobile_demo.AES.Decrypt(yb_data, AESKey);


                SortedDictionary<string, object> sd = Newtonsoft.Json.JsonConvert.DeserializeObject<SortedDictionary<string, object>>(realData);
                /** 3.取得data明文sign。 */
                string sign = (string)sd["sign"];

                /** 4.对map中的值进行验证 */
                StringBuilder signData = new StringBuilder();
                foreach (var item in sd)
                {
                    /** 把sign参数隔过去 */
                    if (item.Key == "sign")
                    {
                        continue;
                    }
                    signData.Append(item.Value);
                }

                string orderid = (string)sd["orderid"];
                Response.Redirect("/WiteCard.html?OID=" + orderid);
            }
            else
            {

                Response.Write("sorry");
               
            }

        }
    }
}