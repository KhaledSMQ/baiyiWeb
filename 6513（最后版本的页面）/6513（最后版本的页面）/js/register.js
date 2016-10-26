     var error1=0;
	var error2=0;
	var error3=0;
	var error4=0;
	var error5=0;
	var error6=0;
	//触发焦点的提示
	function errorTips(o, t, c) {
			var c = c || "";
			var html = "<p><i class=" + c + "></i>" + t + "</p>"
			o.parent().append(html);
		}
    //填写正确的提示
	function successTips(o, t) {
		var t="";
		var html = "<p><i class='true'></i>" + t + "</p>"
		o.parent().append(html)
	} 
	
	//显示验证码
	function ShowValidatePic() {
		if ($("#validatePicArea").style.display == "none") {
			$("#validatePicArea").style.display = "inline";
			$("#validateImage").src = "ValidationImageReg.ashx?" + Math.random();
		}
	}
	$(document).ready(function(){
	//获取焦点的部分
	var regist=$("#regist");
	var registerInput=regist.find("input").not("#btnSubmit");
	
	registerInput.focus(function(){
		var  _this=$(this);
		if(_this.parent().find("i").attr("class") == "true"){
		     return false;
		}
		_this.parent().find("p").remove();
		//账号获取焦点
		if(this.id=="txtAccount"){
		  var t="4-12位英文或数字，区分大小写";
		  errorTips(_this,t,"warn");
		  return false;
		}
		//昵称获取焦点
		if(this.id == "txtNickName"){
		var t="6-18个字符";
		errorTips(_this,t,"warn");
		  return false;
		}
		//设置密码获取焦点
		if(this.id == "txtPassword"){
		   var t="请输入您的密码"
		   errorTips(_this,t,"warn");
		   return false;
		}
		//确认密码获取焦点
		if(this.id == "txtConfirmPassword"){
			var t="请再次输入您的密码"
		   errorTips(_this,t,"warn");
		   return false;
		}
		//验证码获取焦点
		if(this.id == "txtValidCode"){
		   ShowValidatePic();
		}
	});
	//blur 事件
	registerInput.blur(function(){
	    var v=this.value;
		var _this=$(this);
		_this.parent().find("p").remove();
		//验证账号
		if(this.id == "txtAccount"){
			if (v==""){
				var t="账号不能为空";
				errorTips(_this,t,"error");
				error1=0;
				return false;
			}
			if (v.length<4 || v.length>12){
				var t="长度不正确，请输入4-12位的英文或数字";
				errorTips(_this,t,"error");
				error1=2;
				return false;
			}
			var pattentn=/^[A-Za-z0-9]+$/;
			if(!pattentn.test(v)){
				var t="账号只能是英文和或数字的组合";
				errorTips(_this,t,"error");
				error1=3;
				return false;
			}
			successTips(_this);	
			error1=1;
			return true;
		}
		//昵称的验证
		if(this.id=="txtNickName"){
			var nickName=v;
			if(nickName == ""){
				var t="昵称不能为空";
				errorTips(_this,t,"error");
				error2=0;
				return false;
			}
			if(nickName.length<2 || nickName.length>16){
				var t="昵称的长度为2到16";
				errorTips(_this,t,"error");
				error2=2
				return false;
			}
			successTips(_this);	
			error2=1;
			return true;
		}
		//验证密码
		if(this.id=="txtPassword"){
			if(v == ""){
				errorTips(_this,"密码不能为空","error");
				error3=0;
				return false;
			}
			if(v == $("#txtAccount").val()){
				errorTips(_this,"密码和账号不能一样","error");
				error3=2
				return false;
			}
			if(v.length<6 || v.length>18){
			var t="密码的长度为6到18的英文或数字";
				errorTips(_this,t,"error");
				error3=3;
				return false;
			}
			var pattern=/^[A-Za-z0-9]+$/;
			if(!pattern.test(v)){
				var t="密码只能是英文和或数字的组合";
				errorTips(_this,t,"error");
				error3=4
				return false;
			}
			successTips(_this,v);
			error3=1;
			return true;
		}
		//确认密码
		if(this.id=="txtConfirmPassword"){
			var pwd=$("#txtPassword").val();
			if(v == ""){
				errorTips(_this,"确认密码不能为空","error");
				error4=0;
				return false;
			}
			if(v.length<6 || v.length>18){
				var t="密码的长度为6到18的英文或数字";
				errorTips(_this,t,"error");
				error4=2;
				return false;
			}
			if(pwd !=v){
				var t="两次输入的密码不一致";
				errorTips(_this,t,"error");
				error4=3;
				return false;
			}
			successTips(_this);
			error4=1;
			return true;
		}
		
		
		//检查阅读协议
	 $("#Checkbox1").blur(function(){
		if ($("#Checkbox1").attr("checked")) {
		    error6=1;
			return true;
		}
		error6=0;
		return false;
	  });
	});
	  $("#btnSubmit").click(function(){
	    if(error1 !=1 || error2 !=1 || error3 !=1 || error4 !=1 || error6!=1){
			if(error1==0){
				var t="用户名不能为空";
				var html = "<p><i class=" + error + "></i>" + t + "</p>"
				$("#txtAccount").parent().append(html);
			}
			if(error1==2){
				var t="长度不正确，请输入4-12位的英文或数字";
				var html = "<p><i class=" + error + "></i>" + t + "</p>"
				$("#txtAccount").parent().append(html);
			}
			if(error1==3){
				var t="账号只能是英文和或数字的组合";
				var html = "<p><i class=" + error + "></i>" + t + "</p>"
				$("#txtAccount").parent().append(html);
			}
			if(error2==0){
				var t="昵称不能为空";
				var html = "<p><i class=" + error + "></i>" + t + "</p>"
				$("#txtNickName").parent().append(html);
			}
			if(error2==2){
				var t="昵称的长度为2到16";
				var html = "<p><i class=" + error + "></i>" + t + "</p>"
				$("#txtNickName").parent().append(html);
			}
			if(error3==0){
				var t="密码不能为空";
				var html = "<p><i class=" + error + "></i>" + t + "</p>"
				$("#txtPassword").parent().append(html);
			}
			if(error3==2){
				var t="密码和账号不能一样";
				var html = "<p><i class=" + error + "></i>" + t + "</p>"
				$("#txtPassword").parent().append(html);
			}
			if(error3==3){
				var t="密码的长度为6到18的英文或数字";
				var html = "<p><i class=" + error + "></i>" + t + "</p>"
				$("#txtPassword").parent().append(html);
			}
			if(error3==4){
				var t="密码只能是英文和或数字的组合";
				var html = "<p><i class=" + error + "></i>" + t + "</p>"
				$("#txtPassword").parent().append(html);
			}
			if(error4==0){
				var t="确认密码不能为空";
				var html = "<p><i class=" + error + "></i>" + t + "</p>"
				$("#txtConfirmPassword").parent().append(html);
			}
			if(error4==2){
				var t="密码的长度为6到18的英文或数字";
				var html = "<p><i class=" + error + "></i>" + t + "</p>"
				$("#txtConfirmPassword").parent().append(html);
			}
			if(error4==3){
				var t="两次输入的密码不一致";
				var html = "<p><i class=" + error + "></i>" + t + "</p>"
				$("#txtConfirmPassword").parent().append(html);
			}
			if(error6==0){
				alert("请先选中阅读协议");
			}
		}else{
		 alert("注册成功");
		
		}
	  
	  });
	});
