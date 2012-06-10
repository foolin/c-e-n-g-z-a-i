<?php
/**
 +------------------------------------------------------------------------------
 * PublicAction 公共方法
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-09 20:14:11
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class PublicAction extends BaseAction {
	
    /**
     +----------------------------------------------------------
     * 验证码
     +----------------------------------------------------------
     */
	function verify(){
		import("ORG.Util.Image");
		Image::buildImageVerify();
	}

    /**
     +----------------------------------------------------------
     * 取用户卡信息
     +----------------------------------------------------------
     */
    function ajaxUserCard(){
        if(!$this->isAjax()){
            $this->ajaxReturn('', '非法操作', 0);
            return;
        }
        $login_user = $this->getLoginUser();
        if(empty($login_user)){
            $this->ajaxReturn('', '您尚未登录', 0);
            return;
        }
        $this->assign('login_user', $login_user);
        $userid = $this->_get('userid');
        if(empty($userid) || !is_numeric($userid)){
            $this->ajaxReturn('', '参数错误', 0);
            return;
        }
        $user = M('User')->find($userid);
        if(empty($user)){
            $this->ajaxReturn('', '用户不存在！', 0);
            return;
        }
        $this->assign('user', $user);
        $content = $this->fetch();
        $this->ajaxReturn($content, '加载成功', 1);
        return;
    }
}

?>