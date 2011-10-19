<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageTable.aspx.cs" Inherits="Jxmstc.Sop.DbClient.Web.BaseUnit.ManageTable" %>

<%@ Register Assembly="Trirand.Web" Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/grid.locale-en.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.jqGrid.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:JQGrid ID="Jqgrid1" runat="server" onrowadding="Jqgrid1_RowAdding" 
            onrowdeleting="Jqgrid1_RowDeleting" onrowediting="Jqgrid1_RowEditing">
            <Columns>
                <cc1:JQGridColumn DataField="Name" Editable="True" HeaderText="列名" />
                <cc1:JQGridColumn DataField="Typestring" Editable="True" HeaderText="数据类型" />
                <cc1:JQGridColumn DataField="Isnullable" Editable="True" EditType="CheckBox" HeaderText="允许空"/>
            </Columns>
            <ToolBarSettings ShowAddButton="True" ShowDeleteButton="True" 
                ShowEditButton="True" />
<SortSettings InitialSortColumn=""></SortSettings>
        </cc1:JQGrid>
    </div>
    </form>
</body>
</html>
