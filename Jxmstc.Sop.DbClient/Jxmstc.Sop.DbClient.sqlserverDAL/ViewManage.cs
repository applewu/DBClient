using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Jxmstc.Sop.DbClient.IDAL;
using System.Data;

namespace Jxmstc.Sop.DbClient.sqlserverDAL
{
    /// <summary>
    /// 视图管理
    /// </summary>
    public class ViewManage:IViewManage<SqlConnection>
    {
        Execute exec = new Execute();

        public DataTable GetAllView(SqlConnection conn, string dbname)
        {
            return exec.ExecuteDataTable(conn
               , "select [name] from sysobjects where type='V' order by [name]"
               , dbname);
        }

        public bool ContainsView(SqlConnection conn, string dbname, string viewname)
        {
            throw new NotImplementedException();
        }

        public void DelView(SqlConnection conn, string dbname, string name)
        {
            dbname = dbname.TrimStart('[').TrimEnd(']');
            exec.ExecuteSql(conn, string.Format("DROP View [{0}]", name), dbname);
        }


        public string ShowView(SqlConnection conn, string dbname, string name)
        {
            dbname = dbname.TrimStart('[').TrimEnd(']');
            string viewScript=exec.ExecuteDataTable(conn, string.Format("select text from syscomments where id=object_id('{0}')", name), dbname).Rows [0][0].ToString ();
            return viewScript;
        }

    }
}
