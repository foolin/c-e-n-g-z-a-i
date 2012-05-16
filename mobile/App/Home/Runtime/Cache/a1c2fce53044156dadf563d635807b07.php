<?php if (!defined('THINK_PATH')) exit();?><div class="container bg-white">
<section class="padding">

    <div class="page-header"> 
        <h1>新用户注册</h1> 
    </div> 
    
    <div class="register-form">
        <form action="<?php echo U('User/register');?>" method="post">
        	<p>
                <label for="code">邀请码： </label>
                <input id="code" name="code" type="text" value="" />
                <span validation-for="code" class="validation-valid"></span> 
                <span tips-for="code" class="muted">请输入朋友的邀请码或者<a class="btn btn-primary" href="<?php echo U('User/applyInvite');?>">申请邀请码</a></span>
            </p>
            <p>
                <label for="email">邮 箱： </label>
                <input id="email" name="email" type="text" value="" />
                <span class="validation-valid" validation-for="email"></span> 
                <span tips-for="email" class="muted">请真实填写，用来登录和找回密码，不可修改</span>
            </p>
            <p>
                <label for="password">密  码： </label>
                <input id="password" name="password" type="password" value="" />
                <span class="validation-valid" validation-for="password"></span> 
                <span tips-for="password" class="muted">请输入6~16位之间的密码</span> 
            </p>
            <p>
                <label for="name">真实姓名： </label>
                <input id="name" name="name" type="text" value="" />
                <span class="validation-valid" validation-for="name"></span> 
                <span tips-for="name" class="muted">请真实填写，方便朋友查找，不可更改</span> 
            </p> 
            <p>
                <label for="sex">性 别： </label>
                <select name="sex" id="sex">
                	<option value="1">帅哥</option>
                    <option value="1">美女</option>
                </select>
                <span class="validation-valid" validation-for="sex"></span> 
                <span class="muted">请真实填写，不可更改</span>
            </p>
            <p>
                <label for="verify">验证码： </label>
                <input id="verify" name="verify" type="text" value="" />
                <img src="<?php echo U('Public/verify');?>" id="imgVerify"  alt="刷新验证码" style="cursor:pointer;" onclick="refreshVerify('imgVerify')"  />
                <span class="validation-valid" validation-for="verify"></span> 
                <span tips-for="verify" class="muted">请输入验证码</span>
            </p>
            <p class="btn-group">
                <input type="submit" class="btn btn-primary" value="立刻注册" />
                <a href="<?php echo U('User/login');?>" class = "btn btn-danger">已经注册？</a>
            </p>
            
        </form>
    </div>
    
</section>
</div>
<script type="text/javascript">
$(function(){
	$("#code").focus(function(){
		valid('code');
	}).blur(function(){
		var code = $('#code').val();
		if(code == ''){
			error('code', '请输入邀请码');
		}
		else{
			$.ajax({
				type: "POST",
				url: "<?php echo U('User/ajaxValidateCode');?>",
				data: "code=" + code,
				success: function(msg){
					var obj = $.parseJSON(msg);
					if(obj.status == "success"){
						success('code', obj.info);
					}
					else{
						error('code', obj.info);
					}
			   }
			});
		}
	});
	
	$("#email").focus(function(){
		valid('email');
	}).blur(function(){
		var email = $(this).val();
		if(email==''){
			error('email','邮箱不能为空');
			return;
		}
		var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
        if(reg.test(email)){
			$.ajax({
				type: "POST",
				url: "<?php echo U('User/ajaxValidateEmail');?>",
				data: "email=" + email,
				success: function(msg){
					var obj = $.parseJSON(msg);
					if(obj.status == "success"){
						success('email', obj.info);
					}
					else{
						error('email', obj.info);
					}
			   }
			});
		}
		else{
			error('email','邮箱不合法！');
		}
	});
	
	//密码
	$("#password").focus(function(){
		valid('password');
	}).blur(function(){
		var password = $(this).val();
		if(password==''){
			error('password','密码不能为空');
			return;
		}
		if(password.length < 6){
			error('password','密码不能少于6位');
			return;
		}
		if(password.length > 16){
			error('password','密码不能大于16位');
			return;
		}
		success('password','密码合法');
	});
	
	//姓名
	$("#name").focus(function(){
		valid('name');
	}).blur(function(){
		var name = $(this).val();
		if(name==''){
			error('name','姓名不能为空');
			return;
		}
		if(name.length < 2){
			error('name','姓名不能少于2位');
			return;
		}
		if(name.length > 10){
			error('name','姓名不能超过10个字符');
			return;
		}
		success('name','姓名正确');
	});
	
	//验证码
	$("#verify").focus(function(){
		valid('verify');
	}).blur(function(){
		var verify = $(this).val();
		if(verify==''){
			error('verify','验证码不能为空');
			return;
		}
		success('verify','输入正确');
	});
	
});

//刷新
function refreshVerify(id) {
	var img = document.getElementById(id);
	img.src = "<?php echo U('Public/verify');?>?t=" + new Date().getTime();
}

function onSubmit(){
	
}

</script>