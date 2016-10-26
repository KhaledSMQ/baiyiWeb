namespace Game.Utils
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class CtrlHelper
    {
        public static byte GetCheckBoxValue(CheckBox chk)
        {
            return Convert.ToByte(chk.Checked);
        }

        public static int GetInt(Control ctrl, int defValue)
        {
            if (ctrl == null)
            {
                throw new ArgumentNullException("获取文本内容的控件不能为空！");
            }
            if (ctrl is ITextControl)
            {
                return TypeParse.StrToInt(GetText(ctrl as ITextControl), defValue);
            }
            if (ctrl is HtmlInputControl)
            {
                return TypeParse.StrToInt(GetText(ctrl as HtmlInputControl), defValue);
            }
            if (ctrl is HiddenField)
            {
                return TypeParse.StrToInt(GetText(ctrl as HiddenField), defValue);
            }
            return defValue;
        }

        public static string GetSelectValue(DropDownList ddlList)
        {
            return ddlList.SelectedValue.Trim();
        }

        public static byte GetSelectValue(DropDownList ddlList, byte defValue)
        {
            return (byte) TypeParse.StrToInt(GetSelectValue(ddlList), defValue);
        }

        public static string GetText(HtmlInputControl valueCtrl)
        {
            if (valueCtrl == null)
            {
                throw new ArgumentNullException("获取文本内容的控件不能为空！");
            }
            if (TextUtility.EmptyTrimOrNull(valueCtrl.Value))
            {
                return "";
            }
            return TextFilter.FilterScript(valueCtrl.Value.Trim());
        }

        public static string GetText(ITextControl textCtrl)
        {
            if (textCtrl == null)
            {
                throw new ArgumentNullException("获取文本内容的控件不能为空！");
            }
            if (TextUtility.EmptyTrimOrNull(textCtrl.Text))
            {
                return "";
            }
            return TextFilter.FilterScript(textCtrl.Text.Trim());
        }

        public static string GetText(HiddenField hiddenCtrl)
        {
            if (hiddenCtrl == null)
            {
                throw new ArgumentNullException("获取文本内容的控件不能为空！");
            }
            if (TextUtility.EmptyTrimOrNull(hiddenCtrl.Value))
            {
                return "";
            }
            return TextFilter.FilterScript(hiddenCtrl.Value.Trim());
        }

        public static void SetCheckBoxValue(CheckBox chk, byte val)
        {
            chk.Checked = val != 0;
        }

        public static void SetText(HtmlInputControl valueCtrl, string text)
        {
            if (valueCtrl == null)
            {
                throw new ArgumentNullException("设置文本内容的控件不能为空！");
            }
            valueCtrl.Value = Utility.HtmlDecode(text.Trim());
        }

        public static void SetText(ITextControl textCtrl, string text)
        {
            if (textCtrl == null)
            {
                throw new ArgumentNullException("设置文本内容的控件不能为空！");
            }
            textCtrl.Text = Utility.HtmlDecode(text.Trim());
        }

        public static void SetText(HiddenField hiddenCtrl, string text)
        {
            if (hiddenCtrl == null)
            {
                throw new ArgumentNullException("设置文本内容的控件不能为空！");
            }
            hiddenCtrl.Value = Utility.HtmlDecode(text.Trim());
        }

        public static void SetText(TextBox textCtrl, string text)
        {
            SetText((ITextControl) textCtrl, text);
        }
    }
}

