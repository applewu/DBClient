using System.Data;
using Jxmstc.Sop.DbClient.Model;
using Jxmstc.Sop.DbClient.Utility;
using Jxmstc.Sop.DbClient.IDAL;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Jxmstc.Sop.DbClient.DALFactory;
using System.Collections.Generic;
using System;

namespace Jxmstc.Sop.DbClient.BLL{
    public class Provider
    {
        static SqlConnection sqlCon;
        static MySqlConnection mysqlCon;

        /// <summary>
        /// 针对不同数据库 创建不同的连接对象
        /// </summary>
        public static void findConn(string strDBType,ConnectionInfo connInfo)
        {

            if (strDBType == "SQL Server2005/2008")
            {

                if (sqlCon == null) {

                    sqlCon = getMssqlConn(connInfo);
                }

            }
            else if (strDBType == "MySql")
            {

                IConnection<MySqlConnection> con = DBFactory.CreateConnnection<MySqlConnection>();

                mysqlCon = con.CreateConn(connInfo);

                con.OpenConn(mysqlCon);
            }
            else {

                return;
            }

        }

        /// <summary>
        /// 获得Sql Server数据库的连接对象
        /// </summary>
        /// <param name="strDBType">数据库类型</param>
        /// <param name="connInfo">连接实体</param>
        /// <returns></returns>
        internal static SqlConnection getMssqlConn(ConnectionInfo connInfo) {

            IConnection<SqlConnection> con = DBFactory.CreateConnnection<SqlConnection>();

            sqlCon = con.CreateConn(connInfo);

            con.OpenConn(sqlCon);

            return sqlCon;
        }

        /// <summary>
        /// 获得Mysql数据库的连接对象
        /// </summary>
        /// <param name="connInfo"></param>
        /// <returns></returns>
        internal static MySqlConnection getMysqlConn(ConnectionInfo connInfo) {

            IConnection<MySqlConnection> con = DBFactory.CreateConnnection<MySqlConnection>();

            mysqlCon = con.CreateConn(connInfo);

            con.OpenConn(mysqlCon);

            return mysqlCon;
        }

        /// <summary>
        /// 获得数据库资源信息列表
        /// </summary>
        /// <param name="strDBType">数据库类型</param>
        /// <param name="connInfo">连接实体</param>
        /// <returns></returns>
        public static IList<DataBaseInfo> findDBManage(string strDBType, ConnectionInfo connInfo)
        {
            if (sqlCon == null) {

                findConn(strDBType, connInfo);
            }

            if (strDBType == "SQL Server2005/2008")
            {

                return findDBManage_Mssql();
            }
            else if (strDBType == "MySql")
            {

                return findDBManage_Mysql();
            }
           
            else
            {

                return null;
            }
        }

        /// <summary>
        /// 获得数据库名称
        /// </summary>
        /// <param name="strDBType">数据库类型：SqlServer2005/2008、Mysql</param>
        /// <param name="connInfo">连接实体</param>
        /// <returns>数据集</returns>
        public static DataSet getDBList(string strDBType,ConnectionInfo connInfo) {

            DataSet ds = null;

            try
            {
                if (sqlCon == null)
                {

                    findConn(strDBType, connInfo);
                }


                ds = new DataSet();

                if (strDBType == "SQL Server2005/2008")
                {

                    IDatabaseManage<SqlConnection> dbManage = DBFactory.CreateDatabaseManage<SqlConnection>();
                    ds = dbManage.GetAllDatabase(sqlCon);
                }
                else if (strDBType == "MySql")
                {

                    IDatabaseManage<MySqlConnection> dbManage = DBFactory.CreateDatabaseManage<MySqlConnection>();
                    ds = dbManage.GetAllDatabase(mysqlCon);
                }

            }
            catch (Exception ex) { throw ex; }
            return ds;

        }


        #region 获得Sql Server数据库资源(表、列、触发器)信息

