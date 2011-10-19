<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Jxmstc.Sop.DbClient.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登陆</title>
    <style type="text/css">
        body
        {
            font-family: Verdana, Arial, SunSans-Regular, Sans-Serif;
            font-size: 11px;
            background-color: #E7E7E7;
            background-image: url('body.jpg');
            background-repeat: repeat;
            background-attachment: scroll;
            background-position: left top;
            margin: 0px;
            padding: 0px;
        }
        #top
        {
            color: #FFFFFF;
            padding: 0px;
            margin: 0px;
            height: 50px;
        }
        #banner
        {
            background-image: url('css/head.jpg');
            background-repeat: no-repeat;
            background-attachment: scroll;
            background-color: #000000;
            color: #000000;
            height: 200px;
            width: 800px;
            margin: 0px;
        }
        #banner h2
        {
            font-size: 29px;
            color: #000000;
            font-weight: bold;
            font-family: "Trebuchet MS";
            margin: 0px;
            padding-top: 0px;
            padding-right: 130px;
        }
    </style>
    <!--Cookies相关操作-->

    <script language="javascript" type="text/javascript">

        //    var acookie=document .cookie .split (";");

        function getCookie(name) {

            //-----法一：在火狐下时常出问题----

            //        for(var i=0;i<acookie .length;i++){
            //        
            //            var arr=acookie [i].split("=");
            //            
            //            if(name==arr[0]){
            //            
            //                if(arr.length>1)
            //                
            //                    return unescape(arr[1]);
            //                else
            //                
            //                    return ""; 
            //        }
            //    
            //    }

            //    return "";
            //-----------------

            var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
            if (arr != null) return unescape(arr[2]); return null;

        }

        //alert(getCookie("123"));

        function getValue() {

            var username = document.getElementById("txtUserName").value;
            var server = document.getElementById("txtServer").value;

            var name = server + "_" + username;

            var cookieValue = getCookie(name);

            //        if(cookieValue ==null ){
            //        
            //            document .getElementById ("txtPwd").value="";
            //        }else {

            document.getElementById("txtPwd").value = cookieValue;
            //        }


            //        alert(name);
            //          alert (cookieValue );


        }

    
    </script>

</head>
<body>
    <form runat="server">
    <center>
        <div id="top">
        </div>
        <div id="banner">
            <br />
            <br />
            <h2>
                数据库客户端</h2>
            <h3>
                --GET ANOTHER WAY</h3>
        </div>
    </center>
    <br />
    <br />
    <br />
    <div>
        <center>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="数据库类型:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDBType" runat="server">
                            <asp:ListItem Value="SQL Server2005/2008"></asp:ListItem>
                            <asp:ListItem Value="MySql"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="服务器名称:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtServer" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtServer"
                            Display="Dynamic" ErrorMessage="必填!"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="用户名:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName"
                            Display="Dynamic" ErrorMessage="必填!"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="密码:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                    <asp:Label ID="lblMessage" runat="server" Text=" "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="ChkPwd" runat="server" Text="记住密码" />
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="登陆" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnClear" runat="server" Text="重置" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
