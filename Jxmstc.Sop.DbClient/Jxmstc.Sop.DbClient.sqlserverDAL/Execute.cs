using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using Jxmstc.Sop .DbClient .IDAL ;
using Jxmstc.Sop.DbClient.Model;

namespace Jxmstc.Sop.DbClient.sqlserverDAL
{
    /// <summary>
    ///  SQLServer2005执行SQL语句相关业务操作
    /// </summary>
    /// <typeparam name="T">连接对象的类型</typeparam>
    public class Execute:IExecute<SqlConnection>
    {
        public void ExecuteSql(SqlConnection conn, string sql)
        {
            this.ExecuteSql(conn, sql, "master", 0);
        }

        public void ExecuteSql(SqlConnection conn, string sql,string dbName) {

            this.ExecuteSql(conn, sql, dbName, 0);
        }

        public void ExecuteSql(SqlConnection conn, string sql, string dbName, int timeout)
        {

            if (conn  == null)
            {
                //ConnError();
                return;
            }

            if (conn.Database != dbName)
                conn.ChangeDatabase(dbName);

            SqlCommand cmd = new SqlCommand(sql, conn);
            if (timeout > 0) cmd.CommandTimeout = timeout;
            cmd.ExecuteNonQuery();

            if (conn.Database != "master")
                conn.ChangeDatabase("master");
        }


        #region ExecuteDataSet成员方法

        public DataSet ExecuteDataSet(SqlConnection conn, string sql, string dbName, int timeout)
        {
            if (conn == null)
            {
                return null;
            }

            if (conn.State != ConnectionState.Open)
                throw new Exception("连接未打开！");

            if (conn.Database != dbName)
                conn.ChangeDatabase(dbName);


            SqlDataAdapter ada = new SqlDataAdapter(sql, conn);
            if (timeout > 0) ada.SelectCommand.CommandTimeout = timeout;
            DataSet ds = new DataSet();
            ada.Fill(ds);

            if (conn.Database != "master")
                conn.ChangeDatabase("master");

            return ds;
        }

        public DataSet ExecuteDataSet(SqlConnection conn, string sql, string dbName)
        {
            return ExecuteDataSet(conn, sql, dbName, 0);
        }

        public DataSet ExecuteDataSet(SqlConnection conn, string sql)
        {
            return ExecuteDataSet(conn, sql, "master", 0);
        }

        #endregion

        #region ExecuteDataTable成员方法


        public DataTable ExecuteDataTable(SqlConnection conn, string sql, string dbName, int timeout)
        {
            DataSet ds = ExecuteDataSet(conn, sql, dbName, timeout);

            if (ds.Tables.Count <= 0)
                return null;

            return ds.Tables[0];
        }

        public DataTable ExecuteDataTable(SqlConnection conn, string sql, string dbName)
        {
            return ExecuteDataTable(conn, sql, dbName, 0);
        }

        public DataTable ExecuteDataTable(SqlConnection conn, string sql, int timeout)
        {
            return ExecuteDataTable(conn, sql, "master", timeout);
        }

        public DataTable ExecuteDataTable(SqlConnection conn, string sql)
        {
            return ExecuteDataTable(conn, sql, "master", 0);
        }

        #endregion
    }
}
