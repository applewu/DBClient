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
    /// 
    /// </summary>
    public class ProcedureManage:IProcedureManage<SqlConnection>
    {
        Execute exec = new Execute();

        public DataTable GetAllProcedure(SqlConnection conn, string dbname)
        {
            return exec.ExecuteDataTable(conn, "select [name] from sysobjects where type='P' order by [name]", dbname);
        }

        public bool ContainsProcedure(SqlConnection conn, string dbname, string proname)
        {
            throw new NotImplementedException();
        }

        public void DelProcedure(SqlConnection conn, string dbname, string name)
        {
            dbname = dbname.TrimStart('[').TrimEnd(']');
            exec .ExecuteSql(conn, string.Format("DROP Procedure [{0}]", name), dbname);
        }

        public string ShowProc(SqlConnection conn, string dbname, string name)
        {
            dbname = dbname.TrimStart('[').TrimEnd(']');
            string viewScript = exec.ExecuteDataTable(conn, string.Format("select text from syscomments where id=object_id('{0}')", name), dbname).Rows[0][0].ToString();
            return viewScript;
        }

    }
}
