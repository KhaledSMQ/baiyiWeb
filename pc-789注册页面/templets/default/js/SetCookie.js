/** 统计 */
var tempIdName = "789gameTempUid";

function addCookie(name, value, expireMinute) {
	var cookieString = name + "=" + escape(value)+";path=/";
	// 判断是否设置过期时间
	if (expireMinute > 0) {
		var date = new Date();
		date.setTime(date.getTime() + expireMinute  * 60 * 1000 * 60 * 24);
		cookieString = cookieString + "; expires=" + date.toGMTString();
	}
	document.cookie = cookieString;
}
// 获得cookies
function getCookie(cookie_name) {
	var results = document.cookie.match('(^|;) ?' + cookie_name + '=([^;]*)(;|$)');
	if (results) {
		return (unescape(results[2]));
	} else {
		return null;
	}
}
// 删除cookies
function deleteCookie(name) {
	var date = new Date();
	date.setTime(date.getTime() - 10000);
	document.cookie = name + "=v;path=/; expires=" + date.toGMTString();
}

//设置cookies的值
function getTimestamp() {
	var timestamp = Date.parse(new Date());
	var temp = Math.round(Math.random() * 30000000);
	var math = timestamp +""+temp;
	return math;
}
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return "";
}

var referer =  document.referrer;
//从百度获得关键字ID
var pid = getQueryString("pid");
//获得从大厅传递过来的用户临时ID
var tempId = getQueryString("registerKey");
var pbkey = getQueryString("pbkey");
//如果有推荐人key，保存在cookie
if(pbkey != null) addCookie("tuijianren", pbkey, 30);

//如果不是来自大厅，则尝试从cookie获得用户临时ID，否则将来自大厅的用户临时ID保存到cookie
if(tempId == null)  tempId = getCookie(tempIdName);
else
	addCookie(tempIdName, tempId, 30);

//如果大厅里没有，cookie里没有，用户第一次来
if (tempId == null || tempId == "") {
	tempId = getTimestamp();
	addCookie(tempIdName, tempId, 30);
	
//用户不是一次来但是重新从其它网站过来（一般是搜索引擎），重新生成临时ID，记录新的关键字
}else if( referer!='' && referer.indexOf("789game.") < 0 ){
	deleteCookie(tempIdName);
	tempId = getTimestamp();
	addCookie(tempIdName, tempId, 30);
}

$.ajax({
	type : "post",
	async : false,
	data : {
		referer : document.referrer,
		tempId : tempId,
		title : document.title,
		pid : pid
	},
	url : "http://tongji.789game.net:8081/data/collect",
	dataType : "jsonp",
	jsonp : "callback",// 服务端用于接收callback调用的function名的参数
	jsonpCallback : "callback",// callback的function名称
	success : function(json) {
		var host = window.location.href;
		if (host.indexOf("register.html") >= 0||host.indexOf("qp") >= 0||host.indexOf("buyu") >= 0) {
			$("#host").val(json.tjID);
		}
	},
	error : function() {}
});

function callback(json) {}