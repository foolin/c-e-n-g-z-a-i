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
function des_encrypt($str, $key="")      
{
    if(empty($key)){
        $key = C('AUTH_DES_KEY');
    }   
    $block = mcrypt_get_block_size('des', 'ecb');      
    $pad = $block - (strlen($str) % $block);      
    $str .= str_repeat(chr($pad), $pad);      
    $data = mcrypt_encrypt(MCRYPT_DES, $key, $str, MCRYPT_MODE_ECB);
	return base64_encode($data);    
}


//DES解密
function des_decrypt($str, $key="")      
{
    if(empty($key)){
        $key = C('AUTH_DES_KEY');
    }
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

//性别名称
function sex($sex){
    if($sex == 1){
        return '男';
    }
    else if($sex == 2){
        return '女';
    }
    else{
        return '保密';
    }
}

//计算年龄
function age($birth){
    if(empty($birth)){
        return 0;
    }
    list($biry, $birm, $bird) = explode('-', $birth);
    //get current year, month and day
    $y = date('Y');
    $m = date('m');
    $d = date('d');
    $age = $y - $biry;
    if ($m < $birm) {
        $age--;
    }
    elseif ($m = $birm) {
        if ($d < $bird) {
            $age--;
        }
    }
    return $age;
}

/**
*getConstellation 根据出生生日取得星座
*
*@param String $brithday 用于得到星座的日期 格式为yyyy-mm-dd
*
*@param Array $format 用于返回星座的名称
*
*@return String
*/
function constellation($birthday, $format=null)
{
    if(empty($birthday)){
        return "火星座";
    }
    $pattern = '/^\d{4}-\d{1,2}-\d{1,2}$/';
    if (!preg_match($pattern, $birthday, $matchs))
    {
        return null;
    }
    $date = explode('-', $birthday);
    $year = $date[0];
    $month = $date[1];
    $day   = $date[2];
    if ($month <1 || $month>12 || $day < 1 || $day >31)
    {
        return null;
    }
    //设定星座数组
    $constellations = array(
          '摩羯座', '水瓶座', '双鱼座', '白羊座', '金牛座', '双子座',
          '巨蟹座','狮子座', '处女座', '天秤座', '天蝎座', '射手座',);
    
    /*或 ‍‍$constellations = array(
          'Capricorn', 'Aquarius', 'Pisces', 'Aries', 'Taurus', 'Gemini',
          'Cancer','Leo', 'Virgo', 'Libra', 'Scorpio', 'Sagittarius',);*/
    
    //设定星座结束日期的数组，用于判断
    $enddays = array(19, 18, 20, 20, 20, 21, 22, 22, 22, 22, 21, 21,);
    //如果参数format被设置，则返回值采用format提供的数组，否则使用默认的数组
    if ($format != null)
    {
        $values = $format;
    }
    else
    {
        $values = $constellations;
    }
    //根据月份和日期判断星座
    switch ($month)
    {
        case 1:
          if ($day <= $enddays[0])
          {
            $constellation = $values[0];
          }
          else
          {
            $constellation = $values[1];
          }
          break;
        case 2:
          if ($day <= $enddays[1])
          {
            $constellation = $values[1];
          }
          else
          {
            $constellation = $values[2];
          }
          break;
        case 3:
          if ($day <= $enddays[2])
          {
            $constellation = $values[2];
          }
          else
          {
            $constellation = $values[3];
          }
          break;
        case 4:
          if ($day <= $enddays[3])
          {
            $constellation = $values[3];
          }
          else
          {
            $constellation = $values[4];
          }
          break;
        case 5:
          if ($day <= $enddays[4])
          {
            $constellation = $values[4];
          }
          else
          {
            $constellation = $values[5];
          }
          break;
        case 6:
          if ($day <= $enddays[5])
          {
            $constellation = $values[5];
          }
          else
          {
            $constellation = $values[6];
          }
          break;
        case 7:
          if ($day <= $enddays[6])
          {
            $constellation = $values[6];
          }
          else
          {
            $constellation = $values[7];
          }
          break;
        case 8:
          if ($day <= $enddays[7])
          {
            $constellation = $values[7];
          }
          else
          {
            $constellation = $values[8];
          }
          break;
        case 9:
          if ($day <= $enddays[8])
          {
            $constellation = $values[8];
          }
          else
          {
            $constellation = $values[9];
          }
          break;
        case 10:
          if ($day <= $enddays[9])
          {
            $constellation = $values[9];
          }
          else
          {
            $constellation = $values[10];
          }
          break;
        case 11:
          if ($day <= $enddays[10])
          {
            $constellation = $values[10];
          }
          else
          {
            $constellation = $values[11];
          }
          break;
        case 12:
          if ($day <= $enddays[11])
          {
            $constellation = $values[11];
          }
          else
          {
            $constellation = $values[0];
          }
          break;
    }
    return $constellation;
}

 /**
 +----------------------------------------------------------
 * 用户头像URL
 +----------------------------------------------------------
 */
function avatar_url($avatar){
    $encrypt_avatar = des_encrypt($avatar);
    return U('Public/get_avatar', "avatar=$encrypt_avatar") ;
}

 /**
 +----------------------------------------------------------
 * 获取认识好友的连接
 +----------------------------------------------------------
 */
function follow_link($login_userid, $userid){
    if(!is_numeric($login_userid) || !is_numeric($userid)){
        return -1;
    }
    $dbUser = M('Friend');
    $friend = $dbUser->where(array('userid'=>$login_userid, 'frienduserid'=>$userid))->find();
    $htmllink = "";
    //dump($friend); die;
    if(!empty($friend)){
        if($friend['type'] == 1){
            $htmllink = "<a href=". U('Friend/ajax_follow_del', "userid=$userid") 
            ." action='follow' title='点击取消认识' class='btn btn-info disabed '>"
            ."<i class=\"icon-retweet icon-white\"></i> 朋友</a>";
        }
        else if($friend['type'] == 0){
            $htmllink = "<a href=". U('Friend/ajax_follow_del', "userid=$userid") 
            ." action='follow' title='点击取消认识' class='btn btn-info'>"
            ."<i class=\"icon-ok icon-white\"></i> 已认识</a>";
        }
        else if($friend['type'] == -1){
            $htmllink = "<a href=". U('Friend/ajax_follow_del', "userid=$userid") 
            ." action='follow' title='点击取消拉黑' class='btn btn-info'>"
            ."<i class=\"icon-minus icon-white\"></i> 黑名单</a>";
        }
    }
    else{
        $htmllink = "<a href=". U('Friend/ajax_follow_add', "userid=$userid") 
        ." action='follow' title='点击加为认识' class='btn btn-info '>"
        ."<i class=\"icon-plus icon-white\"></i> 加认识</a>";
    }
    
    return $htmllink;
}
?>