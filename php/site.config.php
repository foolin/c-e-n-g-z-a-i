<?php
return array(
	//'配置项'=>'配置值'
	
    
    //网站相关
    'SITE_NAME'             => 'MVC学习',
    'SITE_SLOGAN'           => 'PHP,MVC学习',
    'SITE_DOMAIN'           => 'www.mvc.com',
    'SITE_HOST'             => 'http://test.cengzai.com',
    'SITE_KEYWORDS'         => '曾在,真爱,恋爱',
    'SITE_DESCRIPTION'      => '曾在网是一个真实恋爱社区，致力于情侣或夫妻之间交流，基于朋友之间的介绍或者追求的情感社区！',
    
    'DEFAULT_MODULE'        => 'Home',

    /* 数据库设置 */
    'DB_TYPE'               => 'mysql',     // 数据库类型
	'DB_HOST'               => 'localhost', // 服务器地址
	'DB_NAME'               => 'cengzai',          // 数据库名
	'DB_USER'               => 'root',      // 用户名
	'DB_PWD'                => 'lfl123',          // 密码
	'DB_PORT'               => '3306',        // 端口
	'DB_PREFIX'             => 't_',    // 数据库表前缀


    //安全相关
    'AUTH_DES_KEY'              => 'CENGZAI_AUTH_1314',

    /* 发送邮件配置 */
    'MAIL_ADDRESS'          => 'no-reply@cengzai.com', // 邮箱地址
    'MAIL_SMTP'             => 'smtp.exmail.qq.com', // 邮箱SMTP服务器
    'MAIL_LOGINNAME'        => 'no-reply@cengzai.com', // 邮箱登录帐号
    'MAIL_PASSWORD'         => 'y4yhl9t', // 邮箱密码

);
?>