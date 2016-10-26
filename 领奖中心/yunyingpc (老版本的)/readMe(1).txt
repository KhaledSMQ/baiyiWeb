
注意：该请求全部采用jsonp传输协议，如果要传递中文，需要urlencode



1 修改 host:172.16.10.117   test.huazai.com


2:获取默认列表：
	http://test.huazai.com/index.php/Api/Index/getDefaultList.php


3：登陆：
	http://test.huazai.com/index.php/Api/Index/login.php
	param:
		uid:用户输入的id
		phoneno：用户输入的手机号

4：检查是否登陆：
	http://test.huazai.com/index.php/Api/Index/checkIsLogin.php


5：获取登陆的列表：
	http://test.huazai.com/index.php/Api/Index/getLoginList.php


6:获取奖励
	http://test.huazai.com/index.php/Api/Index/getLoginList.php
	param:id 活动的id（在获取商品列表的json中传递）

7。查看领取活动
	http://test.huazai.com/index.php/Api/Index/getRecord.php
	param:
		pageindex:当前页数
		pageshowcount:一页显示数目

8:实物点击兑换
	http://test.huazai.com/index.php/Api/Index/writeForm.php
	param:
		entityid:（获取列表是传递）
		id：（获取列表是传递）


9：实物查看详情功能：
   http://test.huazai.com/index.php/Api/Index/getEntityInfo.php
   	param:
		entityid:（获取列表是传递）
		id：（获取列表是传递）