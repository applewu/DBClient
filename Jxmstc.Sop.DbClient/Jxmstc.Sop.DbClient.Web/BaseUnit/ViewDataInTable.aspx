<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewDataInTable.aspx.cs" Inherits="Jxmstc.Sop.DbClient.Web.BaseUnit.ViewDataInTable" %>

<%@ Register Assembly="Trirand.Web" Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<cc1:JQGrid ID="Jqgrid1" runat="server">
        </cc1:JQGrid>--%>
        <asp:Label ID="lblMsg" runat="server" Text=" "></asp:Label>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            ForeColor="Black" GridLines="Vertical">
            <RowStyle BackColor="#F7F7DE" />
            <FooterStyle BackColor="#CCCC99" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
