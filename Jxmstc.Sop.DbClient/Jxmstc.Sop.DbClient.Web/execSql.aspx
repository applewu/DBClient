<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="execSql.aspx.cs" Inherits="Jxmstc.Sop.DbClient.Web.execSql" %>

<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>无标题页</title>
    <link href="css/Common.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="JavaScript/ui.tabs.css" type="text/css" media="print, projection, screen">

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/ui.core.js" type="text/javascript"></script>

    <script src="JavaScript/ui.tabs.js" type="text/javascript"></script>

   <script src="codePress/codepress.js" type="text/javascript">   
    </script>

    <script language="javascript" type="text/javascript">
        //清空编辑框
        function clear() {
            document.getElementById("txtsql1").value = "";
        }

        //点击执行按钮，将textarea内容赋给隐藏域
        function onsubmit1() {
            document.getElementById("txtsql1").value = txtsql.getCode();
            return true;
        }

        //将隐藏域txtsql1的值赋值给textarea
        window.onload = function() {
            var s = document.getElementById("txtsql1").value;
            if (s != null) {
                for (var i = 0; i < s.length; i++) {
                    document.getElementById("txtsql1").value = document.getElementById("txtsql1").value.replace("wzt", "\n");
                    document.getElementById("txtsql1").value = document.getElementById("txtsql1").value.replace("dbClient", "\"");
                }
                txtsql.setCode(document.getElementById("txtsql1").value);
            }
        }
        $(function() {
            $('#box > ul').tabs();
        });
    </script>

    <style type="text/css">
        .box
        {
            width: 1000px;
            height: 250px;
        }
        .textarea
        {
            width: 1000px;
            height: 250px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="float: left; margin-left: 15px; text-align: left;">
        <table style="text-align: left; float: left; width: 1000px;">
            <tr>
                <td align="left" width="270">
                    当前数据库：
                    <asp:DropDownList ID="ddlDBList" runat="server">
                    </asp:DropDownList>                   
                </td>
                <td align="left" >
                 <asp:ImageButton ID="btnRun" ImageUrl="~/image/sqltools_08.gif" ToolTip="运行" runat="server"
                        OnClientClick="return onsubmit1(); " OnClick="btnRun_Click" Style="height: 15px;
                        width: 16px" />
                    <asp:ImageButton ID="btnClear" runat="server" ToolTip="清空" ImageUrl="~/image/sqltools_03.gif"
                        OnClientClick="return clear();" />
                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/image/btn_table.gif" ToolTip="导出为Excel"
                        OnClick="btnExport_Click" />
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <ajax:AjaxPanel ID="AjaxPanel1" runat="server">
            <textarea name="txtsql" id="txtsql" class="codepress sql textarea"></textarea>
            <br />
            <input type="hidden" id="txtsql1" name="txtsql1"></input>
            <div id="box">
                <ul id="tabMenu">
                    <li><a href="#posts"><span>结果</span></a></li>
                    <li><a href="#comments"><span>消息</span></a></li>
                </ul>
                <div class="box">
                    <div id="posts">
                        <asp:GridView ID="gvResult" runat="server" CellPadding="4" ForeColor="Black" 
                            GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" 
                            BorderStyle="None" BorderWidth="1px">
                            <RowStyle BackColor="#F7F7DE" />
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                    <div id="comments">
                        <asp:Label ID="lblMessage" runat="server" Text=" "></asp:Label>
                    </div>
                </div>
            </div>
        </ajax:AjaxPanel>
    </div>
    </form>
</body>
</html>
