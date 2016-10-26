var error1=0;
	var error2=0;
	var error3=0;
	$(document).ready(function(){
		//验证用户名
		$("#txtSsoAccount_Tip").focus(function(){
			$("#txtSsoAccount_Tip").css("display","none");
			$("#txtSsoAccount").css("display","block");
			$("#txtSsoAccount").blur(function(){
				var account=$("#txtSsoAccount").val();
				var pattern = /^[A-Za-z0-9]+$/;
					if(account == ""){
						$("#accountFir").addClass("error");
						$("#showinfo1").addClass("flase");
						$("#txtSsoAccount").css("display","none");
						$("#txtSsoAccount_Tip").css("display","block");
						$("#txtSsoAccount_Tip").val("请输入用户名");
						error1=0;
						return false;
					}
					if(!pattern.test(account)){
						$("#accountFir").addClass("error");
						$("#showinfo1").addClass("flase");
						$("#txtSsoAccount").css("display","none");
						$("#txtSsoAccount_Tip").css("display","block");
						$("#txtSsoAccount_Tip").val("用户名只能输入英文或者数字");
							error1=2;
						return false;
					}
					if(account.length <4 || account.length >12){
						$("#accountFir").addClass("error");
						$("#showinfo1").addClass("flase");
						$("#txtSsoAccount").css("display","none");
						$("#txtSsoAccount_Tip").css("display","block");
						$("#txtSsoAccount_Tip").val("用户名长度为4到12");
							error1=3;
						return false;
					}
					$("#accountFir").removeClass("error");
					$("#showinfo1").removeClass("flase");
					$("#txtSsoAccount").css("display","block");
					$("#txtSsoAccount_Tip").css("display","none");
						error1=1;
					return true;
				});
		});
		//验证密码
		$("#txtSsoPassword_Tip").focus(function(){
			$("#txtSsoPassword_Tip").css("display","none");
			$("#txtSsoPassword").css("display","block");
			$("#txtSsoPassword").blur(function(){
				var account=$("#txtSsoPassword").val();
					if(account == ""){
						$("#accountSec").addClass("error");
						$("#showinfo2").addClass("flase");
						$("#txtSsoPassword").css("display","none");
						$("#txtSsoPassword_Tip").css("display","block");
						$("#txtSsoPassword_Tip").val("请输入密码");
						error2=0;
						return false;
					}
					if(account.length <6 || account.length >18){
						$("#accountSec").addClass("error");
						$("#showinfo2").addClass("flase");
						$("#txtSsoPassword").css("display","none");
						$("#txtSsoPassword_Tip").css("display","block");
						$("#txtSsoPassword_Tip").val("密码长度为6到18");
						error2=2;
						return false;
					}
					$("#accountSec").removeClass("error");
					$("#showinfo2").removeClass("flase");
					$("#txtSsoPassword").css("display","block");
					$("#txtSsoPassword_Tip").css("display","none");
					error2=1;
					return true;
				});
		});
		//验证码的验证
		$("#txtSsoValidCode_Tip").focus(function(){
			$("#txtSsoValidCode_Tip").css("display","none");
			$("#txtSsoValidCode").css("display","block");
			$("#txtSsoValidCode").blur(function(){
				var account=$("#txtSsoValidCode").val();
					if(account == ""){
						$("#accountThi").addClass("error");
						$("#showinfo3").addClass("flase");
						$("#txtSsoValidCode").css("display","none");
						$("#txtSsoValidCode_Tip").css("display","block");
						$("#txtSsoValidCode_Tip").val("请输入验证码");
						error3=0;
						return false;
					}
					$("#accountThi").removeClass("error");
					$("#showinfo3").removeClass("flase");
					$("#txtSsoValidCode").css("display","block");
					$("#txtSsoValidCode_Tip").css("display","none");
					error3=1;
					return true;
				});
		});
		$("#btnLogin").bind("click",function(){
		if(error1 != 1 || error2 != 1 || error3 != 1){
		   if(error1 == 0){
				$("#accountFir").addClass("error");
				$("#showinfo1").addClass("flase");
				$("#txtSsoAccount").css("display","none");
				$("#txtSsoAccount_Tip").css("display","block");
			    $("#txtSsoAccount_Tip").val("请输入用户名");
			}
			if(error1 == 2){
				$("#accountFir").addClass("error");
				$("#showinfo1").addClass("flase");
				$("#txtSsoAccount").css("display","none");
				$("#txtSsoAccount_Tip").css("display","block");
				$("#txtSsoAccount_Tip").val("用户名只能输入英文或者数字");
			}
			if(error1 == 3){
				$("#accountFir").addClass("error");
				$("#showinfo1").addClass("flase");
				$("#txtSsoAccount").css("display","none");
				$("#txtSsoAccount_Tip").css("display","block");
				$("#txtSsoAccount_Tip").val("用户名长度为4到12");
			}
			if(error2 == 0){
				$("#accountSec").addClass("error");
				$("#showinfo2").addClass("flase");
				$("#txtSsoPassword").css("display","none");
				$("#txtSsoPassword_Tip").css("display","block");
				$("#txtSsoPassword_Tip").val("请输入密码");
			}
			if(error2 == 2){
			$("#accountSec").addClass("error");
			$("#showinfo2").addClass("flase");
			$("#txtSsoPassword").css("display","none");
			$("#txtSsoPassword_Tip").css("display","block");
			$("#txtSsoPassword_Tip").val("密码长度为6到18");
			}
			if(error3 == 0){
			$("#accountThi").addClass("error");
			$("#showinfo3").addClass("flase");
			$("#txtSsoValidCode").css("display","none");
			$("#txtSsoValidCode_Tip").css("display","block");
			$("#txtSsoValidCode_Tip").val("请输入密码");
			}
		}else {
			 $.post();
		}
		});
	});
