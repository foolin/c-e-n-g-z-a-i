<?php
/**
 +------------------------------------------------------------------------------
 * UserAction 用户相关管理和操作
 +------------------------------------------------------------------------------
 * @author    foolin <foolin@126.com>
 * @version   2012-05-09 20:14:11
 * Copyright (c) 2012 cengzai.com All rights reserved.
 +------------------------------------------------------------------------------
 */
class UserAction extends BaseAction {
	
    /**
     +----------------------------------------------------------
     * 用户登录界面
     +----------------------------------------------------------
     */
	function login(){
		$this->tpl('登录');
	}
	
	 /**
     +----------------------------------------------------------
     * 用户登录界面
     +----------------------------------------------------------
     */
	function ajaxLogin(){
		
	}
	
	
	/**
     +----------------------------------------------------------
     * 申请邀请码
     +----------------------------------------------------------
     */
	function applyInvite(){
	}
	
	
	
	/**
     +----------------------------------------------------------
     * 用户注册页面
     +----------------------------------------------------------
     */
	function register(){
		//是否为post提交
		if($this->isPost()){
			//验证码
			if($this->_session('verify') != md5($this->_post('verify'))){
				$this->error('注册失败！验证码不正确');
				return;
			}
			
			$dbUser = D('User');
			$data = $dbUser->create();	//创建表单
			if(!$data){
				$this->error('注册失败！' . $dbUser->getError());
				return;
			}
			
			//判断邀请码是否正确
			$dbInvite = M('Invite');
			$code = $this->_post('code');
			if(empty($code)){
				$this->error('请输入邀请码！');
				return;
			}
			$invite = $dbInvite->getByCode($code);
			if(empty($invite) || $invite['state'] != 1){
				$this->error('邀请码不存在或者已失效！');
				return;
			}
			
			//初始化数据
			$data['avatar'] = '';
			$data['loveid'] = 0;
			$data['sign'] = C('SITE_SLOGAN');
			$data['intro'] = C('此家伙不懒，但什么都没留下');
			$data['birth'] = null;
			$data['areaid'] = 0;
			$data['company'] = '';
			$data['mobile'] = '';
			$data['loginip'] = '';
			$data['logintime'] = null;
			$data['logincount'] = 0;
			$data['credit'] = 5;	//赠送5个积分
			$data['money'] = 0;
			$data['vip'] = 0;
			$data['privacy'] = 0;
			$data['config'] = '';
			$data['oauthid'] = 0;
			
			//添加到数据库
			if(!$dbUser->add()){
				$this->error('注册失败！' . $dbUser->getError());
				return;
			}
			$dbInvite->where("code='$code'")->setField('state', -1);

			//发邮件
			try{
				$findPasswordUrl = U('User/findPassword','',true, false,true);
				$activeUrl = U('User/active', 'code='. md5($data['regtime']), true, false,true);
				$title = '您注册'. C('SITE_NAME') .'成功，请激活帐号';
				$message = '尊敬的'. $data['name'] .'<br />';
				$message .= '<p>恭喜您在注册'. C('SITE_NAME') .'成功，你注册帐号是：'. $data['email'] .'。</p>';
				$message .= '<p>请您妥善保管您的密码，如果忘记密码，请<a href='. $findPasswordUrl .'>点击这里找回密码</a>。</p>';
				$message .= "<p>您注册的账号需要激活才能正常使用，请尽快激活您的账号。点击这里进行激活，或者复制下面链接到浏览器进行激活：</p>";
				$message .= "<a href='$activeUrl'>$activeUrl</a>";
				$message .= '<br /><br />---------------<br />'. C('SITE_NAME'). '('. C('SITE_DOMAIN') . ')';
				send_mail($data['email'],$title,$message);
			}
			catch(Exception $e){
				$this->assign('waitSecond',30);	//跳转30秒
				$this->success('注册成功，但发送激活邮件失败！请检查邮箱是否正确！', __APP__);
				return;
			}
			$this->assign('waitSecond',30);	//跳转30秒
			$this->success('注册成功，我们发送一封激活邮件到您的邮箱['. $data['email'] .']，请注意查收！', __APP__);
		}
		else{
			$this->tpl('新用户注册');
		}
	}
	
	
	/**
     +----------------------------------------------------------
     * 检查邀请码是否有效
     +----------------------------------------------------------
     */
	function ajaxValidateCode(){
		if($this->isAjax()){
			$code = $this->_post('code');
			if(empty($code)){
				//邀请码不存在
				$this->ajaxReturn('','对不起请输入邀请码！','error');
				return;
			}
			$dbInvite = M('Invite');
			$data = $dbInvite->getByCode($code);
			if(empty($data) || $data['state'] != 1){
				//邀请码不存在
				$this->ajaxReturn('','对不起，邀请码无效或者已过期！','error');
				return;
			}
			else{
				//邀请码存在，正确
				$this->ajaxReturn('','验证通过，邀请码有效。','success');
				return;
			}
		}
	}
	
	/**
     +----------------------------------------------------------
     * 检查邮箱是否注册
     +----------------------------------------------------------
     */
	function ajaxValidateEmail(){
		if($this->isAjax()){
			$email = $this->_post('email');
			if (empty($email) || !ereg("^[-a-zA-Z0-9_\.]+\@([0-9A-Za-z][0-9A-Za-z-]+\.)+[A-Za-z]{2,5}$",$email)){
            	//邮箱不合法
				$this->ajaxReturn('','对不起，该邮箱不合法！','error');
			}
			$dbUser = M('User');
			$data = $dbUser->getByEmail($this->_post('email'));
			if(!empty($data)){
				//邮箱已经被注册
				$this->ajaxReturn('','对不起，该邮箱已经被注册！','error');
			}
			else{
				//邮箱尚未被注册
				$this->ajaxReturn('','恭喜，该邮箱可以注册！','success');
			}
		}
	}
	
	/**
     +----------------------------------------------------------
     * 处理注册页面
     +----------------------------------------------------------
     */
	function ajaxRegister(){
	}
	
	
	/**
     +----------------------------------------------------------
     * 用户注册第二步：完善资料
     +----------------------------------------------------------
     */
	function regStep2(){
	}
	
	
	/**
     +----------------------------------------------------------
     * 用户注册第三步：挑选认识的朋友
     +----------------------------------------------------------
     */
	function regStep3(){
	}
	

}

?>