<?php
/**
 +------------------------------------------------------------------------------
 * BaseAction 前台基础类，所有都要集成此接口
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-09 20:14:11
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class BaseAction extends Action {
	
    /**
     +----------------------------------------------------------
     * 检查是否登录
	 * 登录则返回true，不登录则直接跳转到登录页面
     +----------------------------------------------------------
     */
	function checkLogin(){
		$user = $this->getLoginUser();
		if(empty($user)){
			$this->redirect('Account/login');
		} 
	}
	
	
	/**
     +----------------------------------------------------------
     * 取用户登录信息
	 * 返回用户实体
     +----------------------------------------------------------
     */
	function getLoginUser(){
		$user = session('LOGIN_USER');
		if(empty($user)){
				//加密算法：
				//1.首先得到明文Val：用户ID|时间|注册时间的MD5校验码
				//2.把明文经过DESEncrypt加密得到密文SecrectVal
				//3.把密文SecrectVal写入Cookies
				//解密，逆着解即可
				$des_data = cookie('LOGIN_USER');
				if(empty($des_data)){
					return null;
				}
				$source_data = des_decrypt($des_data, C('AUTH_DES_KEY'));
				if(empty($source_data)){
					return null;
				}
				$keys = explode('|', $source_data);
				if(count($keys) != 3){
					return null;
				}
				$dbUser = M('User');
				$user = $dbUser->getByUserid($keys[0]);
				//判断用户是否合法
				if(empty($user) || $user['state'] == -1 || md5($user['regtime']) != $keys[2]){
					cookie('LOGIN_USER',null);
					return null;
				}
				
				//记录并加积分
				$user['loginip'] = get_client_ip();
				$user['logintime'] = date('Y-m-d H:i:s');
				$user['logincount'] = empty($user['logincount']) ? 1 : ($user['logincount']+1); //登录次数+1
				$user['credit'] = empty($user['credit']) ? 1 : ($user['logincount']+1);	//积分+1
				$dbUser->data($user)->save();
				
				session('LOGIN_USER', $user);	//存入session
		}
		return $user;
		//return array('userid'=>'10000','name'=>'刘付灵', 'sex'=>'0');
		//return array();
	}
	
	//
	function tpl($title=''){
		//标题
		if(empty($title)){
			$title = C('SITE_SLOGAN');
		}
		$this->assign('title', $title);
		
		//登录用户
		$user = $this->getLoginUser();
		$this->assign('user', $user);
		
		//显示模板
		$this->assign('headerfile', 'Layout:header');
		$this->assign('footerfile', 'Layout:footer');
		//$this->display('Layout:header');
		$this->display();
		//$this->display('Layout:footer');
	}
}

?>