<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="User_Sidebar.ascx.cs" Inherits="Game.Web.Themes.New.User_Sidebar" %>
<div class="UserSidebar_left">
    <div class="UserSidebar_left_1">
        <ul>
            <li>&nbsp;【本游戏适合十八岁以上的玩家】</li>
            <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;发布时间：2014年3月</li>
            <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;软件版本1.0.0.0</li>
            <li><a href="http://down.789game.net/Download/789GameCenter.exe">
                <img src="/templets/New/images/download.png" alt="图片加载不出来" /></a></li>
        </ul>
    </div>
    <div class="lanmu-list">
        <ul>
            <li><a href="/User/UserInfoCenter.aspx" id="UserInfoCenter">基本资料</a></li>
            <li><a href="/User/UpdateInfo.aspx" id="UpdateInfo">修改资料</a></li>
            <li><a href="/User/UpdateLoginPass.aspx"  id="UpdateLoginPass">修改密码</a></li>
            <li><a href="/User/ApplyPassProtect.aspx"  id="ApplyPassProtect">密码保护</a></li>
            <li><a href="/User/ResetInsurePass.aspx"  id="ResetLoginPass">找回密码</a></li>
            <li><a href="/User/RecordInsureList.aspx" id="RecordInsureList">交易记录</a></li>
            <li><a href="/User/Spread.aspx" id="Spread">推广中心</a></li>
            <li><a href="/User/SpreadInfo.aspx" id="SpreadInfo">推广收益</a></li>
            <li><a href="/User/BingUserPhone.aspx" id="BingUserPhone">绑定手机</a></li>
            <li><a href="/User/BingUserEmail.aspx" id="BingUserEmail">绑定邮箱</a></li>
            <li>
                <asp:LinkButton ID="linkExit" runat="server" OnClick="linkExit_Click" OnClientClick="return confim();">安全退出</asp:LinkButton>
            </li>
        </ul>
    </div>

</div>
<div class="clear"></div>
<script type="text/javascript">
    function confim() {
        if (window.confirm('你确定要退出吗？')) {
            //alert("确定");
            return true;
        } else {
            //alert("取消");
            return false;
        }
    }

</script>


