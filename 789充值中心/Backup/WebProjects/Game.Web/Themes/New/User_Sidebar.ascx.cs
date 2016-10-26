using Game.Entity.Accounts;
using Game.Facade;
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

namespace Game.Web.Themes.New
{
    public partial class User_Sidebar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void linkExit_Click(object sender, EventArgs e)
        {
            UserTicketInfo userTicket = Fetch.GetUserCookie();
            if (userTicket != null)
            {
                Fetch.DeleteSiteCookies();
                Response.Redirect("/login.aspx");
            }
        }
    }
}