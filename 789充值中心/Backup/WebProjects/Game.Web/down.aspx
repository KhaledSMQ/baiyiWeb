<%@ Page Language="C#" AutoEventWireup="true" %>

<script type="text/C#" runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        string outOrder_no = Request["outOrder_no"].ToString();
        string sign = Request["sign"].ToString();
        WriteFile("outOrder_no=" + outOrder_no + "<br/>sign=" + sign, Server.MapPath("~/"), "a.txt");

    }
    public static bool WriteFile(string str, string path, string htmlfile)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            sw = new System.IO.StreamWriter(path + htmlfile, false, System.Text.Encoding.UTF8);
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
</script>