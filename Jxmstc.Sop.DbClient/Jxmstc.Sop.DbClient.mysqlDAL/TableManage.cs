using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Jxmstc.Sop.DbClient.IDAL;

namespace Jxmstc.Sop.DbClient.mysqlDAL
{
    public class TableManage:ITableManage<MySqlConnection>
    {
        Execute exec = new Execute();

        #region ITableManage<MySqlConnection> 成员

        public System.Data.DataTable GetAllTable(MySqlConnection conn, string dbname)
        {


            string sql_select_all_table = "show tables";

            return exec.ExecuteDataTable(conn,sql_select_all_table ,dbname );
        }

        public bool ContainsTable(MySqlConnection conn, string dbname, string tablename)
        {
            throw new NotImplementedException();
        }

        public void DelTable(MySqlConnection conn, string dbname, string name)
        {
            throw new NotImplementedException();
        }

        public void DelTableData(MySqlConnection conn, string dbname, string name)
        {
            throw new NotImplementedException();
        }

        public void RenameTable(MySqlConnection conn, string dbname, string tablename, string newName)
        {
            throw new NotImplementedException();
        }

        public void AddTable(MySqlConnection conn, string dbname, Jxmstc.Sop.DbClient.Model.TableInfo table)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ShowTableData(MySqlConnection conn, string dbname, string table)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
