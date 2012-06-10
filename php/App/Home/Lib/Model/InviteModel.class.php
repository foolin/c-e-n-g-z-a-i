<?php
/**
 +------------------------------------------------------------------------------
 * t_invite实体
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-13 16:13:08
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class InviteModel extends Model {
	//自动验证
	protected $_validate = array(
		array('email','email','邮箱不合法！',0,'',1), // 在新增的时候验证name字段是否唯一
		array('email','','该邮箱已经提交过申请！',0,'unique',1), // 在新增的时候验证name字段是否唯
		array('name','require','真实姓名不能为空！'), //默认情况下用正则进行验证
		array('sex',array(1,2),'性别不正确！',2,'in'), // 当值不为空的时候判断是否在一个范围内致
	);
	
	//自动完成
	protected $_auto = array (
		array('state', '0', 1), // 注册时，字段默认为0
		array('createtime',"now_datetime",1,'function'), // 对regtime字段在更新的时候写入当前时间戳
	);
	
	//检查密码长度
	function checkPwd(){
		$password = $_POST['password'];
		if(strlen($password) < 6 || strlen($password) > 16){
			return false;
		}
		return true;
	}
}

?>