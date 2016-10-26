// JavaScript Document

//图片切换

document.charset = "utf-8" 

function focus(index,elem){

    var els=$('#focusnum span');

    els.removeClass('active');

    els.eq(index).addClass('active');

}



function ppt(index,elem){

    var els=$('#pptnum span');

    els.removeClass('active');

    els.eq(index).addClass('active');

}
$(window).load(function(){
 $(".newstext img").each(function(){
 var winwidth = document.body.offsetWidth ;

 if(winwidth>480){
 var biwidth = 480;
 }else if(winwidth>=320){
 var biwidth = 280;
 }else{
 var biwidth = 200;
 }
  var w = $(this).width();
  var h = $(this).height();
   if( w > biwidth ){
     var h = Math.ceil(h*(biwidth/w));
    $(this).width(biwidth);
    $(this).height(h);
}

})
})




function elemclick(elem1,elem2){

    elem1.each(function(i){

        var self=this;

        (function(j){

            $(self).on('click',function(){

                elem2.showelem(j);

            })

        })(i)

    })

}



//切换

/*

$(document).ready(function(){



	      //排行榜

		$(".sort_list li a").mouseover(function(){

			$(".sort_list li a").removeClass("current");

			$(".main_list").css("display","none");

			$(this).addClass("current");

			var topid =  $(this).attr("data");

			$("#loadmore").attr("data",topid);

			var cc = $("#loadmore").attr("data");

			var a=$(this).attr('id') + "a";

			$("#" + a).css("display","block");

		});

	

		 //壁纸列表页

		$(".bzcc a").mouseover(function(){

			$(".bzcc a").removeClass("on");

			$(".sjbz_box").css("display","none");

			$(this).addClass("on");

			var b=$(this).attr('id') + "b";

			$("#" + b).css("display","block");

		});

		

	})

	*/

//更多简介

$(function(){
    if($('#j_app_desc').get(0)){
        var descElem = $('#j_app_desc'),
            descHeight = descElem.height(),
            descBtn = $('#j_app_desc_btn'),
            minHeight = 96;

        if(descHeight >= minHeight){
            descElem.css('height', minHeight+'px');
            descBtn.attr('rel', 0).show();
        }

        descBtn.click(function(){
            var rel = $(this).attr('rel');
            if(rel == 0){
                descElem.css('height', 'auto');
                descBtn.html('收起').attr('rel', 1);
				$(".more_btn").addClass("bbj");
            }
            else{
                descElem.css('height', minHeight+'px');
                descBtn.html('更多').attr('rel', 0);
				$(".more_btn").removeClass("bbj");
            }
        });
    }
   
    
});

//底部广告
$(document).ready(function(){
	  $(".dbgg .close").live('click', function () {
        $(".dbgg").hide();
		 $(".footer").removeClass("on");
    });
	})
	
	
	

//加载类

//url路径； wrapId显示的id,get_array传递的数组。typeid类型

function autoLoad(url,wrapId,get_array,typeid) {

	  var tops;

	 var $wraper = $("#"+wrapId);

	     if ($wraper.offset()) {

        tops = $wraper.offset().top;
        
    } else {

        tops = 0

    }



	var page = 2;

	var loading = false;

 $(window).scroll(onScroll);

     function onScroll() {

		if (loading || page>300) return false;	

        var scrollTop = $(window).scrollTop();

		var wrapHeight = parseInt($wraper.height());

		var wH = parseInt($(window).height());		

		var loadBoud = wrapHeight + tops - wH - 50;
            if(typeid==2){
			
			loadBoud = parseInt($(".main").height())- wH + 75;

			}
        if (scrollTop < loadBoud ) return false;

		loading = true;

		$("#loadmore").show();

		var si = setTimeout(loadStart,1000);

    }

function setQueryString(){

var strs=get_array.split("&");

var re = "page="+page;

for (i=0;i<strs.length ;i++ )

{

var strslist=strs[i].split("=");

re += "&"+strslist[0]+"="+strslist[1];

}

   return re;



  }

	function loadStart(){

	 $.ajax({

     type: "GET",

     url: url,

     dataType: "json",

     data: setQueryString(),

     contentType: "application/json",

     success: function(data){

	  if(data){

	    show_data(data.data||[]);

		page++;

	  }

     },

     error: function(){

	  $("#loadmore").hide();

	  $("#sjbz").show();
     $(".footer").show();
	  loading = true;

     },

	

    });

	}

		function show_data(data){

	   for (var i = 0; i<data.length; i++) {

	   if(typeid==1){

			show_data_one(data[i]);

			}else if(typeid==2){

			show_data_arc(data[i]);

			}else{

			show_data_bz(data[i]);

			}

        }

		if(typeid==3){

				if(i==6){

		 loading = false;

		}else{

		$("#loadmore").hide();

		$("#sjbz").show();

		}

		}else{

		if(i==12){

		 loading = false;

		}else{

		$("#loadmore").hide();

		$("#sjbz").show();
		$(".footer").show();

		}

		}

	}

	function show_data_one(list) {

		 var htm = ["<li>                                                                                                                  ",

"  <a href='",list.url,"' class='pic'><img src='",list.thumb,"' width='48' height='48' alt='",list.title,"'></a>",

"   <a href='",list.url,"'><h3 class='name'>",list.title,"</h3>                                             ",

"       <div class='info'>                                                                                             ",

"           <p class='stars star",list.stars,"'></p>                                            ",

"           <span class='soft-size'>",list.filesize,"</span>                                    ",

"       </div> </a>                                                                                                        ",

list.downfile_url,

"   </li> "].join('');

		$wraper.append(htm);

	}

		function show_data_arc(list) {

		 var htm = "<li>›<a href='"+list.url+"'>"+list.title+"</a></li>";

		$wraper.append(htm);

	}		

	function show_data_bz(list) {

		 var htm = " <li><a href='"+list.url+"' class='swipebox' title='"+list.title+"'><img src='"+list.thumb+"' alt='"+list.title+"' /></a></li>";

		$wraper.append(htm);

	}

        





}







