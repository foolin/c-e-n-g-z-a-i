<?php
/**********
 * 发送邮件 *
 **********/
function send_mail($address,$title,$message)
{
    vendor('PHPMailer.class#phpmailer');

    $mail=new PHPMailer();
    // 设置PHPMailer使用SMTP服务器发送Email
    $mail->IsSMTP();

    // 设置邮件的字符编码，若不指定，则为'UTF-8'
    $mail->CharSet='UTF-8';

    // 添加收件人地址，可以多次使用来添加多个收件人
    $mail->AddAddress($address);

    // 设置邮件正文
    $mail->Body=$message;

    // 设置邮件头的From字段。
    $mail->From=C('MAIL_ADDRESS');

    // 设置发件人名字
    $mail->FromName='foolin';

    // 设置邮件标题
    $mail->Subject=$title;

    // 设置SMTP服务器。
    $mail->Host=C('MAIL_SMTP');
	
	// 设置端口
	$mail->Port= 25;

    // 设置为"需要验证"
    $mail->SMTPAuth=true;

    // 设置用户名和密码。
    $mail->Username=C('MAIL_LOGINNAME');
    $mail->Password=C('MAIL_PASSWORD');
	
	//是否调试
	$mail->SMTPDebug  = false;
	
	//是否html邮件
	$mail->IsHTML(true);

    // 发送邮件。
    return($mail->Send());
}

//判断是否是邮箱
function is_email($email){
	if(empty($email)){
		return false;
	}
	$rule = '/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/';
	return preg_match($rule,$email)===1;
}

//取当前时间
function now_datetime(){
	return date('Y-m-d H:i:s');
}

//获取邮箱domain
function get_email_domain($email){
	if(empty($email)){
		return '';
	}
	$domain = substr($email, stristr($email, '@'));
	return $domain;
}

/****************** 加解密 ***************/
//DES加密
function des_encrypt($str, $key)      
{      
    $block = mcrypt_get_block_size('des', 'ecb');      
    $pad = $block - (strlen($str) % $block);      
    $str .= str_repeat(chr($pad), $pad);      
    $data = mcrypt_encrypt(MCRYPT_DES, $key, $str, MCRYPT_MODE_ECB);
	return base64_encode($data);    
}

//DES解密
function des_decrypt($str, $key)      
{
	$str = base64_decode($str);
    $str = mcrypt_decrypt(MCRYPT_DES, $key, $str, MCRYPT_MODE_ECB);
    $block = mcrypt_get_block_size('des', 'ecb');      
    $pad = ord($str[($len = strlen($str)) - 1]);      
    return substr($str, 0, strlen($str) - $pad);      
}


/*************** 用户相关  *************/
//取用户，或者某个用户的信息
function user($userid, $field = ''){
    //dump($userid);
    $dbUser = M('User');
    $user = $dbUser->find($userid);
    if(empty($user)){
        return NULL;
    }
    //dump($user);
    if(empty($field)){
        return $user;
    }
    else{
        return $user[$field];
    } 
}

function user_avatar($userid){
    $user = M('User')->find($userid);
    if(empty($user) || empty($user['avatar'])){
        return "__PUBLIC__/img/noavatar.jpg";
    }
    //dump($user);
    return "__PUBLIC__/upload/".$user['avatar'];
}

?>