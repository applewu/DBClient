using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Jxmstc.Sop.DbClient.IDAL;
using System.Data;

namespace Jxmstc.Sop.DbClient.mysqlDAL
{
    class ViewManage:IViewManage<MySqlConnection>
    {
        Execute exec = new Execute();

        #region IViewManage<MySqlConnection> 成员

        public System.Data.DataTable GetAllView(MySqlConnection conn, string dbname)
        {

            string sql__select_all_View = " select table_name as name from information_schema.views where table_schema='{0}'";

            DataTable dt=exec.ExecuteDataTable(conn, string.Format(sql__select_all_View , dbname));

            return dt;
        }

        public bool ContainsView(MySqlConnection conn, string dbname, string viewname)
        {
            throw new NotImplementedException();
        }

        public void DelView(MySqlConnection conn, string dbname, string name)
        {
            throw new NotImplementedException();
        }

        public string ShowView(MySqlConnection conn, string dbname, string name)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
