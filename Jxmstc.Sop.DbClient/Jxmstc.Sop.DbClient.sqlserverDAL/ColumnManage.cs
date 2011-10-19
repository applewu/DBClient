using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jxmstc.Sop.DbClient.IDAL;
using System.Data.SqlClient;
using Jxmstc.Sop.DbClient.Model;
using Jxmstc.Sop.DbClient.Utility;
using System.Data;

namespace Jxmstc.Sop.DbClient.sqlserverDAL
{
    /// <summary>
    /// 列管理
    /// </summary>
    public  class ColumnManage:IColumnManage<SqlConnection>
    {
        Execute exec = new Execute();

        #region IColumnManage<SqlConnection> 成员

        /// <summary>
        /// 得到该表所有列名
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbname">数据库名</param>
        /// <param name="table">表名</param>
        /// <returns></returns>
        public System.Data.DataTable GetAllColumns(SqlConnection conn, string dbname, string table) {

            string sql__select_all_columns = @"select top 100 percent 
case 
when b.variable=1 then
  b.name + '(' + cast(isnull(a.prec,0) as varchar(9)) + ')'
when isnull(a.prec,0)<>isnull(b.prec,0) and a.scale is null then
  b.name + '(' + cast(isnull(a.prec,0) as varchar(9)) + ')'
when isnull(a.prec,0)<>isnull(b.prec,0) or isnull(a.scale,0)<>isnull(b.scale,0) then
  b.name + '(' + cast(isnull(a.prec,0) as varchar(9)) + ',' + cast(isnull(a.scale,0) as varchar(9)) + ')'
else
  b.name 
end 'typestring',
Convert(varchar(5),a.isnullable)as isnullable,a.name
from syscolumns a
left join systypes b
on a.xtype=b.xtype and a.xusertype=b.xusertype
where a.id=object_id('{0}') order by colid";

                       return exec.ExecuteDataTable(conn, string.Format(sql__select_all_columns, table), dbname);

        }

        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbname">数据库名</param>
        /// <param name="table">表名</param>
        /// <param name="name">列名</param>
        public void DelColumn(SqlConnection conn, string dbname, string table, string name)
        {
            string dbName = dbname.TrimStart('[').TrimEnd(']');

            string strSql = @"declare @d_name varchar(255) 
select @d_name=b.name from sysobjects b join syscolumns a on 
b.id = a.cdefault where a.id = object_id('{0}') and a.name = '{1}'
if(@d_name<>'')
exec('alter table [{0}] drop constraint ' + @d_name)
alter table [{0}] drop column [{1}]
";
            exec.ExecuteSql(conn, string.Format(strSql, table, name), dbName);

        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbname">数据库名</param>
        /// <param name="tableName">表名</param>
        /// <param name="column">列对象</param>
        public void AddColumn(SqlConnection conn, string dbname, string tableName, ColumnInfo column)
        {
            //保证列名唯一
            if (!ContainsColumn(conn, dbname, tableName, column)) {

                string dbName = dbname.TrimStart('[').TrimEnd(']');

                StringBuilder strSql = new StringBuilder();

                strSql.Append("Alter table [" + tableName + "] add [" + column.Name + "] " + column.Typestring);

                //if (column.Isnullable.Equals ("0"))
                //{
                //    strSql.Append(" NOT");
                //} strSql.Append(" NULL");

                strSql.Append(" " + column.Isnullable+" default 0 ");

                exec.ExecuteSql(conn, strSql.ToString (), dbName);
            }

        }

        /// <summary>
        /// 表中是否存在该列
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbname">数据库名</param>
        /// <param name="tablename">表名</param>
        /// <param name="column">列对象</param>
        /// <returns>是否存在</returns>
        public bool ContainsColumn(SqlConnection conn, string dbname, string tablename,ColumnInfo column)
        {

            IList<ColumnInfo> colList = ConvertHelper<ColumnInfo>.FillModel(this.GetAllColumns(conn, dbname, tablename));

            bool isExist = false;

            for (int i = 0; i < colList.Count; i++) {

                if (column.Name == colList[i].Name)
                {

                    isExist = true ;
                    break;
                }
                 
            }

            return isExist;
        }


        public void UpdateColumn(SqlConnection conn, string dbname, string table, ColumnInfo column)
        {
            string str = "ALTER   TABLE   {0}   ALTER   COLUMN   {1}   {2}   {3}";

            exec .ExecuteSql (conn,string.Format (str,table ,column .Name ,column .Typestring ,column .Isnullable),dbname );

        }


        public void RenameColumn(SqlConnection conn, string dbname, string table, string colName, string newName)
        {
            exec.ExecuteSql(conn, string.Format("sp_rename '{0}.{1}','{2}','COLUMN'", table, colName, newName),dbname );
        }

        #endregion
    }

       
    }

