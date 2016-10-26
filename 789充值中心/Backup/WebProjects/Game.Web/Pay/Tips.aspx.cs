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

namespace Game.Web.Pay
{
    public partial class Tips : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = "";
            if (Request["msg"] != null)
            {

                 msg = Request["msg"];

            }
            else
            {
                msg = "";

            }
            this.Label1.Text = "订单提交失败，失败原因：" + msg;
        }
    }
}