        /// <summary>
        ///获得SqlServer2005/2008数据库信息(表、触发器等)
        /// </summary>
        /// <returns></returns>
        private static IList<DataBaseInfo> findDBManage_Mssql()
        {

            IDatabaseManage<SqlConnection> dbManage = DBFactory.CreateDatabaseManage<SqlConnection>();
            ITableManage<SqlConnection> tableManage = DBFactory.CreateTableManage<SqlConnection>();
            IViewManage<SqlConnection> viewManage = DBFactory.CreateViewManage<SqlConnection>();
            IProcedureManage<SqlConnection> procManage = DBFactory.CreateProcManage<SqlConnection>();

            IList<DataBaseInfo> list = new List<DataBaseInfo>();
            DataSet ds = dbManage.GetAllDatabase(sqlCon);

            if (sqlCon.State != ConnectionState.Open)
            {

                sqlCon.Open();
            }

            if (ds.Tables[0].Rows.Count != 0)
            {

                //根据数据库名查询所有的表和视图信息
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    string dbName = ds.Tables[0].Rows[i][0].ToString();

                    DataTable dt_tables = tableManage.GetAllTable(sqlCon, ds.Tables[0].Rows[i][0].ToString());
                    DataTable dt_views = viewManage.GetAllView(sqlCon, ds.Tables[0].Rows[i][0].ToString());
                    DataTable dt_procs = procManage.GetAllProcedure(sqlCon, ds.Tables[0].Rows[i][0].ToString());


                    IList<TableInfo> list_table = new List<TableInfo>();

                    //根据表名得到所有列和触发器的信息 转化为TableInfo实体对象
                    for (int j = 0; j < dt_tables.Rows.Count; j++)
                    {

                        TableInfo tableInfo = new TableInfo();

                        string tableName = dt_tables.Rows[j][0].ToString();

                        tableInfo.TableName = tableName;
                        tableInfo.ColList = getColumns_Mssql(dbName, tableName);
                        tableInfo.TriList = getTriggers_Mssql(dbName, tableName);

                        list_table.Add(tableInfo);
                    }


                    DataBaseInfo dbInfo = new DataBaseInfo();
                    dbInfo.DbName = ds.Tables[0].Rows[i][0].ToString();
                    dbInfo.TableList = list_table;
                    dbInfo.ViewList = ConvertHelper<ViewInfo>.FillModel(dt_views);
                    dbInfo.ProcList = ConvertHelper<ProcedureInfo>.FillModel(dt_procs);

                    list.Add(dbInfo);

                }

            }

            return list;

        }

        /// <summary>
        /// 列信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private  static IList<ColumnInfo> getColumns_Mssql(string dbName, string table)
        {

            DataTable dt = getColumnsToDataTable_Mssql(dbName, table);

            return ConvertHelper<ColumnInfo>.FillModel(dt);
        }

        /// <summary>
        /// 列信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataTable getColumnsToDataTable_Mssql(string dbName, string table) {

            IColumnManage<SqlConnection> columnManage = DBFactory.CreateColumnManage<SqlConnection>();

            if (sqlCon.State != ConnectionState.Open)
            {

                sqlCon.Open();
            }

            DataTable dt = columnManage.GetAllColumns(sqlCon, dbName, table);

