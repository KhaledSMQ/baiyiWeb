using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using payapi_mobile_demo;
using System.Text;
using System.Collections.Generic;
using Game.Entity.Treasure;
using Game.Utils;
using Game.Kernel;
namespace Game.Web
{
    public partial class callbackForFastpay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            TreasureFacade treasureFacade = new TreasureFacade();
            string yb_data = Request["data"].ToString();
            string yb_encryptkey = Request["encryptkey"].ToString();

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

                Request.Cookies.Remove("ErrorMsg");
                Response.Redirect("/showPayInfo.html");
            }
            else
            {
                HttpCookie UserCookie2 = new HttpCookie("ErrorMsg");
                UserCookie2["error"] = HttpUtility.UrlEncode("订单失败");
                UserCookie2.Expires = DateTime.Now.AddMinutes(7);
                Response.Cookies.Add(UserCookie2);
                Response.Redirect("/showPayInfo.html");
            }

        }
    }
}
