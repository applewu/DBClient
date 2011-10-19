<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftTree.aspx.cs" ContentType ="GBK" Inherits="Jxmstc.Sop.DbClient.Web.LeftTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>左侧</title>
    <link href="css/Common.css" rel="stylesheet" type="text/css" />
    <link href="JavaScript/tree_component.css" rel="stylesheet" type="text/css" />

    <script src="JavaScript/css.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/tree_component.js" type="text/javascript"></script>

    <script src="JavaScript/Tree.js" type="text/javascript"></script>

    <style type="text/css">
        #left
        {
            padding: 0px;
            margin: 0px;
            width: 200px;
        }
        .tree-default li a, .tree-default li span
        {
            padding: 1px 4px 1px 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="left">
        <ul id="browser">
        </ul>
    </div>
    </form>
</body>
</html>

<script language="javascript" type="text/javascript" charset ="GBK">

    var tree = Jxmstc.Sop.DbClient.Web.LeftTree.CreateTree();


    $("#browser").tree({
        data: {
            type: "json",
            json: eval("(" + tree.value + ")")
        },
        rules: {
            renameable: "all",
            creatable: ["all"],
            deletable: "all"
        }
    })

    $(function() {

        $("#browser").tree({

            callback: {
                appendContextMenuItems: function(t, context) {
                    if (t.settings.ui.context != false) {
                        var m = [
                        //测试订制菜单...            
                        //                        {
                        //                            id: "createDB",
                        //                            label: "createDB",
                        //                            icon: "create.png",

                        //                            //control the display of menuItem
                        //                            visible: function(NODE, TREE_OBJ) {
                        //                                //返回-1 hide
                        //                                //返回 false disable
                        //                                //返回true ,show
                        //                                if (NODE.length != 1) return false;
                        //                                return true;
                        //                            },
                        //                            //定制点击该上下文菜单项所需要的动作
                        //                            action: function(NODE, TREE_OBJ) {
                        //                                // TREE_OBJ.create(false, TREE_OBJ.get_node(NODE[0]));

                        //                            }
                        //                        },
{
id: "manageTable",
label: "修改",
icon: "create.png",

//control the display of menuItem
visible: function(NODE, TREE_OBJ) {
    //返回-1 hide
    //返回 false disable
    //返回true ,show
    if (NODE[0].type == "Table") return true;
    return -1;
},
//定制点击该上下文菜单项所需要的动作
action: function(NODE, TREE_OBJ) {

    var dbName = TREE_OBJ.get_node(NODE)[0].parentNode.parentNode.parentNode.parentNode.id;

    //转到BaseUnit文件夹的ManageTable.aspx
    window.parent.document.getElementById("mf").src = "BaseUnit/ManageTable.aspx?dbName=" + dbName + "&&tableName=" + NODE[0].id;


}
}
// , {
//     id: "addTable",
//     label: "新建表",
//     icon: "create.png",

//     //control the display of menuItem
//     visible: function(NODE, TREE_OBJ) {
//         //返回-1 hide
//         //返回 false disable
//         //返回true ,show
//     if (NODE[0].type == "folder_table") return true;
//         return -1;
//     },
//     //定制点击该上下文菜单项所需要的动作
//     action: function(NODE, TREE_OBJ) {

//         
//         var dbName = TREE_OBJ.get_node(NODE)[0].parentNode.parentNode.id;
//         window.parent.document.getElementById("mf").src = "BaseUnit/AddTable.aspx?db=" + dbName;
//     }
// }

                   , {
                       id: "showTableData",
                       label: "查看表数据",
                       icon: "create.png",

                       //control the display of menuItem
                       visible: function(NODE, TREE_OBJ) {
                           //返回-1 hide
                           //返回 false disable
                           //返回true ,show
                           if (NODE[0].type == "Table") return true;
                           return -1;
                       },
                       //定制点击该上下文菜单项所需要的动作
                       action: function(NODE, TREE_OBJ) {

                           var dbName = TREE_OBJ.get_node(NODE)[0].parentNode.parentNode.parentNode.parentNode.id;
                           window.parent.document.getElementById("mf").src = "BaseUnit/ViewDataInTable.aspx?db=" + dbName + "&&table=" + NODE[0].id;
                       }
                   },


                   {
                       id: "showScript",
                       label: "查看创建脚本",
                       icon: "create.png",

                       //control the display of menuItem
                       visible: function(NODE, TREE_OBJ) {
                           //返回-1 hide
                           //返回 false disable
                           //返回true ,show
                           if ((NODE[0].type == "View") || (NODE[0].type == "Procedure")) return true;
                           return -1;
                       },
                       //定制点击该上下文菜单项所需要的动作
                       action: function(NODE, TREE_OBJ) {

                           var dbName = TREE_OBJ.get_node(NODE)[0].parentNode.parentNode.parentNode.parentNode.id;
                           var script = Jxmstc.Sop.DbClient.Web.LeftTree.ShowScript(NODE[0].id, dbName);

                           window.parent.document.getElementById("mf").src = "execSql.aspx?txtsql1=" + script.value;
                       }
                   }, {
                       id: "reLoad",
                       label: "刷新",
                       icon: "create.png",

                       //control the display of menuItem
                       visible: function(NODE, TREE_OBJ) {
                           //返回-1 hide
                           //返回 false disable
                           //返回true ,show
                           if (NODE[0].type == "DataBase") return true;
                           return -1;
                       },
                       //定制点击该上下文菜单项所需要的动作
                       action: function(NODE, TREE_OBJ) {

                           //刷新整棵树
                           window.parent.frames["I1"].location.reload();
                       }
                   }, {
                       id: "backup",
                       label: "备份",
                       icon: "create.png",

                       //control the display of menuItem
                       visible: function(NODE, TREE_OBJ) {
                           //返回-1 hide
                           //返回 false disable
                           //返回true ,show
                           if (NODE[0].type == "DataBase") return true;
                           return -1;
                       },
                       //定制点击该上下文菜单项所需要的动作
                       action: function(NODE, TREE_OBJ) {

                           //转到BaseUnit文件夹的BackupDataBase.aspx
                           window.parent.document.getElementById("mf").src = "BaseUnit/BackupDataBase.aspx?DBName=" + NODE[0].id;


                       }
                   }

                    ];
                        for (var n = 0; n < m.length; n++) { context[context.length] = m[n]; }
                    }
                },

                //删除节点--实现删除数据库、表、列、视图的功能
                //--触发器、存储过程待实现
                ondelete: function(NODE, TREE_OBJ, rb) {

                    var node = TREE_OBJ.get_node(NODE);
                    // var json = TREE_OBJ.getJSON();

                    var root = new Node();
                    root.attributes.id = "root12";
                    Load(root, eval("(" + tree.value + ")"));

                    //当前节点对象
                    var node1 = findNode(root, NODE.id);
                    //如，某列(Column)是“列文件夹(folder_col)”的子节点
                    //意味着，知道节点在“XX文件夹”下，即知道节点类型
                    var nodeType = node1.parentNode.attributes.id;

                    if (nodeType == "folder_col") {

                        var tableNode = node1.parentNode.parentNode;
                        var dbNode = tableNode.parentNode.parentNode;

                        //Jxmstc.Sop.DbClient.Web.LeftTree.Delete(nodeType, NODE.id, tableNode.attributes.id, dbNode.attributes.id);
                        //不提交到后台 直接转向ManageTable.aspx页面
                        window.parent.document.getElementById("mf").src = "BaseUnit/ManageTable.aspx?dbName=" + dbNode.attributes.id + "&&tableName=" + tableNode.attributes.id;
                    }
                    else if ((nodeType == "folder_table") || (nodeType == "folder_db") || (nodeType == "folder_view")) {

                        var parentNode = node1.parentNode.parentNode;

                        Jxmstc.Sop.DbClient.Web.LeftTree.Delete(nodeType, NODE.id, parentNode.attributes.id);

                        if (nodeType == "folder_db") {

                            //--刷新execSql.aspx
                            window.parent.frames["I2"].location.reload();
                        }
                    }

                },
                //创建节点---
                oncreate: function(NODE, REF_NODE, TYPE, TREE_OBJ, RB) {


                },
                ///重命名节点---实现数据库、表的重命名功能
                onrename: function(NODE, LANG, TREE_OBJ, RB) {

                    var node = TREE_OBJ.get_node(NODE);
                    var newName = node[0].childNodes[0].innerHTML;
                    var oldName = NODE.id;

                    ////父节点的值有问题(json数据在页面加载时绑定，若父节点已重命名，而在json数据中未改变，则导致子节点重命名失败)...
                    ////下一句是可能导致错误的取值方式：
                    //  var parentNodeName=node1.parentNode.parentNode.attributes.id;

                    //-----这里的父节点是当前表所在的数据库名or当前列所在的表名
                    var parentNodeName = $(NODE.parentNode.parentNode.parentNode.parentNode.innerHTML)[0].innerHTML;


                    //根据oldName是否为空 判断是新建后的重命名，还是对已存在对象的重命名
                    if (oldName == "") {

                        var createObjectType = $(NODE.parentNode.parentNode.innerHTML)[0].innerHTML;


                        if (createObjectType == "数据库") {

                            //转到后台方法--添加数据库
                            Jxmstc.Sop.DbClient.Web.LeftTree.Create(newName);
                            //--刷新execSql.aspx
                            window.parent.frames["I2"].location.reload();


                        }
                    } else {

                        var root = new Node();
                        root.attributes.id = "root12";
                        Load(root, eval("(" + tree.value + ")"));

                        //当前节点对象
                        var node1 = findNode(root, NODE.id);
                        var nodeType = node1.attributes.type;

                        if ((nodeType == "DataBase") || (nodeType == "Table")) {

                            Jxmstc.Sop.DbClient.Web.LeftTree.Rename(nodeType, oldName, newName, parentNodeName);

                            if (nodeType == "DataBase") {

                                //--刷新execSql.aspx
                                window.parent.frames["I2"].location.reload();
                            }
                        }

                        //                     else if (nodeType == "Column") {

                        //                        var dbName = node1.parentNode.parentNode.parentNode.parentNode.attributes.id;
                        //                        window.parent.document.getElementById("mf").src = "BaseUnit/updateColumn.aspx?db="+dbName +"&&table="+parentNodeName +"&&data="+node1.data;

                        //                    }


                    }
                }

            }
        })
    })

</script>

