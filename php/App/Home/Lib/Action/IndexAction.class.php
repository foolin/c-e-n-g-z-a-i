<?php
/**
 +------------------------------------------------------------------------------
 * IndexAction 默认首页，非登录时显示
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-09 20:14:11
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class IndexAction extends BaseAction {
	
    /**
     +----------------------------------------------------------
     * 默认首页
     +----------------------------------------------------------
     */
	function index(){
		$this->checkLogin();
		echo '<a href="'. U('Account/login') .'">登录</a>';
		echo '<a href="'. U('Account/register') .'">注册</a>';
	}

}

?>