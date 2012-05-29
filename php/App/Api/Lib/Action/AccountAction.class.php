<?php
/**
 +------------------------------------------------------------------------------
 * HomeAction 登录后的首页
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-09 20:14:11
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class AccountAction extends BaseAction {
    public function dologin(){
        header("Content-Type:text/html; charset=utf-8");
        $dbUser = M('User');
        //dump($dbUser->select());
        
        $email = $this->_post('email');
        $password = $this->_post('password');
        $authtoken = $this->_post('authtoken'); //
        
        $data = array('uid'=>10086, 'authtoken' => md5(date()), 'time' => time());
        return $this->jsonp($data, '登录成功！', 1, $this->_get('jsonp'));
    }
}
?>