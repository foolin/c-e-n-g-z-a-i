//****** 取用户信息卡片 *********/
//打开用户卡片
function openUserCard(_this){
	var uid = $(_this).attr('usercard');
	var offset =$(_this).offset();
	if(uid == null || uid == ""){
		return;
	}
	//清除其它卡片
	$('[class="user-card"]').hide();
	//清除关闭的timeout
	if(__timeout__){
		clearTimeout(__timeout__);
	}
	
	__cardid__ = '#user-card-' + uid;
	var card = $(__cardid__);
	
	if(card.length > 0){
		card.css('top', offset.top + 30);
		card.css('left', offset.left);
		card.css('display', 'block');
		addCloseUserCardEvent();
		return;
	}
	//异地获取
	$.ajax(APP.HOST + '/public/ajaxusercard',{
		type:'GET',
		data:'userid=' + uid,
		dataType:'json',
		success:function(msg){
			if(msg.status == 1){
				card = $(msg.data);
				$('body').append(card);
				console.log(offset);
				card.css('top', offset.top + 30);
				card.css('left', offset.left);
				card.css('display','block');
				addCloseUserCardEvent();
			}
			else{
				$.dialog({
				    title: '获取用户信息失败！',
				    content: msg.info,
		    		focus: false,
					follow:this
				});
			}
			
		}
	});
}

var __cardid__ = null;	//cardid，包括#
var __timeout__ = null;	//定时器

//新增用户卡的事件
function addCloseUserCardEvent(){
	//鼠标移上去时清除定时器
	$(__cardid__).mouseover(function(){
		//清除关闭的timeout
		if(__timeout__){
			clearTimeout(__timeout__);
		}
	});
	//鼠标离开时关闭
	$(__cardid__).mouseleave(function(){
		//closeUserCard();
		__timeout__ = setTimeout("closeUserCard();", 500);
	});
}

//隐藏卡片信息
function closeUserCard(){
	$(__cardid__).hide();
}

//初始化
//使用属性： usercard="用户id"
function initUserCard(){
	$("[usercard]").mouseover(function(){	
		openUserCard(this);
	}).mouseleave(function(){
		//closeUserCard();
		__timeout__ = setTimeout("closeUserCard();", 500);
	});
}


$(document).ready(function(){
	initUserCard();
})
