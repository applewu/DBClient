<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updateColumn.aspx.cs" Inherits="Jxmstc.Sop.DbClient.Web.BaseUnit.updateColumn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div><table>
    <tr><td>
     <asp:Label ID="Label1" runat="server" Text="列名"></asp:Label> 
    
    </td><td>
        <asp:TextBox ID="txbName" runat="server"></asp:TextBox></td></tr>
    <tr><td>
        <asp:Label ID="Label2" runat="server" Text="数据类型"></asp:Label></td>
        <td>
            <asp:TextBox ID="txbTypestring" runat="server"></asp:TextBox></td></tr>
            <tr><td>
                <asp:Label ID="Label3" runat="server" Text="允许空"></asp:Label></td>
                <td>
                    <asp:CheckBox ID="chkIsnullable" runat="server" /> </td></tr>
    </table>
       
    </div>
    </form>
</body>
</html>
