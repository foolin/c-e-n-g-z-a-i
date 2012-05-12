<?php if (!defined('THINK_PATH')) exit();?><div class="container bg-white">
<section class="padding">

    <div class="page-header"> 
        <h1>用户登录</h1> 
    </div> 

    <div class="login-form">
        <form action="<?php echo U('User/ajaxLogin');?>" method="post">
            <p>
                <label for="email">邮 箱：</label>
                <input class="input-validation-error span3" placeholder="请输入邮箱" type="text" name="email" id="email" value="" />
                <span class="field-validation-error" data-valmsg-for="email" data-valmsg-replace="true">请输入正确邮箱！</span>
            </p>
            <p>
                <label for="email">邮 箱：</label>
                <input class="input-validation-error span3" placeholder="请输入密码" type="text" name="email" id="email" value="" />
                <span class="field-validation-error" data-valmsg-for="email" data-valmsg-replace="true">请输入邮箱</span>
                <a href="<?php echo U('User/findPassword');?>">忘记密码？</a>
            </p>
                
            <p>
                <label class="checkbox inline"> 
                    <input type="checkbox" name="remember" id="remember" value="1"> 一个月内自动登录 <span class="muted">（使用公共电脑切勿选择此项）</span>
                </label> 
                
            </p>
            <p class="btn-group">
                <input type="submit" class="btn btn-primary" value="登录" />
                <a href="<?php echo U('User/register');?>" class = "btn btn-danger">尚未注册？</a>
            </p>
            
        </form>
    </div>
    
</section>
</div>