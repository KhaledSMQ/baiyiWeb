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

using Game.Web.Tools;
using System.Drawing;
using Game.Facade;

namespace Game.Web
{
    /// <summary>
    /// 验证码
    /// </summary>
    public partial class ValidateImage : Page, System.Web.SessionState.IRequiresSessionState 
    {
        /// <summary>
        /// 初始化页面
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string verifyCode = Game.Utils.TextUtility.CreateAuthStr(4, true);
            Game.Utils.SessionState.Set(Fetch.UC_VERIFY_CODE_KEY, verifyCode);
            Utils.Utility.WriteCookie(Fetch.UC_VERIFY_CODE_KEY, Utils.CWHEncryptNet.XorEncrypt(verifyCode));
            VerifyImageInfo verifyimg = new VerifyImage().GenerateImage(verifyCode, 42, 20, Color.FromArgb(227, 227, 227));
            Bitmap image = verifyimg.Image;
            Response.ContentType = verifyimg.ContentType;
            image.Save(Response.OutputStream, verifyimg.ImageFormat);
        }
    }
}
