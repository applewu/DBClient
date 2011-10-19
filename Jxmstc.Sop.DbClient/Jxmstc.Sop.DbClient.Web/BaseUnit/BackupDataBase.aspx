<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BackupDataBase.aspx.cs" Inherits="Jxmstc.Sop.DbClient.Web.BaseUnit.BackupDataBase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    您是否要备份数据库[<asp:Label ID="lblDBName" runat="server" Text=" "></asp:Label>]到本地？
    <br />
        <asp:Button ID="btnBackup" runat="server" Text="确定" onclick="btnBackup_Click" />
    &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="取消" 
            onclick="btnCancel_Click" />
    </div>
    </form>
</body>
</html>
