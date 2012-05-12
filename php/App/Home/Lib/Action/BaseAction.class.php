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
		
	}
	
	
	/**
     +----------------------------------------------------------
     * 取用户登录信息
	 * 返回用户实体
     +----------------------------------------------------------
     */
	function getLoginUser(){
		return array('userid'=>'10000','name'=>'刘付灵', 'sex'=>'0');
		//return array();
	}
	
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
		$this->display('Layout:header');
		$this->display();
		$this->display('Layout:footer');
	}
}

?>