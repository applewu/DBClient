<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Jxmstc.Sop.DbClient.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据库客户端主页</title>

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.layout.js" type="text/javascript"></script>

    <!--jQuery基本布局和样式-->

    <script type="text/javascript">
        $(document).ready(function() {
            $('body').layout({ applyDefaultStyles: true });
        });
    </script>

    <style type="text/css">
        #Iframe1
        {
            height: 385px;
            width: 183px;
        }
        .ui-layout-south
        {
            font-size: 12px;
        }
        .ui-layout-north
        {
            background-image: url('css/img.jpg') !important;
            background-repeat: no-repeat !important;
            height: 122px !important;
            width: 711px !important;
        }
    </style>
    <!--备份数据库事件处理-->

    <script type="text/javascript">
        function input() {

            value = prompt("输入备份的数据库名称");

            document.getElementById("Hidden1").value = value;

        }

        function clearTableData() {

            tableName = prompt("输入待清空数据的表名:");

            Jxmstc.Sop.DbClient.Web._Default.clearData(tableName);

            if (tableName != null) {

                alert('对表' + tableName + '数据清空完毕');
            }

        }
    </script>

    <!--鱼眼导航的样式和脚本-->
    <%-- <style>
        html, body
        {
            height: 100%;
        }
        img
        {
            border: none;
        }
        #fx_fishEye
        {
        	padding-left:280px !important;
             width:100%  !important;
             overflow:hidden  !important;
        }
        #fx_fishEye a
        {
          
            display: block;
            float: left;
            text-decoration: none;
            width: 100px;
         
        }
        #fx_fishEye a:hover
        {
            cursor: pointer;
        }
        #fx_fishEye img
        {
     
        }
        #fx_fishEye a:hover span
        {
            visibility: visible;
        }

        #loading
        {
            font-family: Arial;
            font-size: 12px;
            color: #fff;
            background: #009900;
            padding: 15px;
        }
    </style>--%>
    <%-- <script>
        var Pro = {
            fixPNG: function(blankGif, range) { // For IE6.0
                if (!(window.ActiveXObject && (!window.XMLHttpRequest))) return this;
                var images = document.body.getElementsByTagName('img'), png;
                for (var i = 0; i < images.length; ++i) {
                    var img = images[i];
                    png = img.src;
                    //if(!/\.png\s*$/i.test(png))return;
                    img.src = blankGif || 'b.gif';
                    img.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='"
				+ png + "',sizingMethod='scale)";
                };
            },
            doCenter: function() {
                var win_w = document.body.clientWidth;
                var l = Math.round((win_w - this.width) / 2, 10);
                this.box.style.left = l + 'px';
            },
            doSize: function(_x, _y) {
                if (_x) {
                    var x = _x - this.box.offsetLeft, c = document.body.clientHeight;
                    var z = 220 - ((_y > (c - 100)) ? 100 : (c - _y));
                    if (_y < (c - 180)) _x = false;
                };
                var unit = 180 / Math.min(500, this.box.offsetWidth);
                var size = function(_x, _w) {
                    var A = Math.round(90 - (x - _x - _w + 10) * unit);
                    A = Math.max(0, Math.min(180, A)) / 180 * Math.PI;
                    return Math.max(Math.round(Math.sin(A) * z), this.min);
                };
                for (var i = 0, left = 0, prev = 0; i < this.icos.length; ++i) {
                    var _1 = this.icos[i];
                    _1.style.left = left + 'px';
                    _1.style.width = (_x ? size.call(this, left, prev * 0.6) : this.min) + 'px';
                    prev = _1.offsetWidth;
                    left += prev;
                };
                this.box.style.width = left + 'px';
                this.width = left;
            },
            init: function(ini) {
                var pro = this;
                this.min = ini.min || 36;
                this.box = document.getElementById(ini.box);
                this.icos = this.box.getElementsByTagName("A");
                this.fixPNG();
               // document.getElementById("loading").style.display = 'none';
                this.box.style.display = '';
                this.box.style.height = this.min + 10 + 'px';
                this.doSize();
                this.doCenter();
                document.onmousemove = function(e) {
                    clearTimeout(pro.fxTimer);
                    var e = e || event, x = e.clientX, y = e.clientY;
                    pro.fxTimer = setTimeout(function() { pro.doSize(x, y) }, 3);
                };
            }
        };
        window.onload = function() { Pro.init({ box: 'fx_fishEye' }) };
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div class="ui-layout-north" id="fx_fishEye" style="display: none">
        <input id="Hidden1" type="hidden" runat="server" />
        <asp:ImageButton ID="btnBackup" runat="server" ToolTip="备份数据库到本地" ImageUrl="~/image/FishMenu/4.png"
            OnClientClick="input()" OnClick="btnBackup_Click" />
        <%--   <asp:Button ID="Button1" runat="server" Text="清空表数据" />--%>
        <a href="#" title="" onclick="clearTableData()">
            <img src="image/FishMenu/7.png" title="清空表数据" /></a>
        <%--<a href="javascript:void(0)"
                title="">
                <img src="image/FishMenu/7.png" /><span>菜单7</span></a> <a href="javascript:void(0)"
                    title="">
                    <img src="image/FishMenu/8.png" /><span>菜单8</span></a> <a href="javascript:void(0)"
                        title="">
                        <img src="image/FishMenu/9.png" /><span>菜单9</span></a>--%>
    </div>
    <div class="ui-layout-west">
        <iframe id="Iframe1" frameborder="0" src="LeftTree.aspx" scrolling="auto"></iframe>
    </div>
    <div class="ui-layout-center">
        <iframe id="mf" style="width: 100%; height: 861px;" frameborder="0" src="execSql.aspx"
            scrolling="auto"></iframe>
    </div>
    <div id="south" class="ui-layout-south">
        <center>
            ©2009.11 籽藤<br />
            Design - Apple.Wu</center>
    </div>
    </form>
</body>
</html>
