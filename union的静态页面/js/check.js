       //用户账号验证
    function  checkUserAccount(){
	    var user_name = $("#user_name").val();
		//alert(user_name);
        var pattern = /^[A-Za-z0-9]+$/;
	       if (user_name.length < 3 || user_name.length > 20){
				$("#user_name_span").html("用户名的长度为3到20个字符！");
                $("#user_name_span").css("color", "red");
				$("#user_name").focus();
				return false;
            }else{
				if (!pattern.test(user_name)){
				   $("#user_name_span").html("用户名为字母或者数字！");
					$("#user_name_span").css("color", "red");
					$("#user_name").focus();
					return false;
				}else {
				    $("#user_name_span").html("该用户名可以注册！");
					$("#user_name_span").css("color", "#454545");
					return true;
				}
			}
		}
        //密码校验
		function  checkUserPwd(){
			var patrn = /^[a-zA-Z]{6,20}$/;
            var user_pwd = $("#user_pwd").val();
            if (user_pwd.length < 6 || user_pwd.length > 20){
				$("#user_pwd_span").html("用户名的长度为3到20个字符！")
                $("#user_pwd_span").css("color", "red");
				$("#user_pwd").focus();
				return false;
            }else{
			 if(!patrn.test(user_pwd)){
				$("#user_pwd_span").html("用户名为字母或者数字！");
                $("#user_pwd_span").css("color", "red");
				$("#user_pwd").focus();
				return false;
				}else{
				$("#user_pwd_span").html("该密码可以使用！");
                $("#user_pwd_span").css("color", "#454545");
				return true;
                 }
			}
		}
        //确认密码的校验
		function  checkRepPwd(){
			var user_pwd = $("#user_pwd").val();
            var repwd = $("#user_pwd2").val();
			if (repwd == ""){
			   $("#user_pwd2_span").html("请输入确认密码");
                $("#user_pwd2_span").css("color", "red");
				$("#user_pwd2").focus();
				return false;
            }else{
				if (repwd != user_pwd){
				 $("#user_pwd2_span").html("两次输入密码不一样");
                $("#user_pwd2_span").css("color", "red");
				return false;
				}else{
				 $("#user_pwd2_span").html("确认密码输入正确！");
                $("#user_pwd2_span").css("color", "#454545");
				return true;
				}
			}
		}
        //电话号码
		function checkPhone(){
			var user_tel = $("#user_tel").val();
            var reg = RegExp(/^1[3|4|5|8][0-9]{9}$/);
            if (user_tel.length == 0) {
				$("#user_tel_span").html("手机号码不能为空");
                $("#user_tel_span").css("color", "red");
				$("#user_tel").focus();
				return false;
            }else{
				if (!reg.test(user_tel)) {
				$("#user_tel_span").html("手机号码格式输入错误");
                $("#user_tel_span").css("color", "red");
				$("#user_tel").focus();
				return false;
				}else {
				$("#user_tel_span").html("此电话号码可以使用！");
                $("#user_tel_span").css("color", "#454545");
				return true;
				}
		    }
        }
        //电子邮箱
		function  checkEmail(){
			var user_email = $("#user_email").val();
            var pattern =/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
            if (user_email == "") {
			$("#user_email_span").html("请输入您的电子邮箱！");
                $("#user_email_span").css("color", "red");
				$("#user_email").focus();
                return false;
            }else{
				if (!pattern.test(user_email)) {
				$("#user_email_span").html("您的电子邮箱格式输入错误！");
                $("#user_email_span").css("color", "red");
				$("#user_email").focus();
                return false;
				}else{
				$("#user_email_span").html("审核通过后，我们会给您发送邮件！");
                $("#user_email_span").css("color", "#454545");
                return true;
				}
			}
		}
		
		
		
        //选择网站类型
		function checkWebsiteType(){
			if (website_type.value == "0") {
			   $("#website_type_span").html("请输入您的网站类型！");
                $("#website_type_span").css("color", "red");
				$("#website_type").focus();
                return false;
            }else{
				$("#website_type_span").css("color", "#454545");
				return true;
			}
		}
		
        //判断网站名称
		function checkWebsiteName(){
			var  reg=  /^(\w|[\u4E00-\u9FA5])*$/;
            var websiteName = $("#website_name").val();
			if (websiteName == "") {
				$("#website_name_span").html("请输入您的网站名称！");
                $("#website_name_span").css("color", "red");
				$("#website_name").focus();
				return false;
            }else{
				if (!reg.test(websiteName)) {
					$("#website_name_span").html("您的网站名称输入格式错误！");
					$("#website_name_span").css("color", "red");
					$("#website_name").focus();
					return false;
				}else{
					$("#website_name_span").css("color", "#454545");
					return true;
				}
			}
		}
        //判断网站网址
		function checkWebsiteDomain(){
			var websiteDomain=$("#website_domain").val();
           // var webDoman = /^([\.a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+/;
			  var webDoman = /^[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+\.?$/;
            if(websiteDomain == ""){
				$("#website_domain_span").html("请输入您的网站网址！");
                $("#website_domain_span").css("color", "red");
				$("#website_domain").focus();
                return false;
            }else{
				if (!webDoman.test(websiteDomain)) {
					$("#website_domain_span").html("您的网站网址格式错误！");
					$("#website_domain_span").css("color", "red");
					$("#website_domain").focus();
					return false;
				}else{
					$("#website_domain_span").css("color", "#454545");
					return true;
				}
			}	
		}
		//判断协议
		
		function checkService(){
		 if ($("#user_register_agree_rule").attr("checked")) {
			$("#user_register_agree_rule_span").css("color","#454545");
				return true;
				
            }else {
			 $("#user_register_agree_rule_span").html("必须同意服务条款才能注册！");
			 $("#user_register_agree_rule_span").css("color","red");
			 return false;
            }
		}
		
		function checkService2(){
		 if ($("#user_register_agree_rule2").attr("checked")) {
			$("#user_register_agree_rule_span2").css("color","#454545");
				return true;
            }else {
			 $("#user_register_agree_rule_span2").html("必须同意服务条款才能注册！");
			  $("#user_register_agree_rule_span2").css("color","red");
			 return false;
            }
		}
		
function checkInput(){
	if (!checkUserAccount()){
        $("#user_name").focus();
		return false;
	}else if(!checkUserPwd()){
	   $("#user_pwd").focus();
	   return false;
	}else if(!checkRepPwd()){
		$("#user_pwd2").focus();
		return false; 
	}else if(!checkPhone()) {
		$("#user_tel").focus(); 
		return false; 
	}else if(!checkEmail()) { 
		$("#user_email").focus(); 
		return false;
	}else if(!checkService()){
		return false;
	}else{
		return true;
	}
	
	}
	
	function checkInputWeb(){
	if (!checkUserAccount()){
        $("#user_name").focus();
		return false;
	}else if(!checkUserPwd()){
	   $("#user_pwd").focus();
	   return false;
	}else if(!checkRepPwd()){
		$("#user_pwd2").focus();
		return false; 
	}else if(!checkWebsiteType()){
		$("#website_type").focus(); 
		return false;
	}else if(!checkWebsiteName()){
		$("#website_name").focus(); 
		return false;
	}else if(!checkWebsiteDomain()){
		$("#website_domain").focus();
		return false;
	}else if(!checkPhone()) {
		$("#user_tel").focus(); 
		return false; 
	}else if(!checkEmail()) { 
		$("#user_email").focus(); 
		return false;
	}else if(!checkService2()){
		return false;
	}else{
		return true;
	}
	
	}
	