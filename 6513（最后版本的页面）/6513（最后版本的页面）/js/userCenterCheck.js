  function checkInput() {
            if ($("#txtCompellation").val().length > 16) {
                alert("真实姓名长度不能大于16个字符!");
                return false;
            }
            var regMobile = /^((\(\d{2,3}\))|(\d{3}\-))?((13\d{9})|(15[389]\d{8})|(18\d{9}))$/;
            if ($("#txtMobilePhone").val() != "") {
                if (!$("#txtMobilePhone").val().match(regMobile)) {
                    alert("手机号码不正确，请重新输入");
                    return false;
                }
            }
            var regPhone = /(^[0-9]{3,4}-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^([0-9]{3,4})[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)/;
            if ($("#txtSeatPhone").val() != "") {
                if (!$("#txtSeatPhone").val().match(regPhone)) {
                    alert("固定电话号码不正确，请重新输入");
                    return false;
                }
            }
            if ($("#txtQQ").val().length > 16) {
                alert("QQ长度不能大于16个字符!");
                return false;
            }
            var regEmail = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
            if ($("#txtEmail").val() != "") {
                if (!IsEmail($("#txtEmail").val())) {
                    alert("Email地址不正确，请重新输入");
                    return false;
                }
            }
            if ($("#txtAddress").val().length > 128) {
                alert("详细地址长度不能大于128个字符!");
                return false;
            }
            if ($("#txtUserNote").val().length > 256) {
                alert("详细地址长度不能大于256个字符!");
                return false;
            }

            return true;
        }
        $(document).ready(function () {
            $("#btnUpdate").click(function () {
                return checkInput();
            });
            $("#UpdateInfo").addClass("lanmu-list_hover");
        });
    </script>
	<script type="text/javascript">
        function hintMessage(hintObj,error,message){
            //删除样式
            if(error=="error"){
                $("#"+hintObj+"").removeClass("rightTips");
                $("#"+hintObj+"").addClass("wrongTips");
            }else{
                $("#"+hintObj+"").removeClass("wrongTips");
                $("#"+hintObj+"").addClass("rightTips");
            }
    	
            $("#"+hintObj+"").html(message);
        }
    
        function checkPass(){
            if($("#txtOldPass").val()==""){
                hintMessage("txtOldPassTip","error","请输入您的原始密码!");
                return false;
            }
            if($("#txOldtPass").val().length<6||$("#txtPass").val().length>32){
                hintMessage("txtOldPassTip","error","原始密码长度为6到32个字符!");
                return false;
            }
            hintMessage("txtOldPassTip","right","");
            return true;
        }
    
        function checkNewPass(){
            if($("#txtNewPass").val()==""){
                hintMessage("txtNewPassTip","error","请输入您的新密码!");
                return false;
            }
            if($("#txtNewPass").val().length<6||$("#txtNewPass").val().length>32){
                hintMessage("txtNewPassTip","error","新密码长度为6到32个字符!");
                return false;
            }
            hintMessage("txtNewPassTip","right","");
            return true;
        }
    
        function checkConPass(){
            if($("#txtNewPass2").val()==""){
                hintMessage("txtNewPass2Tip","error","请输入您的确认密码!");
                return false;
            }
            if($("#txtNewPass2").val()!=$("#txtNewPass").val()){
                hintMessage("txtNewPass2Tip","error","确认密码与新密码不一致，请重新输入!");
                return false;
            }
            hintMessage("txtNewPass2Tip","right","");
            return true;
        }
    
        function checkInput(){
            if(!checkPass()){ $("#txtOldPass").focus();return false; };
            if(!checkNewPass()){ $("#txtNewPass").focus();return false; };
            if(!checkConPass()){ $("#txtNewPass2").focus();return false; };
        }
    
        $(document).ready(function(){
            $("#txtOldPass").blur(function(){ checkPass()});
            $("#txtNewPass").blur(function(){ checkNewPass()});
            $("#txtNewPass2").blur(function(){ checkConPass()});
        
            $("#btnUpdate").click(function(){ return checkInput(); });
            $("#UpdateLoginPass").addClass("lanmu-list_hover");
        });
        </script>
		    <script type="text/javascript">
        function hintMessage(hintObj, error, message) {
            //删除样式
            if (error == "error") {
                $("#" + hintObj + "").removeClass("rightTips");
                $("#" + hintObj + "").addClass("wrongTips");
            } else {
                $("#" + hintObj + "").removeClass("wrongTips");
                $("#" + hintObj + "").addClass("rightTips");
            }

            $("#" + hintObj + "").html(message);
        }

        function checkQuestion1() {
            if ($("#ddlQuestion1 :selected").val() == "请选择密保问题") {
                hintMessage("lblQuestion1", "error", "请选择密保问题!");
                return false;
            }
            if ($("#ddlQuestion1 :selected").val() == $("#ddlQuestion2 :selected").val() || $("#ddlQuestion1 :selected").val() == $("#ddlQuestion3 :selected").val()) {
                hintMessage("lblQuestion1", "error", "不能选择相同的问题!");
                return false;
            }
            hintMessage("lblQuestion1", "right", "");
            return true;
        }
        function checkAnswer1() {
            if ($("#txtAnswer1").val() == "") {
                hintMessage("lblAnswer1", "error", "请输入您的密保答案!");
                return false;
            }
            if ($("#txtAnswer1").val().length > 32) {
                hintMessage("lblAnswer1", "error", "密保答案长度不能大于32个字符!");
                return false;
            }
            hintMessage("lblAnswer1", "right", "");
            return true;
        }

        function checkQuestion2() {
            if ($("#ddlQuestion2 :selected").val() == "请选择密保问题") {
                hintMessage("lblQuestion2", "error", "请选择密保问题!");
                return false;
            }
            if ($("#ddlQuestion2 :selected").val() == $("#ddlQuestion1 :selected").val() || $("#ddlQuestion2 :selected").val() == $("#ddlQuestion3 :selected").val()) {
                hintMessage("lblQuestion2", "error", "不能选择相同的问题!");
                return false;
            }
            hintMessage("lblQuestion2", "right", "");
            return true;
        }
        function checkAnswer2() {
            if ($("#txtAnswer2").val() == "") {
                hintMessage("lblAnswer2", "error", "请输入您的密保答案!");
                return false;
            }
            if ($("#txtAnswer2").val().length > 32) {
                hintMessage("lblAnswer2", "error", "密保答案长度不能大于32个字符!");
                return false;
            }
            hintMessage("lblAnswer2", "right", "");
            return true;
        }

        function checkQuestion3() {
            if ($("#ddlQuestion3 :selected").val() == "请选择密保问题") {
                hintMessage("lblQuestion3", "error", "请选择密保问题!");
                return false;
            }
            if ($("#ddlQuestion3 :selected").val() == $("#ddlQuestion1 :selected").val() || $("#ddlQuestion3 :selected").val() == $("#ddlQuestion2 :selected").val()) {
                hintMessage("lblQuestion3", "error", "不能选择相同的问题!");
                return false;
            }
            hintMessage("lblQuestion3", "right", "");
            return true;
        }
        function checkAnswer3() {
            if ($("#txtAnswer3").val() == "") {
                hintMessage("lblAnswer3", "error", "请输入您的密保答案!");
                return false;
            }
            if ($("#txtAnswer3").val().length > 32) {
                hintMessage("lblAnswer3", "error", "密保答案长度不能大于32个字符!");
                return false;
            }
            hintMessage("lblAnswer3", "right", "");
            return true;
        }

        function checkPassportType() {
            if ($("#ddlPassportType :selected").val() == "0") {
                hintMessage("lblPassportType", "error", "请选择证件类型!");
                return false;
            }
            hintMessage("lblPassportType", "right", "");
            return true;
        }
        function checkPassportID() {
            if ($("#txtPassportID").val() == "") {
                hintMessage("lblPassportID", "error", "请输入您的证件号码!");
                return false;
            }
            if ($("#txtPassportID").val().length > 32) {
                hintMessage("lblPassportID", "error", "证件号码长度不能大于32个字符!");
                return false;
            }
            hintMessage("lblPassportID", "right", "");
            return true;
        }
        function checkConPassportID() {
            if ($("#txtConPassportID").val() == "") {
                hintMessage("lblConPassportID", "error", "请输入您的确认证件号码!");
                return false;
            }
            if ($("#txtConPassportID").val() != $("#txtPassportID").val()) {
                hintMessage("lblConPassportID", "error", "确认证件号码与原证件号码不一致!");
                return false;
            }
            hintMessage("lblConPassportID", "right", "");
            return true;
        }

        function checkEmail() {
            if ($("#txtEmail").val() == "") {
                hintMessage("lblEmail", "error", "请输入您的电子邮件!");
                return false;
            }
            if ($("#txtEmail").val().length > 32) {
                hintMessage("lblEmail", "error", "电子邮件长度不能大于32个字符!");
                return false;
            }
            if (/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test($("#txtEmail").val()) == false) {
                hintMessage("lblEmail", "error", "电子邮件不正确，请重新输入!");
                return false;
            }
            hintMessage("lblEmail", "right", "");
            return true;
        }
        function checkConEmail() {
            if ($("#txtConEmail").val() == "") {
                hintMessage("lblConEmail", "error", "请输入您的确认电子邮件!");
                return false;
            }
            if ($("#txtConEmail").val() != $("#txtEmail").val()) {
                hintMessage("lblConEmail", "error", "确认电子邮件与原电子邮件不一致!");
                return false;
            }
            hintMessage("lblConEmail", "right", "");
            return true;
        }

        function checkInput() {
            if (!checkQuestion1()) { return false; }
            if (!checkAnswer1()) { $("#txtAnswer1").focus(); return false; }
            if (!checkQuestion2()) { return false; }
            if (!checkAnswer2()) { $("#txtAnswer2").focus(); return false; }
            if (!checkQuestion3()) { return false; }
            if (!checkAnswer3()) { $("#txtAnswer3").focus(); return false; }
            if (!checkPassportType()) { return false; }
            if (!checkPassportID()) { $("#txtPassportID").focus(); return false; }
            if (!checkConPassportID()) { $("#txtConPassportID").focus(); return false; }
            if (!checkEmail()) { $("#txtEmail").focus(); return false; }
            if (!checkConEmail()) { $("#txtConEmail").focus(); return false; }
        }

        $(document).ready(function () {
            $("#txtAnswer1").blur(function () { checkAnswer1(); });
            $("#txtAnswer2").blur(function () { checkAnswer2(); });
            $("#txtAnswer3").blur(function () { checkAnswer3(); });
            $("#txtPassportID").blur(function () { checkPassportID(); });
            $("#txtConPassportID").blur(function () { checkConPassportID(); });
            $("#txtEmail").blur(function () { checkEmail(); });
            $("#txtConEmail").blur(function () { checkConEmail(); });
            $("#ddlQuestion1").change(function () { checkQuestion1(); });
            $("#ddlQuestion2").change(function () { checkQuestion2(); });
            $("#ddlQuestion3").change(function () { checkQuestion3(); });

            $("#btnConfirm").click(function () {
                return checkInput();
            });
            $("#ApplyPassProtect").addClass("lanmu-list_hover");
        });
    </script>
	
	    <script type="text/javascript">
        function checkAnswer() {
            if ($("#txtAnswer11").val() == "" || $("#txtAnswer12").val() == "" || $("#txtAnswer13").val() == "") {
                alert("请输入您的密保答案");
                return false;
            }
            return true;
        }

        function hintMessage(hintObj, error, message) {
            //删除样式
            if (error == "error") {
                $("#" + hintObj + "").removeClass("rightTips");
                $("#" + hintObj + "").addClass("wrongTips");
            } else {
                $("#" + hintObj + "").removeClass("wrongTips");
                $("#" + hintObj + "").addClass("rightTips");
            }

            $("#" + hintObj + "").html(message);
        }

        function checkNewPass() {
            if ($("#txtNewPassWord").val() == "") {
                hintMessage("lblNewsPass", "error", "请输入您的新密码!");
                return false;
            }
            if ($("#txtNewPassWord").val().length > 32 || $("#txtNewPassWord").val().length < 6) {
                hintMessage("lblNewsPass", "error", "新密码长度为6到32个字符!");
                return false;
            }
            hintMessage("lblNewsPass", "right", "");
            return true;
        }

        function checkConPass() {
            if ($("#txtConPassWord").val() == "") {
                hintMessage("lblConPass", "error", "请输入您的确认密码!");
                return false;
            }
            if ($("#txtConPassWord").val() != $("#txtNewPassWord").val()) {
                hintMessage("lblConPass", "error", "确认密码与新密码不一致，请重新输入!");
                return false;
            }
            hintMessage("lblConPass", "right", "");
            return true;
        }

        function checkInput() {
            if (!checkAnswer()) { return false; }
            if (!checkNewPass()) { $("#txtNewPassWord").focus(); return false; }
            if (!checkConPass()) { $("#txtConPassWord").focus(); return false; }
        }

        $(document).ready(function () {
            $("#txtNewPassWord").blur(function () { checkNewPass(); });
            $("#txtConPassWord").blur(function () { checkConPass(); });
            $("#ResetLoginPass").addClass("lanmu-list_hover");
        });
    </script>
	<!--推广收益  js判断-->
	<script type="text/javascript">
        $(document).ready(function () {
            $("#BingUserPhone").addClass("lanmu-list_hover");
            $("#Submit").bind("click", function () {
                if ($("#vertifyCode").val() == "") {
                    alert("请输入你的有效电话号码");
                    return false;
                }
                if (!$("#vertifyCode").val().match(/^1[3|4|5|8][0-9]\d{4,8}$/)) {
                    alert('手机号码格式不正确，请重新输入');
                    return false;

                }
                if ($("#SecureCode").val() == "") {
                    alert("请输入你的验证码");
                    return false;
                }

            });
            $("#GetCode").bind("click", function () {
                if ($("#vertifyCode").val() == "") {
                    $("#Error_cellPhone").html("请输入你的电话号码");
                    return false;
                }

                if (!$("#vertifyCode").val().match(/^1[3|4|5|8][0-9]\d{4,8}$/)) {
                    $("#Error_cellPhone").html('手机格式不正确，请重新输入');
                    return false;
                } else {
                    $("#Error_cellPhone").html('');

                }
            });

        });
    </script>
	<!--绑定邮箱 js  判断-->
	<script type="text/javascript">
        $(document).ready(function () {
            $("#vertifyCodes").bind("blur", function () {
                if ($("#vertifyCodes").val() == "") {
                    $("#Error_cellPhones").html("请输入你的有效邮箱");
                    return false;
                }
              
                if (/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test($("#vertifyCodes").val()) == false) {
                    $("#Error_cellPhones").html('邮箱格式不正确，请重新输入');

                } else {
                    $("#Error_cellPhones").html('');

                }
            });
            $("#SecureCodes").bind("blur", function () {
                if ($("#SecureCodes").val() == "") {
                    $("#Error_secureCodes").html("请输入验证码");
                    return false;
                }
                else {
                    $("#Error_secureCodes").html('');

                }
            });
            $("#Submits").bind("click", function () {
                if ($("#vertifyCodes").val() == "") {
                    $("#Error_cellPhones").html("请输入你的有效邮箱");
                    return false;
                }

                if (/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test($("#vertifyCodes").val()) == false) {
                    $("#Error_cellPhones").html('邮箱格式不正确，请重新输入');
                    return false;

                } else {
                    $("#Error_cellPhones").html('');

                }
                if ($("#SecureCodes").val() == "") {
                    $("#Error_secureCodes").html("请输入验证码");
                    return false;
                }
                else {
                    $("#Error_secureCodes").html('');

                }
            });
            $("#BingUserEmails").addClass("lanmu-list_hover");
        });