            return dt;
        }

        /// <summary>
        /// 触发器信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private  static IList<TriggerInfo> getTriggers_Mssql(string dbName, string table)
        {

            ITriggerManage<SqlConnection> triManage = DBFactory.CreateTriggerManage<SqlConnection>();

            if (sqlCon.State != ConnectionState.Open)
            {

                sqlCon.Open();
            }

            DataTable dt = triManage.GetAllTriggers(sqlCon, dbName, table);

            return ConvertHelper<TriggerInfo>.FillModel(dt);
        }

        #endregion

        #region 获得Mysql数据库资源信息

        /// <summary>
        /// 获得MySQL数据库信息(表、触发器等)
        /// </summary>
        /// <returns></returns>
        private static IList<DataBaseInfo> findDBManage_Mysql() {

            IDatabaseManage<MySqlConnection> dbManage = DBFactory.CreateDatabaseManage<MySqlConnection>();
            ITableManage<MySqlConnection> tableManage = DBFactory.CreateTableManage<MySqlConnection>();
            IViewManage<MySqlConnection> viewManage = DBFactory.CreateViewManage<MySqlConnection>();
            IProcedureManage<MySqlConnection> procManage = DBFactory.CreateProcManage<MySqlConnection>();

                IList<DataBaseInfo> list = new List<DataBaseInfo>();
                DataSet ds = dbManage.GetAllDatabase(mysqlCon);

                if (mysqlCon.State != ConnectionState.Open)
                {

                    mysqlCon.Open();
                }

                if (ds.Tables[0].Rows.Count != 0)
                {

                    //根据数据库名查询所有的表和视图信息
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        string dbName = ds.Tables[0].Rows[i][0].ToString();

                        DataTable dt_tables = tableManage.GetAllTable(mysqlCon, ds.Tables[0].Rows[i][0].ToString());
                        DataTable dt_views = viewManage.GetAllView(mysqlCon, ds.Tables[0].Rows[i][0].ToString());
                        DataTable dt_procs = procManage.GetAllProcedure(mysqlCon, ds.Tables[0].Rows[i][0].ToString());


                        IList<TableInfo> list_table = new List<TableInfo>();

                        //根据表名得到所有列和触发器的信息 转化为TableInfo实体对象
                        for (int j = 0; j < dt_tables.Rows.Count; j++)
                        {

                            TableInfo tableInfo = new TableInfo();

                            string tableName = dt_tables.Rows[j][0].ToString();

                            tableInfo.TableName = tableName;
                            tableInfo.ColList = getColumns_Mysql(dbName, tableName);
                            tableInfo.TriList = getTriggers_Mysql(dbName, tableName);

                            list_table.Add(tableInfo);
                        }


                        DataBaseInfo dbInfo = new DataBaseInfo();
                        dbInfo.DbName = ds.Tables[0].Rows[i][0].ToString();
                        dbInfo.TableList = list_table;
                        dbInfo.ViewList = ConvertHelper<ViewInfo>.FillModel(dt_views);
                        dbInfo.ProcList = ConvertHelper<ProcedureInfo>.FillModel(dt_procs);

                        list.Add(dbInfo);

                    }

                }

                return list;

            }

        /// <summary>
        /// 列信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private static IList<ColumnInfo> getColumns_Mysql(string dbName, string table)
        {

            DataTable dt = getColumnsToDataTable_Mysql(dbName, table);

            return ConvertHelper<ColumnInfo>.FillModel(dt);
        }

        /// <summary>
        /// 列信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataTable getColumnsToDataTable_Mysql(string dbName, string table)
        {

            IColumnManage<MySqlConnection> columnManage = DBFactory.CreateColumnManage<MySqlConnection>();

            if (mysqlCon.State != ConnectionState.Open)
            {

                mysqlCon.Open();
            }

            DataTable dt = columnManage.GetAllColumns(mysqlCon, dbName, table);

            return dt;
        }

        /// <summary>
        /// 触发器信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private static IList<TriggerInfo> getTriggers_Mysql(string dbName, string table)
        {

            ITriggerManage<MySqlConnection> triManage = DBFactory.CreateTriggerManage<MySqlConnection>();

            if (mysqlCon.State != ConnectionState.Open)
            {

                mysqlCon.Open();
            }

            DataTable dt = triManage.GetAllTriggers(mysqlCon, dbName, table);

            return ConvertHelper<TriggerInfo>.FillModel(dt);
        }

        #endregion


        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strDBType">数据库类型</param>
       /// <param name="strSqlText">sql语句</param>
       /// <param name="strDB">数据库名</param>
       /// <returns>结果集</returns>
        public static DataSet findExec(string strDBType,string strSqlText,string strDB) {

            DataSet ds = new DataSet();

            if (strDBType == "SQL Server2005/2008")
            {

                IExecute<SqlConnection> exec = DBFactory.CreateExecute<SqlConnection>();

                if (sqlCon.State != ConnectionState.Open)
                {

                    sqlCon.Open();
                }

                ds = exec.ExecuteDataSet(sqlCon,strSqlText, strDB );

                return ds;
            }
            else if (strDBType == "MySql")
            {

                IExecute<MySqlConnection> exec = DBFactory.CreateExecute<MySqlConnection>();

                if (mysqlCon.State != ConnectionState.Open)
                {

                    mysqlCon.Open();
                }

                ds = exec.ExecuteDataSet(mysqlCon, strSqlText, strDB);

                return ds;

            }
            else
            {

                return null;
            }
        }
    }
}
