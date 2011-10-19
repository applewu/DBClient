using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jxmstc.Sop.DbClient.Model;
using System.Data.SqlClient;
using Jxmstc.Sop.DbClient.IDAL;
using Jxmstc.Sop.DbClient.DALFactory;
using System.Data;
using System.IO;

namespace Jxmstc.Sop.DbClient.BLL
{
    public class Manage:ManageBase 
    {
        //数据库备份
        public void BackupDataBase(string dbName,ConnectionInfo conn,string fileFullName) {


                SqlConnection sqlCon = Provider.getMssqlConn(conn);

                IDatabaseManage<SqlConnection> dbManage = DBFactory.CreateDatabaseManage<SqlConnection>();

                dbManage .BackupDatabase(sqlCon ,dbName ,fileFullName);
            
        }

        //查看表数据
        public DataTable ShowTableData(string dbName, ConnectionInfo conn, string table) {

            SqlConnection sqlCon = Provider.getMssqlConn(conn);

            ITableManage<SqlConnection> tableManage = DBFactory.CreateTableManage<SqlConnection>();
            return tableManage.ShowTableData(sqlCon, dbName, table);
        }

        //查看脚本
        public override string ShowScript(string dbName, ConnectionInfo conn, string name) {

            SqlConnection sqlCon = Provider.getMssqlConn(conn);

            IViewManage<SqlConnection> tableManage = DBFactory.CreateViewManage<SqlConnection>();
            string str= tableManage.ShowView(sqlCon, dbName, name);

            //对str进行/r/n处理
            str =str.Replace("\r\n", " ");

            return str;
        }

        //数据库是否存在
        public bool IsExistDataBase(string dbName,ConnectionInfo conn) {


                SqlConnection sqlCon = Provider.getMssqlConn(conn);

                IDatabaseManage<SqlConnection> dbManage = DBFactory.CreateDatabaseManage<SqlConnection>();

                return dbManage.IsExist(sqlCon, dbName);


        }

        public override void CreateObject(ConnectionInfo conn, string objName) {

                SqlConnection sqlCon = Provider.getMssqlConn(conn);

                IDatabaseManage<SqlConnection> dbManage = DBFactory.CreateDatabaseManage<SqlConnection>();

                dbManage.AddDatabase(sqlCon, objName);
        }

        public void CreateObject(ConnectionInfo conn, string dbName, string tableName, ColumnInfo column) {

            SqlConnection sqlCon = Provider.getMssqlConn(conn);

            IColumnManage<SqlConnection> columnManage = DBFactory.CreateColumnManage<SqlConnection>();

            columnManage.AddColumn(sqlCon, dbName, tableName, column);
            
        }

        public override void RenameObject( ConnectionInfo conn, string objName, string newName) {


                SqlConnection sqlCon = Provider.getMssqlConn(conn);
                IDatabaseManage<SqlConnection> dbManage = DBFactory.CreateDatabaseManage<SqlConnection>();

                dbManage.Rename(sqlCon, objName, newName);

        }

        public override void RenameObject(ConnectionInfo conn, string objName, string newName, string dbName) {


                SqlConnection sqlCon = Provider.getMssqlConn(conn);
                ITableManage<SqlConnection> tableManage = DBFactory.CreateTableManage<SqlConnection>();
                tableManage.RenameTable(sqlCon, dbName, objName, newName);

        }

        public void RenameObject(ConnectionInfo conn, string objName, string newName, string dbName, string tableName) {

            SqlConnection sqlCon = Provider.getMssqlConn(conn);
            IColumnManage<SqlConnection> columnManage = DBFactory.CreateColumnManage<SqlConnection>();
            columnManage.RenameColumn(sqlCon, dbName, tableName, objName, newName);
        }

        public override void DeleteObject(ConnectionInfo conn, string objName)
        {


                    SqlConnection sqlCon = Provider.getMssqlConn(conn);

                    IDatabaseManage<SqlConnection> dbManage = DBFactory.CreateDatabaseManage<SqlConnection>();

                    dbManage.DelDatabase(sqlCon, objName );

        }

        public override void DeleteObject(ConnectionInfo conn, string objType, string dbName, string objName)
        {

            if (objType == "folder_table")
            {

                    SqlConnection sqlCon = Provider.getMssqlConn(conn);

                    ITableManage<SqlConnection> tableManage = DBFactory.CreateTableManage<SqlConnection>();

                    tableManage.DelTable(sqlCon, dbName, objName);
            }
            else if (objType == "folder_view")
            {

                    SqlConnection sqlCon = Provider.getMssqlConn(conn);

                    IViewManage<SqlConnection> viewManage = DBFactory.CreateViewManage<SqlConnection>();

                    viewManage.DelView(sqlCon, dbName, objName);
            }
        }

        public bool DeleteObject(ConnectionInfo conn, string objType, string dbName, string tableName, string objName) {

            if (objType == "folder_col")
            {

                    SqlConnection sqlCon = Provider.getMssqlConn(conn);

                    IColumnManage<SqlConnection> colManage = DBFactory.CreateColumnManage<SqlConnection>();

                    DataTable dt=colManage.GetAllColumns(sqlCon, dbName, tableName);

                    if (dt.Rows.Count > 1) {

                        colManage.DelColumn(sqlCon, dbName, tableName, objName);
                        return true;
                    }

            }

            return false;
        }

        public void UpdateColumn(ConnectionInfo conn, string dbName, string tableName, ColumnInfo column) {

            SqlConnection sqlCon = Provider.getMssqlConn(conn);

            IColumnManage<SqlConnection> colManage = DBFactory.CreateColumnManage<SqlConnection>();

            colManage.UpdateColumn(sqlCon, dbName, tableName, column);
        }
    }
}
