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
using Game.Entity.Platform;

namespace Game.Web.Themes.New
{
    public partial class Common_Footer : System.Web.UI.UserControl
    {
        public string content = "";
        private PlatformFacade platformFacade = new PlatformFacade();

        protected void Page_Load(object sender, EventArgs e)
        {
//             if (!IsPostBack)
//             {
//                 PublicConfig config = platformFacade.GetPublicConfig("Website");
//                 content = config.Field2;
//             }
        }
    }
}