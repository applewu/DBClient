<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Jxmstc.Sop.DbClient.Web.About" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>关于</title>
    <style type="text/css">
        li
        {
            color: #808080;
            padding: 0px 5px 0px 0px;
            margin: 5px 0px 5px 15px;
        }
        ul
        {
            list-style-type: circle;
            list-style-position: outside;
            list-style-image: none;
        }
        body
        {
            background-image: none;
            background-repeat: no-repeat;
            color: #D9E3CB;
        }
        #head
        {
            background-image: url('css/img.jpg');
            background-repeat: no-repeat;
            height: 125px;
        }
        #content
        {
            margin: 0px auto auto auto;
            background-image: url('image/content.jpg');
            background-repeat: inherit;
        }
        h3
        {
            color: #8B0000;
        }
        #head h1
        {
            color: #000000;
            font-weight: bold;
            font-size: 18pt;
            line-height: 25px;
            overflow: hidden;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="head"><br /><br /><br /><br /><center ><h1>About</h1></center>
 </div> 
    
    <div id ="content"><h3 >
    Thank you for using DbClient!</h3>
    <ul>
    <li>支持SQL Server2005、SQL Server2008、MySQL数据库</li> 
    <li>显示数据库、表、列、触发器、存储过程、视图基本信息</li>
    <li>查看表数据</li>
    <li>对象基本的新增、删除、重命名操作</li>
    <li>T-SQL语法高亮显示</li>
    <li>执行SQL语句</li> 
    <li>查询结果导出到Excel</li>
    <li>数据库备份到本地</li>
    </ul> 
    </div>
    </form>
</body>
</html>
