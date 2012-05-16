/*************** 表单提示 *****************/
//成功提示
function success(id, msg){
	$('#' + id).removeClass('input-error');
	$('[validation-for="'+ id +'"]').removeClass().addClass('validation-success').html('√ ' + msg);
}
//失败提示
function error(id, msg){
	$('#' + id).addClass('input-error');
	$('[validation-for="' + id + '"]').removeClass().addClass('validation-error').html('× ' + msg);
}
//隐藏提示
function valid(id){
	$('#' + id).removeClass('input-error');
	$('[validation-for="' + id + '"]').removeClass().addClass('validation-valid').html('');
}


/*********公共函数**********/
function isEmail(email){
	var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
    return reg.test(email);
}




//刷新验证码
function refreshVerify(id) {
	var img = document.getElementById(id);
	img.src = "{:U('Public/verify')}?t=" + new Date().getTime();
}
