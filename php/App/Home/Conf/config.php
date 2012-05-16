<?php

$siteconfig	=	require './site.config.php';
$appconfig = array(
	//'配置项'=>'配置值'
	
	//系统相关
	'SHOW_PAGE_TRACE'       => false, 		//开启调试=true
	'DEFAULT_CHARSET'		=> 'utf-8',
	'DEFAULT_THEME'			=> 'default',	//默认模板主题
	'URL_CASE_INSENSITIVE' 	=> true,		//Url不区分分大小写=true，区分=false
	
	//网站相关
	'SITE_NAME'				=> 'MVC学习',
	'SITE_SLOGAN'			=> 'PHP,MVC学习',
	'SITE_DOMAIN'			=> 'www.mvc.com',
	'SITE_HOST'				=> 'http://test.cengzai.com',
	'SITE_KEYWORDS'			=> '曾在,真爱,恋爱',
	'SITE_DESCRIPTION'		=> '曾在网是一个真实恋爱社区，致力于情侣或夫妻之间交流，基于朋友之间的介绍或者追求的情感社区！',
	
	//安全相关
	'AUTH_DES_KEY'				=> 'CENGZAI_AUTH_1314',
	


	/* 发送邮件配置 */
	'MAIL_ADDRESS'			=> 'no-reply@cengzai.com', // 邮箱地址
	'MAIL_SMTP'				=> 'smtp.exmail.qq.com', // 邮箱SMTP服务器
	'MAIL_LOGINNAME'		=> 'no-reply@cengzai.com', // 邮箱登录帐号
	'MAIL_PASSWORD'			=> 'y4yhl9t', // 邮箱密码
);

return array_merge($siteconfig,$appconfig);
?>