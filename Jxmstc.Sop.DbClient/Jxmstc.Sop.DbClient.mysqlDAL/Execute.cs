using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Jxmstc.Sop.DbClient.IDAL;
using System.Data;

namespace Jxmstc.Sop.DbClient.mysqlDAL
{
    class Execute:IExecute<MySqlConnection>
    {
        #region IExecute<MySqlConnection> 成员

        public void ExecuteSql(MySqlConnection conn, string sql)
        {
            this.ExecuteSql(conn, sql, "information_schema", 0);
        }

        public void ExecuteSql(MySqlConnection conn, string sql, string dbName)
        {
            this.ExecuteSql(conn, sql, dbName, 0);
        }

        public void ExecuteSql(MySqlConnection conn, string sql, string dbName, int timeout)
        {
            if (conn == null)
            {
                //ConnError();
                return;
            }

            if (conn.Database != dbName)
                conn.ChangeDatabase(dbName);

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            if (timeout > 0) cmd.CommandTimeout = timeout;
            cmd.ExecuteNonQuery();

            if (conn.Database != "information_schema")
                conn.ChangeDatabase("information_schema");
        }

        public System.Data.DataSet ExecuteDataSet(MySqlConnection conn, string sql, string dbName, int timeout)
        {
            if (conn == null)
            {
                return null;
            }

            if (conn.State != ConnectionState.Open)
                throw new Exception("连接未打开！");

            if (conn.Database != dbName)
                conn.ChangeDatabase(dbName);


            MySqlDataAdapter ada = new MySqlDataAdapter(sql, conn);
            if (timeout > 0) ada.SelectCommand.CommandTimeout = timeout;
            DataSet ds = new DataSet();
            ada.Fill(ds);

            if (conn.Database != "information_schema")
                conn.ChangeDatabase("information_schema");

            return ds;
        }

        public System.Data.DataSet ExecuteDataSet(MySqlConnection conn, string sql, string dbName)
        {
            return ExecuteDataSet(conn, sql, dbName, 0);
        }

        public System.Data.DataSet ExecuteDataSet(MySqlConnection conn, string sql)
        {
            return ExecuteDataSet(conn, sql, "information_schema", 0);
        }

        public System.Data.DataTable ExecuteDataTable(MySqlConnection conn, string sql, string dbName, int timeout)
        {
            DataSet ds = ExecuteDataSet(conn, sql, dbName, timeout);

            if (ds.Tables.Count <= 0)
                return null;

            return ds.Tables[0];
        }

        public System.Data.DataTable ExecuteDataTable(MySqlConnection conn, string sql, string dbName)
        {
            return ExecuteDataTable(conn, sql, dbName, 0);
        }

        public System.Data.DataTable ExecuteDataTable(MySqlConnection conn, string sql, int timeout)
        {
            return ExecuteDataTable(conn, sql, "information_schema", timeout);
        }

        public System.Data.DataTable ExecuteDataTable(MySqlConnection conn, string sql)
        {
            return ExecuteDataTable(conn, sql, "information_schema", 0);
        }

        #endregion
    }
}
