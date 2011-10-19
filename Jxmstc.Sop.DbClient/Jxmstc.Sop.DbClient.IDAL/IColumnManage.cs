using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Jxmstc.Sop.DbClient.Model;

namespace Jxmstc.Sop.DbClient.IDAL
{
    public interface IColumnManage<T>
    {

        DataTable GetAllColumns(T conn, string dbname, string table);

        void DelColumn(T conn, string dbname, string table, string name);

        void AddColumn(T conn, string dbname, string tableName, ColumnInfo column);

        bool ContainsColumn(T conn, string dbname, string tablename,ColumnInfo column);

        void UpdateColumn(T conn, string dbname, string table, ColumnInfo column);

        void RenameColumn(T conn, string dbname, string table, string colName, string newName);
    }
}
