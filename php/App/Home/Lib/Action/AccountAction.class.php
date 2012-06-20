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
class AccountAction extends BaseAction {
	
    /**
     +----------------------------------------------------------
     * 用户登录界面
     +----------------------------------------------------------
     */
	function login(){
		//判断是否登录，登录则直接跳转
		$login_user = $this->get_login_user();
		if(!empty($login_user)){
			$this->redirect('Home/index');
		}

		//未登录处理
		if($this->isPost()){
			$email = $this->_post('email');
			$password = $this->_post('password');
			if(!is_email($email)){
				$this->error('邮箱不合法！');
				return;
			}
			if(empty($password)){
				$this->error('密码不能为空！');
				return;
			}
			$dbUser = M('User');
			$user = $dbUser->getByEmail($email);
			if(empty($user) || $user['password'] != md5($password)){
				$this->error('邮箱不正确或者密码错误！');
				return;
			}
			if($user['state'] == -1){
				$this->error('用户不存在或者被冻结！');
				return;
			}
			
			$remember = $this->_post('remember');
			if($remember == 1){
				/******* 写入Cookies ********/
				//算法：
				//1.首先得到明文Val：用户ID|时间|注册时间的MD5校验码
				//2.把明文经过DESEncrypt加密得到密文SecrectVal
				//3.把密文SecrectVal写入Cookies
				
				$source_data = $user['userid']. '|' .time(). '|' . md5($user['regtime']);
				$des_data = des_encrypt($source_data, C('AUTH_DES_KEY'));
				cookie('LOGIN_USER', $des_data, 60 * 60 * 24 * 7); 	//一周不用重复登录
			}
			
			//记录并加积分
			$user['loginip'] = get_client_ip();
			$user['logintime'] = date('Y-m-d H:i:s');
			$user['logincount'] = empty($user['logincount']) ? 1 : ($user['logincount']+1); //登录次数+1
			$user['credit'] = empty($user['credit']) ? 1 : ($user['logincount']+1);	//积分+1
			$dbUser->data($user)->save();
			
			//写入session
			session('LOGIN_USER', $user);
			//跳转到首页
			$this->redirect('Home/index');
		}
		else{
			$this->tpl('登录');
			return;
		}
		
	}
	
	/**
     +----------------------------------------------------------
     * 退出登录
     +----------------------------------------------------------
     */
	function logout(){
		session('LOGIN_USER', null);
		cookie('LOGIN_USER',null);
		$this->success('退出登录成功！', U('Account/login'));
	}
	

	
	
	/**
     +----------------------------------------------------------
     * 用户注册页面
     +----------------------------------------------------------
     */
	function register(){
		//判断是否登录，登录则直接跳转
		$login_user = $this->get_login_user();
		if(!empty($login_user)){
			$this->redirect('Home/index');
		}
		
		//是否为post提交g
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
				$findPasswordUrl = U('Account/findpassword','',true, false,true);
				$activeUrl = U('Account/active', 'email='. $data['email'].'&code='. md5($data['regtime']), true, false,true);
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
	function ajax_validate_code(){
		if($this->isAjax()){
			$code = $this->_post('code');
			if(empty($code)){
				//邀请码不存在
				$this->ajaxReturn('','对不起请输入注册码！','error');
				return;
			}
			$dbInvite = M('Invite');
			$data = $dbInvite->getByCode($code);
			if(empty($data) || $data['state'] != 1){
				//邀请码不存在
				$this->ajaxReturn('','对不起，注册码无效或者已过期！','error');
				return;
			}
			else{
				//邀请码存在，正确
				$this->ajaxReturn('','验证通过，注册码有效。','success');
				return;
			}
		}
	}
	
	/**
     +----------------------------------------------------------
     * 检查邮箱是否注册
     +----------------------------------------------------------
     */
	function ajax_validate_email(){
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
     * 用户注册第二步：完善资料
     +----------------------------------------------------------
     */
	function reg_step2(){
	}
	
	
	/**
     +----------------------------------------------------------
     * 用户注册第三步：挑选认识的朋友
     +----------------------------------------------------------
     */
	function reg_step3(){
	}
	
	
		
	/**
     +----------------------------------------------------------
     * 申请邀请码
     +----------------------------------------------------------
     */
	function apply(){
		if($this->isPost()){
			if(md5($this->_post('verify')) != $this->_session('verify')){
				$this->error('验证码不正确！');
				return;
			}
			$dbInvite = D('Invite');
			$data = $dbInvite->create();
			if(!$data){
				$this->error('申请失败，原因：' . $dbInvite->getError());
				return;
			}
			$dbUser = M('User');
			if($dbUser->getByEmail($data['email'])){
				$this->error('申请失败，该邮箱已经被注册！');
				return;
			}
			if(!$dbInvite->add()){
				$this->error('申请失败，原因：' . $dbInvite->getError());
				return;
			}
			$this->assign('waitSecond',30);	//跳转30秒
			$this->success('申请成功！我们会在48个小时内处理！到时我们会把处理结果发到您的邮箱，请注意查收！', __APP__);
			return;
		}
		else{
			$this->tpl('申请注册码');
		}
	}
	
		
	/**
     +----------------------------------------------------------
     * 激活帐号
     +----------------------------------------------------------
     */
	function active(){
		$code = $this->_get('code');
		$email = $this->_get('email');
		if(empty($code) || empty($email) || !is_email($email)){
			$this->error('对不起，激活失败！参数错误！', U('Home/index'));
		}
		$dbUser = M('User');
		$user = $dbUser->getByEmail($email);
		if(empty($user)){
			$this->error('对不起，激活失败！帐号不存在！', U('Home/index'));
		}
		if($user['state'] < 0){
			$this->error('对不起，激活失败！帐号不存在或者已经被冻结！', U('Home/index'));
		}
		if($user['state'] > 0){
			$this->error('您的帐号已经激活，无重新需激活！', U('Home/index'));
		}
		if($dbUser->where($user['userid'])->setField('state','1')){
			$this->error('帐号激活成功！', U('Home/index'));
		}
		else{
			$this->error('帐号激活失败！原因:' . $dbUser->getError(), U('Home/index'));
		}
	}
	
	
	function find_password(){
		$this->tpl('找回密码');
	}
	
	
	function resend_active(){
		$this->tpl('模板');
	}
	
}

?>