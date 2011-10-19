using System;
using System.Data;

using Jxmstc.Sop.DbClient.Model;


namespace Jxmstc.Sop.DbClient.Web.UserControl
{
    public partial class List : System.Web.UI.UserControl
    {
        //static  SqlConnection sqlCon;
        //static MySqlConnection mysqlCon;

        public static DataSet dsAllNodes = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            AjaxPro.Utility.RegisterTypeForAjax(typeof(List));
            CreateNodes();
        }

        #region 创建节点

        private DataTable CreateStructure()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("CategoryID", typeof(int)));
            dt.Columns.Add(new DataColumn("CategoryName", typeof(string)));
            dt.Columns.Add(new DataColumn("FatherID", typeof(string)));
            dt.Columns.Add(new DataColumn("IsChild", typeof(bool)));
            return dt;
        }

        public void CreateNodes()
        {
            DataTable dt = this.CreateStructure();

            DataRow drNew = dt.NewRow();
            drNew["CategoryID"] = 1;
            drNew["CategoryName"] = "服务器";
            drNew["FatherID"] = 0;
            dt.Rows.Add(drNew);

            drNew = dt.NewRow();
            drNew["CategoryID"] = 2;
            drNew["CategoryName"] = "数据库";
            drNew["FatherID"] = 1;
            dt.Rows.Add(drNew);

           DataTable dtNodes=this.Nodes ().Tables [0];

           for (int i = 0; i <dtNodes.Rows.Count; i++) {

               drNew = dt.NewRow();
               drNew["CategoryID"] = i + 3;
               drNew["CategoryName"] = dtNodes.Rows[i][0];
               drNew["FatherID"] = 2;
               dt.Rows.Add(drNew);

           }

               dsAllNodes.Tables.Add(dt);
        }

        [AjaxPro.AjaxMethod]
        public bool IsLeaf(int Category)
        {
            foreach (DataRow dr in dsAllNodes.Tables[0].Rows)
            {
                if (dr["FatherID"] != null && int.Parse(dr["FatherID"].ToString()) == Category)
                {
                    return false;
                }
            }
            return true;
        }

        [AjaxPro.AjaxMethod]
        public DataSet GetSubCategory(int CategoryID)
        {
            DataSet ds = new DataSet();
            DataTable dt = this.CreateStructure();
            DataRow[] drSelect = dsAllNodes.Tables[0].Select("FatherID=" + CategoryID.ToString());
            foreach (DataRow drTemp in drSelect)
            {
                DataRow dr = dt.NewRow();
                dr["CategoryID"] = drTemp["CategoryID"];
                dr["CategoryName"] = drTemp["CategoryName"];
                dr["FatherID"] = drTemp["FatherID"];
                dr["IsChild"] = IsLeaf(int.Parse(drTemp["CategoryID"].ToString()));
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            return ds;

        }

        #endregion

         ///<summary>
         ///服务器上数据库信息列表
         ///</summary>
         ///<returns></returns>
        public DataSet Nodes() {

            ConnectionInfo connInfo = new ConnectionInfo();
            connInfo.Server = Session["Server"].ToString();
            connInfo.UserName = Session["UserName"].ToString();
            connInfo.Pwd = Session["Pwd"].ToString();

           //DataSet ds=Provider .findDBManage (Session ["DBType"].ToString (),connInfo );

           //return ds;
            return null;
        }

        }
    }
