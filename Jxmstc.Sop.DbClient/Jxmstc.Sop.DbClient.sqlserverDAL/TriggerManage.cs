using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jxmstc.Sop.DbClient.IDAL;
using System.Data.SqlClient;

namespace Jxmstc.Sop.DbClient.sqlserverDAL
{
    public class TriggerManage:ITriggerManage<SqlConnection>
    {
        Execute exec = new Execute();

        #region ITriggerManage<T> 成员

        public System.Data.DataTable GetAllTriggers(SqlConnection conn, string dbname, string table)
        {
            string sql__select_all_Trigger = @"select a.[name]from sysobjects a inner join sysobjects b on a.parent_obj=b.[id] and a.type='TR' where b.[name]='{0}' 
order by a.[name]";

            return exec.ExecuteDataTable(conn, string.Format(sql__select_all_Trigger, table), dbname);
        }

        #endregion
    }
}
