<?php
/**
 +------------------------------------------------------------------------------
 * BaseAction 前台基础类，所有都要集成此接口
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   1.0 2012-05-21 21:09:02
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class BaseAction extends Action {
    
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
                //1.首先得到明文Val：用户ID|随机验证码|注册时间的MD5校验码
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
                $user['credit'] = empty($user['credit']) ? 1 : ($user['logincount']+1); //积分+1
                $dbUser->data($user)->save();
                
                session('LOGIN_USER', $user);   //存入session
        }
        return $user;
        //return array('userid'=>'10000','name'=>'刘付灵', 'sex'=>'0');
        //return array();
    }
    
    //数据，信息，状态，函数
    function jsonp($data, $info='', $status=1, $func='callback'){
        $result  =  array();
        $result['status']  =  $status;
        $result['info'] =  $info;
        $result['data'] = $data;
        if(empty($func)){
            $func = 'callback';
        }
        // 返回JSON数据格式到客户端 包含状态信息
        header('Content-Type:text/html; charset=utf-8');
        $strjson = "$func(" .json_encode($result). ")";;
        exit($strjson);
    }
}

?>