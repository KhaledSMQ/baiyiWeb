    var error1=0;
	var error2=0;
	var error3=0;
	$(document).ready(function(){
		//验证用户名
			$("#txtSsoAccount").blur(function(){
				var account=$("#txtSsoAccount").val();
				var pattern = /^[A-Za-z0-9]+$/;
					if(account == ""){
						$("#showerrorMsg").html("请输入用户名");
						$("#accountName").css("border","1px solid #ffa7a0");
						error1=0;
						return false;
					}
					if(!pattern.test(account)){
						$("#showerrorMsg").html("用户名只能输入英文或者数字");
						$("#accountName").css("border","1px solid #ffa7a0");
						error1=2;
						return false;
					}
					if(account.length <4 || account.length >12){
						$("#showerrorMsg").val("用户名长度为4到12");
						$("#accountName").css("border","1px solid #ffa7a0");
							error1=3;
						return false;
					}
				    error1=1;
					$("#accountName").css("border","1px solid #e5e5e5");
					return true;
				});
		//验证密码
		$("#txtSsoPassword").blur(function(){
				var account=$("#txtSsoPassword").val();
					if(account == ""){
						$("#showerrorMsg").html("请输入密码");
						$("#accountSec").css("border","1px solid #ffa7a0");
						error2=0;
						return false;
					}
					if(account.length <6 || account.length >18){
						$("#showerrorMsg").html("密码长度为6到18");
						$("#accountSec").css("border","1px solid #ffa7a0");
						error2=2;
						return false;
					}
					error2=1;
					$("#accountSec").css("border","1px solid #e5e5e5");
					return true;
				});
		//验证码的验证
			$("#txtSsoValidCode").blur(function(){
				var account=$("#txtSsoValidCode").val();
					if(account == ""){
						$("#showerrorMsg").html("请输入验证码");
						$("#accountThi").css("border","1px solid #ffa7a0");
						error3=0;
						return false;
					}
					error3=1;
					$("#accountThi").css("border","1px solid #e5e5e5");
					$("#showerrorMsg").html("");
					return true;
				});
				
		$("#btnLogin").bind("click",function(){
		if(error1 != 1 || error2 != 1 || error3 != 1){
		   if(error1 == 0){
			    $("#showerrorMsg").html("请输入用户名");
			}
			if(error1 == 2){
				$("#showerrorMsg").html("用户名只能输入英文或者数字");
			}
			if(error1 == 3){
				$("#showerrorMsg").html("用户名长度为4到12");
			}
			if(error2 == 0){
				$("#showerrorMsg").html("请输入密码");
			}
			if(error2 == 2){
			$("#showerrorMsg").html("密码长度为6到18");
			}
			if(error3 == 0){
			$("#showerrorMsg").html("请输入密码");
			}
		}else {
			 $.post();
		}
		});
	});
