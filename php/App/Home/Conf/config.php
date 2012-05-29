<?php

$siteconfig	=	require './site.config.php';
$appconfig = array(
	//'配置项'=>'配置值'
	
	//系统相关
	'SHOW_PAGE_TRACE'       => false, 		//开启调试=true
	'DEFAULT_CHARSET'		=> 'utf-8',
	'DEFAULT_THEME'			=> 'default',	//默认模板主题
	'URL_CASE_INSENSITIVE' 	=> true,		//Url不区分分大小写=true，区分=false

);

return array_merge($siteconfig,$appconfig);
?>