using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Jxmstc.Sop.DbClient.IDAL;

namespace Jxmstc.Sop.DbClient.mysqlDAL
{
    public class ProcedureManage:IProcedureManage<MySqlConnection>
    {
        Execute exec = new Execute();

        #region IProcedureManage<MySqlConnection> 成员

        public System.Data.DataTable GetAllProcedure(MySqlConnection conn, string dbname)
        {
            string sql__select_all_Proc = "select name from mysql.proc where db= '{0}'";

            return exec.ExecuteDataTable(conn, string.Format(sql__select_all_Proc , dbname));
        }

        public bool ContainsProcedure(MySqlConnection conn, string dbname, string proname)
        {
            throw new NotImplementedException();
        }

        public void DelProcedure(MySqlConnection conn, string dbname, string name)
        {
            throw new NotImplementedException();
        }

        public string ShowProc(MySqlConnection conn, string dbname, string name)
        {
            throw new NotImplementedException();

            //select INFO from information_schema.Processlist where DB={0}
        }

        #endregion
    }
}
