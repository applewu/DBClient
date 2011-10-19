using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Jxmstc.Sop.DbClient.IDAL;
using System.Data;

namespace Jxmstc.Sop.DbClient.mysqlDAL
{
    public class ColumnManage:IColumnManage<MySqlConnection>
    {

        Execute exec = new Execute();

        #region IColumnManage<MySqlConnection> 成员

        public DataTable GetAllColumns(MySqlConnection conn, string dbname, string table)
        {
                string sql__select_all_columns = "show columns from {0}";

                DataTable dt=exec.ExecuteDataTable(conn, string.Format(sql__select_all_columns, table), dbname);

                DataTable dt_Columns = new DataTable();
                dt_Columns .Columns .Add (new DataColumn ("name",typeof (string)));
                dt_Columns .Columns .Add (new DataColumn ("typestring",typeof (string )));
                dt_Columns .Columns .Add (new DataColumn ("isnullable",typeof (string )));

                foreach (DataRow dr in dt.Rows) {

                    DataRow dr_Column=dt_Columns .NewRow ();

                    dr_Column ["name"]=dr["Field"];
                    dr_Column["typestring"] = dr["Type"];
                    dr_Column["isnullable"] = dr["Null"];

                    dt_Columns.Rows.Add(dr_Column);
                }

                return dt_Columns;

        }

        public void DelColumn(MySqlConnection conn, string dbname, string table, string name)
        {
            throw new NotImplementedException();
        }

        public void AddColumn(MySqlConnection conn, string dbname, string tableName, Jxmstc.Sop.DbClient.Model.ColumnInfo column)
        {
            throw new NotImplementedException();
        }

        public bool ContainsColumn(MySqlConnection conn, string dbname, string tablename, Jxmstc.Sop.DbClient.Model.ColumnInfo column)
        {
            throw new NotImplementedException();
        }

        public void UpdateColumn(MySqlConnection conn, string dbname, string table, Jxmstc.Sop.DbClient.Model.ColumnInfo column)
        {
            throw new NotImplementedException();
        }

        public void RenameColumn(MySqlConnection conn, string dbname, string table, string colName, string newName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
