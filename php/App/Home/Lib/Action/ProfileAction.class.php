<?php
/**
 +------------------------------------------------------------------------------
 * ProfileAction 登录后的首页
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-09 20:14:11
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class ProfileAction extends BaseAction {
	
    /**
     +----------------------------------------------------------
     * 默认首页
     +----------------------------------------------------------
     */
	function index(){
		$this->check_login();
		//echo "登录成功喇！<a href='".U('Account/login') ."'>登录</a> ！<a href='". U('Account/logout') ."'>退出</a> ";
		$user = $this->get_login_user();
		$userid = $this->_get('userid');
        if(!empty($userid)){
            if(!is_numeric($userid)){
                $this->error("对不起，该用户没有没找到！403", U('Profile/index'));
                exit;
            }
            $dbUser = M('User');
            $user = $dbUser->find($userid);
            if(empty($user)){
                $this->error("对不起，该用户没有没找到！404", U('Profile/index'));
                exit;
            }
        } 
        $this->assign("user", $user);
		$this->tpl('我的主页');
	}
    
    
    /**
     +----------------------------------------------------------
     * 查询个人档案
     +----------------------------------------------------------
     */
    function ajax_profile(){
        if(!$this->isAjax()){
            $this->ajaxReturn("", "非法来源", 0);
            return;
        }
        $user = $login_user = $this->get_login_user();
        if(empty($user)){
            $this->ajaxReturn("", "您尚未登录", 0);
            return;
        }
        $userid = $this->_get('userid');
        if(is_numeric($userid)){
            $user = D('User')->find($userid);
            if(empty($user)){
                $this->ajaxReturn("", "用户不存在！404", 0);
                return;
            }
        }
        $this->assign('user', $user);
        $this->assign('login_user', $login_user);
        $tpl = $this->fetch();
        $this->ajaxReturn($tpl, '读取成功', 1);
        return;
    }
    
    
     /**
     +----------------------------------------------------------
     * 查询个人故事
     +----------------------------------------------------------
     */
    function ajax_story(){
        if(!$this->isAjax()){
            $this->ajaxReturn("", "非法来源", 0);
            return;
        }
        $user = $login_user = $this->get_login_user();
        if(empty($user)){
            $this->ajaxReturn("", "您尚未登录", 0);
            return;
        }
        $userid = $this->_get('userid');
        if(is_numeric($userid)){
            $user = D('User')->find($userid);
            if(empty($user)){
                $this->ajaxReturn("", "用户不存在！404", 0);
                return;
            }
        }
        $map['userid']=$user['userid'];
        $storys = D('Story')->where($map)->select();
        //dump($storys); return;
        /*
        if(empty($storys)){
            $this->ajaxReturn(" ", "用户尚未写故事！", 1);
            return;
        }*/
        $this->assign('storys', $storys);
        $this->assign('user', $user);
        $this->assign('login_user', $login_user);
        $tpl = $this->fetch();
        $this->ajaxReturn($tpl, "加载成功", 1);
        return;
    }

    /**
     +----------------------------------------------------------
     * 查询个人证书
     +----------------------------------------------------------
     */
    function ajax_certify(){
        if(!$this->isAjax()){
            $this->ajaxReturn("123", "非法来源", 0);
        }
         $this->ajaxReturn("456", "非法来源", 1);
    }
    
     /**
     +----------------------------------------------------------
     * 恋人
     +----------------------------------------------------------
     */
    function ajax_lover(){
        if(!$this->isAjax()){
            $this->ajaxReturn("123", "非法来源", 0);
        }
         $this->ajaxReturn("456", "非法来源", 1);
    }
    
    
    /**
     +----------------------------------------------------------
     * 好友印象
     +----------------------------------------------------------
     */
    function ajax_impress(){
        if(!$this->isAjax()){
            $this->ajaxReturn("123", "非法来源", 0);
        }
        $this->ajaxReturn("456", "非法来源", 1);
    }
    
}

?>