using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Jxmstc.Sop.DbClient.IDAL;
using Jxmstc.Sop.DbClient.Model;
using System.Data;

namespace Jxmstc.Sop.DbClient.sqlserverDAL
{
    /// <summary>
    /// 表管理
    /// </summary>
    public class TableManage:ITableManage<SqlConnection>
    {
        Execute exec = new Execute();

        /// <summary>
        /// 得到所有表名
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbname">数据库名</param>
        /// <returns></returns>
        public System.Data.DataTable GetAllTable(SqlConnection conn, string dbname)
        {
            string sql_select_all_table = "select top 100 percent [name] from sysobjects where type='U' order by [name]";

            return exec.ExecuteDataTable(conn,sql_select_all_table ,dbname );
        }

        /// <summary>
        /// 是否包含此表
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbname">数据库名</param>
        /// <param name="tablename">表名</param>
        /// <returns></returns>
        public bool ContainsTable(SqlConnection conn, string dbname, string tablename)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbname">数据库名</param>
        /// <param name="name">表名</param>
        public void DelTable(SqlConnection conn, string dbname, string name)
        {
            dbname = dbname.TrimStart('[').TrimEnd(']');
            exec.ExecuteSql(conn, string.Format("DROP TABLE [{0}]", name), dbname);

        }

        /// <summary>
        /// 清除表数据
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbname">数据库名</param>
        /// <param name="name">表名</param>
        public void DelTableData(SqlConnection conn, string dbname, string name)
        {
            dbname = dbname.TrimStart('[').TrimEnd(']');
            exec.ExecuteSql(conn, string.Format("Delete From [{0}]", name), dbname);
        }

        //重命名
        public void RenameTable(SqlConnection conn, string dbname, string tablename, string newName)
        {

            dbname = dbname.TrimStart('[').TrimEnd(']');
            exec.ExecuteSql(conn, string.Format("sp_rename '{0}','{1}'", tablename, newName), dbname);
            
        }

        //添加表--未测试
        public void AddTable(SqlConnection conn, string dbname, TableInfo table)
        {
            StringBuilder sb = new StringBuilder();

            foreach (ColumnInfo col in table.ColList) {

                int i = 0;

                if (i != table.ColList.Count - 1)
                {
                    sb.Append(col.Name + " " + col.Typestring + " " + col.Isnullable + ",");

                    i++;
                }
                else {

                    sb.Append(col.Name + " " + col.Typestring + " " + col.Isnullable);
                }
            }

            string strSql = "Create Table {0}(" + sb.ToString() + ")";

            exec.ExecuteSql(conn, string.Format(strSql, table.TableName ), dbname);
        }

        //查看表中数据
        public DataTable ShowTableData(SqlConnection conn, string dbname, string table) {

            return  exec.ExecuteDataTable(conn, string.Format("select * from {0}", table), dbname);
        }
    }
}
