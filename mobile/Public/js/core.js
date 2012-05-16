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
