function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

$(document).ready(function () {
    $(":input").keypress(function (e) {
        if (e.which == 13) { // 判断所按是否回车键
            reg();
            return false; // 取消默认的提交行为
        }
    });

});
var submitRegData = function () {

    var Url = "../ashx/Reg.ashx";
    var PostData = "Ilogname=" + $("#input_username").val().trim() + "&Ipwd=" + encodeURIComponent($("#input_password").val().trim()) +
            "&InickName=" + $("#input_nickName").val().trim();

    $.ajax({
        type: "POST",
        url: Url,
        data: PostData,
        success: function (pass) {
            if (pass == "success") {
                alert("注册成功！");
                location.href = "../index.aspx";
            }
            else {
                alert(pass);
            }
        }
    });
    return true;
};
function reg() {
    if (checkUserName($('#input_username')) && checkPassword() && checkNickName($('#input_nickName')) && checkRePassword()) {
        //alert("success");
        submitRegData();
    }
}
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
String.prototype.Trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}


// var checkDianXi = function (callback) {
//     var e = $("#input_username");
//     var n = e.val().trim();
//     var reg = /\b(hz|hu|zj|nb|wz|jx|sx|jh|qz|tz|zs|ls)\w+$/;
// 
//     var r = n.match(reg);
//     if (r != null) {
//         alert(r[1] + "开头的是浙江电信用户保留帐号");
//         return false;
//     }
//     return true;
// };

var checkUserName = function (callback) {
    var e = $('#input_username');
    var n = e.val().trim();
    if (n != e.val()) {
        alert("不允许输入空格");
        return false;
    }
    else if (n.length == 0) {
        alert("请输入账号");
        return false;
    } else if (!/^[a-zA-Z][a-zA-Z0-9_]{5,15}$/.test(n)) {
        alert("账号名由字母开头,由6-16个英文字母或数字组成.");
        return false;
    }

    return true;
};
//计算字符串长度(可同时字母和汉字，字母占一个字符，汉字占2个字符)
function strlen(str) {
    var len = 0;
    for (var i = 0; i < str.length; i++) {
        var c = str.charCodeAt(i);
        //单字节加1 
        if ((c >= 0x0001 && c <= 0x007e) || (0xff60 <= c && c <= 0xff9f)) {
            len++;
        }
        else {
            len += 2;
        }
    }
    return len;
}
var checkNickName = function (callback) {

    var u = $('#input_username').val().trim();

    var e = $('#input_nickName');
    var n = e.val().trim();
    if (n != e.val()) {
        alert("不允许输入空格");
        return false;
    }
    else if (n.length == 0) {
        alert("请输入昵称");
        return false;
    } else if (strlen(n) < 6 || strlen(n) > 15) {
        alert("昵称可以为汉字、字母、数字、下划线等都可.");
        return false;
    }
    else if (n == u) {
        alert("账号和昵称不能相同");
        return false;
    }
    return true;
};
var checkPassword = function (callback) {
    var e = $('#input_password');
    var p = e.val().trim();
    if (e.val() != p) {
        alert("密码中不允许输入空格,请重新输入.");
        return false;
    }
    else if (p.length == 0) {
        alert("密码不能为空");
        return false;
    }
    else if (p.length < 6) {
        alert("密码必须大于6位,请重新输入.");
        return false;
    }
    else if (p.length > 16) {
        alert("密码长度必须为6-16位,请重新输入.");
        return false;
    }
    else {
        var chinese = p.match(/[^\x00-\xff]/ig) || [];
        if (chinese.length > 0) {
            alert("密码必须为字母、数字或符号的组合,请重新输入.");
            return false;
        }

    }
    return true;
};
var checkRePassword = function (callback) {
    var e = $('#input_repassword');
    var p = $('#input_password').val().trim();
    var p1 = $('#input_repassword').val().trim();
    if (p1 == "") {
        alert("两次输入密码不一致，请重新输入.");
        return false;
    }
    else if (p1 != "" && $('#input_password').val() != $('#input_repassword').val()) {
        alert("两次输入密码不一致，请重新输入.");
        return false;
    }
    else if (p1.length > 5 && p1.length < 17 && p1 == p) {
        return true;
    } else {
        return false;
    }

};

