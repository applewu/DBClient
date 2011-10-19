using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Jxmstc.Sop.DbClient.Model;

namespace Jxmstc.Sop.DbClient.IDAL
{
    public interface ITableManage<T>
    {
        DataTable GetAllTable(T conn, string dbname);

        bool ContainsTable(T conn, string dbname, string tablename);

        void DelTable(T conn, string dbname, string name);

        void DelTableData(T conn, string dbname, string name);

        void RenameTable(T conn, string dbname, string tablename, string newName);

        void AddTable(T conn, string dbname, TableInfo table);

        DataTable ShowTableData(T conn, string dbname, string table);
    }
}
