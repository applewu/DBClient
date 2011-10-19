using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Jxmstc.Sop.DbClient.BLL;
using Jxmstc.Sop.DbClient.Model;

namespace Jxmstc.Sop.DbClient.Web.BaseUnit
{
    public partial class ManageTable : System.Web.UI.Page
    {
        static string dbName;
        static string tableName;

        Manage manage = new Manage();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Jqgrid1.DataSource = this.GetData();
                Jqgrid1.DataBind();
            }
        }

        private DataTable GetData()
        {

            if (Request.QueryString["tableName"]!=null )
            {

                dbName = Request.QueryString["dbName"];
                tableName = Request.QueryString["tableName"];
            }

             DataTable dt = Provider.getColumnsToDataTable_Mssql(dbName, tableName);

            return dt;
        }

        public ConnectionInfo getConnInfo()
        {

            ConnectionInfo connInfo = new ConnectionInfo();
            connInfo.Server = Session["Server"].ToString();
            connInfo.UserName = Session["UserName"].ToString();
            connInfo.Pwd = Session["Pwd"].ToString();

            return connInfo;
        }

        //添加列
        protected void Jqgrid1_RowAdding(object sender, Trirand.Web.UI.WebControls.JQGridRowAddEventArgs e)
        {
            ColumnInfo column = new ColumnInfo();

            column .Name  = e.RowData["Name"];
            column .Typestring  = e.RowData["Typestring"];

            if (column .Isnullable  == "on")
            {

                column.Isnullable = "null";
            }
            else
            {

                column.Isnullable = "not null";
            }

            manage.CreateObject(getConnInfo(), dbName, tableName, column);
        }

        //删除列
        protected void Jqgrid1_RowDeleting(object sender, Trirand.Web.UI.WebControls.JQGridRowDeleteEventArgs e)
        {
            string colName = e.RowKey;

            manage.DeleteObject(getConnInfo(), "folder_col", dbName, tableName, colName);

        }

        //编辑列
        protected void Jqgrid1_RowEditing(object sender, Trirand.Web.UI.WebControls.JQGridRowEditEventArgs e)
        {
            ColumnInfo column = new ColumnInfo();
            column.Name = e.RowData["Name"];
            column.Typestring = e.RowData["Typestring"];
            if (e.RowData["Isnullable"] == "on")
            {

                column.Isnullable = "null";
            }
            else {

                column.Isnullable = "not null";
            }

            //若修改了列名
            if (e.RowKey != e.RowData["Name"].ToString ()) {

                manage.RenameObject(getConnInfo(), e.RowKey, e.RowData["Name"], dbName, tableName);
            }

            manage.UpdateColumn(getConnInfo(), dbName, tableName, column);
        }
    }
}
