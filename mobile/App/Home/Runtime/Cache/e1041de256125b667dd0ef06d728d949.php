<?php if (!defined('THINK_PATH')) exit();?><!DOCTYPE html>
<html>
<head>
    <title><?php echo ($title); ?> - <?php echo C('SITE_NAME');?></title>
    <meta name="keywords" content="<?php echo C('SITE_KEYWORDS');?>" />
    <meta name="description" content="<?php echo C('SITE_DESCRIPTION');?>" />
    <meta property="qc:admins" content="1541240606635672116375" />
    <link href="__PUBLIC__/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="__PUBLIC__/css/style.css" rel="stylesheet" type="text/css" />
    
    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="__PUBLIC__/js/html5.js" type="text/javascript"></script>
    <![endif]-->
    <script src="__PUBLIC__/js/jquery.min.js" type="text/javascript"></script>
    <script src="__PUBLIC__/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="__PUBLIC__/js/mvc/MicrosoftAjax.js" type="text/javascript"></script> 
    <script src="__PUBLIC__/js/mvc/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="__PUBLIC__/js/mvc/jquery.unobtrusive-ajax.min.js" type="text/javascript"></script>
    <link href="__PUBLIC__/js/dialog/skins/twitter.css" rel="stylesheet" type="text/css" />
    <script src="__PUBLIC__/js/dialog/jquery.artDialog.min.js" type="text/javascript"></script>
    <script src="__PUBLIC__/js/dialog/artDialog.plugins.min.js" type="text/javascript"></script>
    <script src="__PUBLIC__/js/core.js" type="text/javascript"></script>
    <link rel="shortcut icon" href="__PUBLIC__/img/favicon.ico")">
    <header class="navbar navbar-fixed-top">
	<div class="navbar-inner">
	<div class="container">
		<a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
		<span class="icon-bar"></span>
		<span class="icon-bar"></span>
		<span class="icon-bar"></span>
		</a>
		<a class="brand logo" href="@ViewBag.SiteHost">@ViewBag.SiteName</a>
		<div class="nav-collapse">
        
		<?php if(empty($user)): ?><ul class="nav">
                <li><a href="<?php echo U('User/login');?>">登录</a></li>
            </ul>
            <p class="navbar-text pull-right">还有没有注册？ <a href="<?php echo U('User/register');?>">立即注册</a></p>
        <?php else: ?>
		    <ul class="nav">
			    <li class="active"><a href="<?php echo U('Home/index');?>">首页</a></li>
                <li><a href="<?php echo U('Blog/'.$user['userid']);?>">我的主页</a></li>
                <!--<li><a href="<?php echo U('Twitter/index');?>">唠叨</a></li>-->
                <li><a href="<?php echo U('Love/index');?>">情侣<sup id="loverNewCount" class="label label-important hide">0</sup></a></li>
			    <li><a href="<?php echo U('Friend/index');?>">好友</a></li>
		    </ul>
            <!--<form class="navbar-form pull-left" action="">-->
            <form method="get" action="<?php echo U('Friend/find');?>" class="navbar-form pull-left">
                <div class="input-append">
                    <input type="text" name="keyword" class="span2" id="appendedPrependedInput" size="16"  placeholder="搜索感兴趣的人"><input type="submit" class="btn" value="搜索" />
                </div>
            </form>
            <!--</form>-->
            <ul class="nav pull-right nav-mini-link">
                <li class="dropdown"> 
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown"><?php echo ($user['name']); ?> <b class="caret"></b></a> 
                    <ul class="dropdown-menu"> 
                    <li><a href="<?php echo U('Settings/avatar');?>">上传头像</a></li> 
                    <li><a href="<?php echo U('Settings/profile');?>">完善资料</a></li> 
                    <li><a href="<?php echo U('Settings/password');?>">修改密码</a></li> 
                    <li class="divider"></li> 
                    <li><a href="<?php echo U('User/logout');?>">退出登录</a></li> 
                    </ul> 
                </li>
                <li class="divider-vertical"></li> 
                <li><a href="<?php echo U('Inbox/unread');?>">私信<sup id="inboxNewCount" class="label label-success hide">0</sup></a></li>
                <li><a href="<?php echo U('Settings/avatar');?>">邀请</a></li> 
                <li><a href="<?php echo U('Settings/logout');?>">退出</a></li>
            </ul><?php endif; ?>
        
		</div><!--/.nav-collapse -->

	</div>
	</div>
</header>
<!-- navbar 结束 -->
</head>

<body>