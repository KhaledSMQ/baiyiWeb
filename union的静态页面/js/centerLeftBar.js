$(document).ready(function(){
$(".leftList").find("li").each(function(j){
	$(this).bind("click",function(){
	var index=$(".leftList").find(".current").index();
	for(var i=0;i<=$(".leftList").find("li").length;i++){
		if(i==j){
		$(".leftList").find("li").eq(index).find("img").attr("src","img/userCenter/z"+(index+1)+".png");
		$(".leftList").find("li").eq(index).removeClass("current");
		$(this).find("img").attr("src","img/userCenter/c"+(i+1)+".png");
		$(this).addClass("current");
		var data=$(".leftList").eq(j);
		data.siblings(".leftList").hide();
		data.show();
	  }
	}
});
});
$("#returnIndex").click(function(){
	window.location.href="";
});
$("#closePage").click(function(){
	alert("确定要退出吗？")
	window.location.href="";
});
});
