<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="CengZai.Web._404" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>您访问的页面不存在！</title>
    <style type="text/css">
        .box
        {
        	margin:20px auto;
        	width:600px;
        	border:solid 5px #ccc;
        	background:#e9e9e9;
        }
        .box-inner
        {
        	margin:0;
        	padding:0;
        	background:#fff;
        }
        h1
        {
        	margin:0;
        	padding:0;
        	font-size:16pt;
        	padding:10px 5px;
        	background:#e9e9e9;
        }
        p
        {
        	margin:0;
        	padding:5px;
        	background:#e9e9e9;
        }
        li
        {
        	padding:5px 2px;
        }
    </style>
</head>
<body>
    <div class="box">
    <div class="box-inner"> 
        <h1>很抱歉，您要访问的页面不存在。(404)</h1>
        <ol>
    	    <li>请检查您输入的网址是否正确。</li>
            <li><a href="/">返回网站首页</a></li>
        </ol>
        <p><%=CengZai.Helper.Config.SiteName + "，" + CengZai.Helper.Config.SiteSlogan %></p>
         
    </div>
    </div>
</body>
</html>
