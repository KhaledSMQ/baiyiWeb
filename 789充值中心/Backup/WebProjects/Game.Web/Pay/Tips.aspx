<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tips.aspx.cs" Inherits="Game.Web.Pay.Tips" %>
<%@ Register TagPrefix="game" TagName="Header" Src="~/Themes/New/Common_Header.ascx" %>
<%@ Register TagPrefix="game" TagName="User_Left" Src="~/Themes/New/Common_Left.ascx" %>
<%@ Register TagPrefix="game" TagName="Footer" Src="~/Themes/New/Common_Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>网银充值-789游戏中心</title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/templets/images/home.css" rel="stylesheet" type="text/css">
 <script src="/templets/images/jquery.js" type="text/javascript"></script>
    <script src="/templets/images/jquery_002.js" type="text/javascript"></script>
        <script src="/templets/images/jquery-1.js" type="text/javascript"></script>
    <script src="/templets/images/all.js" type="text/javascript"></script>

    <script src="/templets/images/validate.js" type="text/javascript"></script>

    <script type="text/javascript" src="/templets/images/promoterindex.js"></script>

    <script src="/templets/images/MSClass.js" type="text/javascript"></script>



    <style type="text/css">
 .cz_fhangshi {
	 MARGIN-TOP: 25px; WIDTH: 100%; MARGIN-BOTTOM: 2px; FLOAT: left; HEIGHT: auto
}
.cz_fhangshi UL {
	MARGIN: 2px; DISPLAY: inline; FLOAT: left
}
.cz_fhangshi UL LI {
	BORDER-BOTTOM: #d5d5d5 2px solid; WIDTH: 652px; DISPLAY: inline; BACKGROUND: url(rechangeimgs/cz_listbg.gif) repeat-x; FLOAT: left; HEIGHT: 122px; COLOR: #252525
}
.cz_fhangshi UL LI SPAN {
	DISPLAY: block; FLOAT: left
}
.fspic {
	TEXT-ALIGN: center; WIDTH: 140px; PADDING-RIGHT: 2px; BACKGROUND: url(rechangeimgs/cz_line.gif) no-repeat right 50%; HEIGHT: 122px
}
.fsfont {
 WIDTH: 350px; MARGIN-LEFT: 15px
}
.fsfont H3 {
	MARGIN-BOTTOM: 5px; FONT-SIZE: 14px; FONT-WEIGHT: bold
}
.fsbttn {
	MARGIN-TOP: 45px; WIDTH: 100px; HEIGHT: 36px; MARGIN-LEFT: 30px
}
img
{
margin:0px;
padding:0px;
text-align:center;	
}
.line
{
line-height:40px;	
}
.cz_tabtitle {
	BORDER-BOTTOM: #d1c0a8 1px dashed; POSITION: relative; PADDING-BOTTOM: 20px; MARGIN: 20px 15px; FONT-FAMILY: "微软雅黑", "黑体"
}

.tabtitle_1 {
	COLOR: #2e2e2e; FONT-SIZE: 20px
}
.tabtitle_2 {
	COLOR: #ff4e00; FONT-SIZE: 24px
}
.tabtitle_3 {
	POSITION: absolute; FONT-FAMILY: "宋体"; RIGHT: 0px; _right: 20px
}
 .btnL
        {
            background: url(/templets/images/rechangeImgs/cz_ljcz_h.gif);
            list-style-type: none;
            text-decoration: none;
            width: 147px;
            height: 37px;
            float: left;
            border: 0;
            margin-right: 15px;
            font-size: 14px;
            font-weight: bold;
        }
    </style>
  
</head>
<body>
    <div class="cot">
        <div id="wrap">
            <!--左侧栏目 start-->
            <div id="side_left">
                <game:User_Left ID="user_left" runat="server" />
           </div>
            <!--左侧栏目end-->
            <!--右侧栏目 start-->
            <div id="side_right">
                <!--导航栏  start-->
                <game:Header ID="header" runat="server" />
                <!--  大广告轮播图 end  -->
                <div class="pran">
                    <div class="xwbt">
                        主页>>充值中心</div>
                    <div class="wfjs" style="overflow: hidden;">
                    
                       <div style="color: #2e1b03; font-size: 14px;">
                            <b>充值流程：</b></div>
                        <div class="czstep">
                            <span>
                                <img src="/templets/images/rechangeImgs/czlch_01.jpg" alt="选择充值方式"></span> <span
                                    style="margin-left: 15px;">
                                    <img src="/templets/images/rechangeImgs/czlch_02.jpg" alt="填写充值信息"></span>
                            <span style="margin-left: 15px;">
                                <img src="/templets/images/rechangeImgs/czlch_03.jpg" alt="充值付款"></span> <span style="margin-left: 15px;
                                    background: none; padding: 0;">
                                    <img src="/templets/images/rechangeImgs/czlch_04.jpg" alt="充值成功"></span>
                        </div>
                        <div>
                            <font class="cf00">温馨提示：</font>充值成功后，系统将在10分钟内将幸运豆存入您的帐户，请您登陆游戏大厅或个人中心查看！</div>
                      <div class="cz_fhangshi" style="font-size:15px; color:Red;">
                      
                      
                          <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
         
                      
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--右侧栏目 end-->
        <!--footer-->
        <game:Footer ID="Footer" runat="server" />
        <!--footer  end-->
    </div>
</body>
</html>

<script src="/templets/images/jquery-1.js"></script>

<script src="/templets/images/main.js"></script>

