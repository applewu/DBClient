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
    public partial class ViewDataInTable : System.Web.UI.Page
    {
        static  ConnectionInfo connInfo = new ConnectionInfo();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                //Jqgrid1.DataSource = this.GetData();
                //Jqgrid1.DataBind();

                if (this.GetData().Rows.Count > 0)
                {

                    GridView1.DataSource = this.GetData();
                    GridView1.DataBind();
                }
                else {

                    lblMsg.Text = "该表没有数据";
                }

              
            }
        }

        /// <summary>
        /// 获得表中数据
        /// </summary>
        /// <returns></returns>
        private DataTable GetData() {

            Manage manage = new Manage();

            string dbName = Request.QueryString["db"];
            string tableName = Request.QueryString["table"];
            findConn ();

            return manage.ShowTableData(dbName, connInfo, tableName);

            //--测试jqGrid
            //DataTable dt = new DataTable();

            //dt.Columns.Add(new DataColumn("Name", typeof(string)));
            //dt.Columns.Add(new DataColumn("Typestring", typeof(string)));
            //dt.Columns.Add(new DataColumn("Isnullable", typeof(string)));

            //DataRow dr = dt.NewRow();
            //dr["Name"] = "hai";
            //dr["Typestring"] = "int";
            //dr["Isnullable"] = "true";

            //dt.Rows.Add(dr);


            //return dt;
        }

        /// <summary>
        /// 为连接信息的实体对象赋值
        /// </summary>
        private void findConn()
        {

            connInfo.Server = Session["Server"].ToString();
            connInfo.UserName = Session["UserName"].ToString();
            connInfo.Pwd = Session["Pwd"].ToString();

            //connInfo.Server = @"WWW-955D12A1603\WZT2009";
            //connInfo.UserName = "sa";
            //connInfo.Pwd = "Pass@word";

        }
    }
}
