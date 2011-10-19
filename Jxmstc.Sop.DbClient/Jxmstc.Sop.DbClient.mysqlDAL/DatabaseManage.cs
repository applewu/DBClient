using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;
using Jxmstc.Sop.DbClient.IDAL;
using System.Data;

namespace Jxmstc.Sop.DbClient.mysqlDAL
{
    class DatabaseManage : IDatabaseManage<MySqlConnection>
    {

        Execute exec = new Execute();

        public DataSet GetAllDatabase(MySqlConnection conn)
        {
            
            string sqlText = "show databases";

            return exec.ExecuteDataSet(conn, sqlText);

        }

        public void DelDatabase(MySqlConnection conn, string dbName)
        {
            dbName = dbName.TrimStart('[').TrimEnd(']');
            KillConnBydbName(conn, dbName);
            exec.ExecuteSql(conn, string.Format("DROP DATABASE {0}", dbName));
        }

        public void KillConnBydbName(MySqlConnection conn, string dbName)
        {
            throw new NotImplementedException();
        }

        public void Rename(MySqlConnection conn, string dbName, string newName)
        {
            throw new NotImplementedException();
        }

        public void AddDatabase(MySqlConnection conn, string dbName)
        {
            exec.ExecuteSql(conn, string.Format("create database {0}", dbName));
        }


        #region IDatabaseManage<MySqlConnection> 成员


        public void BackupDatabase(MySqlConnection conn, string dbName, string fileFullName)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDatabaseManage<MySqlConnection> 成员


        public bool IsExist(MySqlConnection conn, string dbName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
