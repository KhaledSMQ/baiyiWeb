$(document).ready(function () {
            //Vip页面不同等级时鼠标悬浮切换不同块的状态事件
            var tab_li = $(".title ul li");
            tab_li.hover(function () {
                $(this).addClass("active").siblings().removeClass("active");
                var index = tab_li.index(this);
                $("div.showAllInfo  > div").eq(index).show().siblings().hide();
            });

            $(".navBar1 ul li ").each(function () {
              $(this).bind("hover", function () {
                  $(this).siblings().css("background", "none");
                  $(this).css("background", "url('images/2-1.jpg') 50% 100% no-repeat");
              });
          });
		  
		  $("#images1").hover(function(){
		  $(this).attr("src","images/4-1-1.jpg");
		  },function(){
		  $(this).attr("src","images/4-1.jpg");
		  });
		   $("#images2").hover(function(){
		  $(this).attr("src","images/4-2-2.jpg");
		  },function(){
		  $(this).attr("src","images/4-2.jpg");
		  });
		   $("#images3").hover(function(){
		  $(this).attr("src","images/4-3-3.jpg");
		  },function(){
		  $(this).attr("src","images/4-3.jpg");
		  });
		   $("#images4").hover(function(){
		  $(this).attr("src","images/4-4-4.jpg");
		  },function(){
		  $(this).attr("src","images/4-4.jpg");
		  });
		  
		  	//文章信息的切换
			var tab_lis = $(".newsListShow ul li");
            tab_lis.hover(function () {
                $(this).addClass("showtitleBg").siblings().removeClass("showtitleBg");
                var index = tab_lis.index(this);
                $("div.showAllNewsInfo  > div").eq(index).show().siblings().hide();
            });
			
        });