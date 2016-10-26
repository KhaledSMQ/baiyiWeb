 var error1=0;
	var error2=0;
	var error3=0;
	var error4=0;
	var error5=0;
	var error6=0;
	var error7=0;
	var error8=0;
	var error9=0;
	var error10=0;
//触发焦点的错误提示
	function error(o,t){
	  var html="t";
	  o.css("color","red");
	  o.append(html);
	}
	//触发焦点的正确提示
	function success(o,t){
	  var html="t"
	  o.css("color","#7a7a7a");
	  o.append(html);
	}
$(document).ready(function(){
	/*var Edit=$("#Edit");
	var editInput=Edit.find("input").not("#Edit");
	editInput.blur(function(){
	    var v=this.value();
		var _this=$(this);
	
	if(this.id == "oldPwd"){
	 if(v=""){
		var t="请输入您的原密码"
		error(_this+"Error"+,t);
	 }
	}
	
	});*/
	
	
	$("#oldPwd").blur(function(){
	  var oldPwd=$("#oldPwd").val();
	  if(oldPwd.length<6){
		$("#oldpwdError").html("请输入您的原密码,密码长度不能小于6！");
		$("#oldpwdError").css("color","red");
		$("#oldPwd").focus();
		error1=0;
		return false;
	  }else{
		error1=1;
		$("#oldpwdError").html("");
		$("#oldpwdError").css("color","#7a7a7a");
		return true;
	  }
	});
	
	$("#newPwd").blur(function(){
	  var newPwd=$("#newPwd").val();
	  if(newPwd.length<6){
		$("#newpwdError").html("请输入您的新密码，密码长度不能小于6");
		$("#newpwdError").css("color","red");
		error2=0;
		return false;
	  }else{
		error2=1;
		$("#newpwdError").html("");
		$("#newpwdError").css("color","#7a7a7a");
		return true;
	  }
	});
	
	$("#repPwd").blur(function(){
	 var repPwd=$("#repPwd").val();
	 var newPwd=$("#newPwd").val();
	 if(repPwd.length < 6){
		$("#repNewPwd").html("请再次输入您的密码");
		$("#repNewPwd").css("color","red");
		error3=0;
		return false;
	 }else{
		if(repPwd != newPwd){
			$("#repNewPwd").html("两次输入的密码不一样");
			$("#repNewPwd").css("color","red");
			error3=3;
			return false;
		}else{
			$("#repNewPwd").html("");
			$("#repNewPwd").css("color","#7a7a7a");
			error3=1;
			return true;
		}
	 }
	});
	
	$("#Edit").click(function(){
		if(error1 !=1 || error2 !=1 || error3 !=1){
		  if(error1 == 0){
			 $("#oldpwdError").html("请输入您的原密码,密码的长度不能小于6位数");
			 $("#oldpwdError").css("color","red");
		  }else if(error2 == 0){
			$("#newpwdError").html("请输入您的新密码,密码的长度不能小于6位数");
			$("#newpwdError").css("color","red");
		  }else if(error3 ==0){
			$("#repNewPwd").html("请再次输入您的密码");
			$("#repNewPwd").css("color","red");
		  }else if(error3 == 3){
			$("#repNewPwd").html("两次输入的密码不一样");
			$("#repNewPwd").css("color","red");
		  }
		}else{
			if($("#newPwd").val() == $("#repPwd").val()){
				$.post();
			}else{
				  $("#repNewPwd").html("两次输入的密码不一样");
				  $("#repNewPwd").css("color","red");
			}
		}
	 });
	 
	 //银行信息部分的绑定
	 //验证开户行网点
	$("#bankAddress").blur(function(){
	 var bankAddress=$("#bankAddress").val();
		if(bankAddress == ""){
		 $("#bankAddressError").css("color","red");
	     $("#bankAddressError").html("请输入您的开户行网点");
		  error4=0;
	    }else{
		  $("#bankAddressError").html("");
		  $("#bankAddressError").css("color","#7a7a7a");
		  error4=1;
		}
	 });
	 //验证联系人
	 $("#contentPerson").blur(function(){
	 var contentPerson=$("#contentPerson").val();
	 var reg1 = /^[\u4E00-\u9AF5]+$/;
	 var pattern1 = /^[A-Za-z]+$/;
	 if(contentPerson == ""){
		$("#contentPersonError").css("color","red");
		$("#contentPersonError").html("请输入您的姓名");
		error5=0;
	 }else{
		if(!(reg1.test(contentPerson) || pattern1.test(contentPerson))){
		$("#contentPersonError").html("请认真填写您的姓名");
		$("#contentPersonError").css("color","red");
		error5=2;
	    }else{
		$("#contentPersonError").html("（请输入您的姓名）");
		$("#contentPersonError").css("color","#7a7a7a");
		error5=1;
		}
	 }
	 
	 });
	 //验证QQ号码
	  $("#personQQ").blur(function(){
	 var personQQ=$("#personQQ").val();
	 var reg1 = RegExp(/^[1-9][0-9]{4,15}$/);//qq
	 if(personQQ.length <5){
		$("#personQQError").html("QQ号码的长度不能小于5");
		$("#personQQError").css("color","red");
		error6=0;
	 }else{
		if(reg1.test(personQQ)){
			$("#personQQError").html("");
			$("#personQQError").css("color","#7a7a7a");
			error6=1;
		}else{
			$("#personQQError").html("QQ号码的格式输入错误");
			$("#personQQError").css("color","red");
			error6=2;
		}
	 }
	 });
	//验证手机号码
	$("#TelNo").blur(function(){
		var TelNo=$("#TelNo").val();
		var reg = RegExp(/^1[3|4|5|8][0-9]{9}$/);//phone
		if(TelNo == ""){
			$("#TelNoError").html("请输入您的手机号码");
			$("#TelNoError").css("color","red");
			error7=0;
		}else{
			if(reg.test(TelNo)){
				$("#TelNoError").html("（请输入您的手机号码）");
				$("#TelNoError").css("color","#7a7a7a");
				error7=1;
			}else{
				$("#TelNoError").html("您输入的手机格式错误");
				$("#TelNoError").css("color","red");
				error7=2;
			}
		}
	});
	
	//验证账户名
	 $("#bankName").blur(function(){
		var bankName=$("#bankName").val();
		var reg1 = /^[\u4E00-\u9AF5]+$/;
		var pattern1 = /^[A-Za-z]+$/;
		if(bankName == ""){
			$("#bankNameError").css("color","red");
			$("#bankNameError").html("请输入您的姓名");
			error8=0;
		}else{
			if(!(reg1.test(bankName)|| pattern1.test(bankName))){
				$("#bankNameError").html("请认真填写您的姓名");
				$("#bankNameError").css("color","red");
				error8=2;
			}else{
				$("#bankNameError").html("（请输入您的账户名）");
				$("#bankNameError").css("color","#7a7a7a");
				error8=1;
			}
		}
	 });
	 
	  //验证银行卡号
	 $("#bankNo").blur(function(){
		var bankNo=$("#bankNo").val();
		var reg = /^[0-9]{19}$/;
		if(bankNo== ""){
			$("#bankNoError").html("请输入您的银行卡号或网银账号");
			$("#bankNoError").css("color","red");
			error9=0;
		}else{
			if (!reg.exec(bankNo)) {
				$("#bankNoError").html("银行卡号输入错误，请仔细填写");
				$("#bankNoError").css("color","red");
				error9=2;
			}else{
				$("#bankNoError").html("（请输入您的银行卡号）");
				$("#bankNoError").css("color","#7a7a7a");
				error9=1;
			}
		}
	 });
	//重复验证银行卡号
	 $("#repBankNo").blur(function(){
		var repBankNo=$("#repBankNo").val();
		var bankNo=$("#bankNo").val();
		var reg = /^[0-9]{19}$/;
		if(repBankNo == ""){
			$("#repBankNoError").css("color","red");
			$("#repBankNoError").html("请再次输入您的银行卡号或网银账号");
			error10=0;
		}else{
			if(repBankNo!= bankNo){
			$("#repBankNoError").css("color","red");
			$("#repBankNoError").html("两次输入的银行卡号不同");
		  error10=2;
	  }else{
	      $("#repBankNoError").css("color","#7a7a7a");
	      $("#repBankNoError").html("请再次输入您的银行卡号或网银账号");
		  error10=1;
	    }
	   }
	 });
	 //确认修改按钮的click事件
	 $("#MarkSureEdit").click(function(){
	 if(error4 !=1 || error5 !=1 || error6 !=1 || error7 !=1 || error8 !=1 || error9 !=1 || error10 !=1){
		if(error4 == 0){
			$("#bankAddressError").css("color","red");
			$("#bankAddressError").html("请输入您的开户行网点");
		}else if(error5 == 0){
			$("#contentPersonError").css("color","red");
			$("#contentPersonError").html("请输入您的姓名");
		}else if(error5 == 2){
			$("#contentPersonError").html("请认真填写您的姓名");
			$("#contentPersonError").css("color","red");
		}else if(error6 == 0){
			$("#personQQError").html("QQ号码的长度不能小于5");
			$("#personQQError").css("color","red");
		}else if(error6 == 2){
			$("#personQQError").html("QQ号码的格式输入错误");
			$("#personQQError").css("color","red");
		}else if(error7 == 0){
			$("#TelNoError").html("请输入您的手机号码");
			$("#TelNoError").css("color","red");
		}else if(error7 == 2){
			$("#TelNoError").html("您输入的手机格式错误");
			$("#TelNoError").css("color","red");
		}else if(error8 == 0){
			$("#bankNameError").css("color","red");
			$("#bankNameError").html("请输入您的姓名");
		}else if(error8 == 2){
			$("#bankNameError").html("请认真填写您的姓名");
			$("#bankNameError").css("color","red");
		}else if(error9 == 0){
			$("#bankNoError").html("请输入您的银行卡号或网银账号");
			$("#bankNoError").css("color","red");
		}else if(error9 == 2){
			$("#bankNoError").html("银行卡号输入错误，请仔细填写");
			$("#bankNoError").css("color","red");
		}else if(error10 == 0){
			$("#repBankNoError").css("color","red");
			$("#repBankNoError").html("请再次输入您的银行卡号或网银账号");
		}else if(error10 == 2){
			$("#repBankNoError").css("color","red");
			$("#repBankNoError").html("两次输入的银行卡号不同");
		}
	 }else{
			if($("#bankNo").val() ==$("#repBankNo").val()){
				alert("success");
				$.post();
			}else{
				$("#repBankNoError").css("color","red");
				$("#repBankNoError").html("两次输入的银行卡号不同");
			}
	 }
	 });
	 
	 //银行网点的查询
	 
	 $("#showWebInfo").click(function(){
		iDialog({
            title:false,
            padding: '0px 0px 0px 0px',
            content: document.getElementById('BankWebShow'),
            lock: true,
            width: 545,
            fixed: true,
            opacity: 0.1,
            effect: 'i-top-slide',
            height: 368,
            init: function () {
                var that = this;
                $("#closeBankHelp").click(function () {
                    that.hide();
                });
            }
         });
	 });
	});
