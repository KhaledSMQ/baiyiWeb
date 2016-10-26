//init
var scrollObj = null;
var scrollSize = 26;
var scrollSpeed = 200;
var scrollTime = 2000;
var bannerObj = null;
var bannerTime = 3000;
var bannerSpeed = 300;
//loading
$(function(){
	//notice_scroll
	$('#web_notice_content').mouseover(function(){
		stop_scroll();
	}).mouseout(function(){
		start_scroll();
	});
	$('#scroll_ul_bottom').html($('#scroll_ul_top').html());
	start_scroll();
	//banner_slider
	banner_slider(0);
	//banner_select
	$('.banner_select > a').hover(function(){
		$('.banner_select > a').removeClass('banner_select_on');
		$(this).addClass('banner_select_on');
		window.clearTimeout(bannerObj);
		var step = parseInt($(this).text());
		step = (step + 1)<$('.banner_item').length? (step + 1) : 0;
		$('.banner_item').hide().eq(step).fadeIn(bannerSpeed);
	},function(){
		var step = parseInt($(this).text());
		step = (step + 1)<$('.banner_item').length? (step + 1) : 0;
		bannerObj = setTimeout("banner_slider("+step+")",bannerTime);
	});
	
	//login-status
	var login_status_url = uri_index_parse('Login.loginInfo');
	$.get(login_status_url,{},function(json){
		if(json.status==1){
			$('#user_login_center_logo,#user_login_content_info').show();
			//userinfo
			$('#userinfo_name').text(json.userinfo.NAME);
			$('#userinfo_uid').text(json.userinfo.UID);
			$('#userinfo_balance').text(json.userinfo.BALANCE);
			$('#userinfo_link').html(json.userinfo.LINK);
			//welcome
			$('#regard_message').text(regard_by_time());
		}else{
			$('#user_login_logo,#user_login_content_table').show();
			var code_url = uri_index_parse('Login.vcode')+'&t='+Date.parse(new Date());
			$('#user_vcode_img').show().attr('src',code_url);
		}
	},'json');
});
//开始滚动
function start_scroll(){
	scrollObj = setInterval("scroll()",scrollTime);
}
//停止滚动
function stop_scroll(){
	window.clearInterval(scrollObj);
}
//滚动
function scroll(){
	var scroll_div_top = $('#web_notice_content').scrollTop();
	var scroll_top_height = $('#scroll_ul_top').outerHeight();
	if( scroll_div_top >= scroll_top_height ){
		scroll_div_top = 0;
		$('#web_notice_content').scrollTop(scroll_div_top);
	}
	$('#web_notice_content').animate({"scrollTop":scroll_div_top+scrollSize+"px"},scrollSpeed);
}

//banner_slider
function banner_slider(step){
	$('.banner_item').hide().eq(step).fadeIn(bannerSpeed);
	$('.banner_select > a').removeClass('banner_select_on').eq(step).addClass('banner_select_on');
	step = (step + 1)<$('.banner_item').length? (step + 1) : 0;
	bannerObj = setTimeout("banner_slider("+step+")",bannerTime);
}


//uri_parse
function uri_index_parse(uri){
	var uriArr = uri.split('.');
	if(uriArr.length==0){
		return ;
	}
	var url = './index.php?action='+uriArr[0]+'&do='+uriArr[1];
	return url;
}
