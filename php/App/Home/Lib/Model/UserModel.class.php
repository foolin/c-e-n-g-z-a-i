<?php
/**
 +------------------------------------------------------------------------------
 * t_user实体
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-11 00:06:19
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class UserModel extends Model {
	//自动验证
	protected $_validate = array(
		array('verify','require','验证码必须！'), //默认情况下用正则进行验证
		array('email','email','邮箱不合法！',0,'',1), // 在新增的时候验证name字段是否唯一
		array('email','','邮箱已经被注册！',0,'unique',1), // 在新增的时候验证name字段是否唯
		array('name','require','真实姓名不能为空！'), //默认情况下用正则进行验证
		array('sex',array(1,2),'性别不正确！',2,'in'), // 当值不为空的时候判断是否在一个范围内致
		array('password','checkPwd','密码长度必须6~16位之间',0,'callback'), // 自定义函数验证密码格式
	);
	
	//自动完成
	protected $_auto = array (
		array('password','md5',1,'function') , // 对password字段在新增的时候使md5函数处理
		array('state', '0', 1), // 注册时，字段默认为0
		array('regtime',"getCurrentDatetime",1,'callback'), // 对regtime字段在更新的时候写入当前时间戳
		array('regip','get_client_ip',1,'function'), //取ip
	);
	
	//检查密码长度
	function checkPwd(){
		$password = $_POST['password'];
		if(strlen($password) < 6 || strlen($password) > 16){
			return false;
		}
		return true;
	}
	
	//取当前时间
	function getCurrentDatetime(){
		return date('Y-m-d H:i:s');
	}
}

?>