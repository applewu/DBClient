using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Jxmstc.Sop.DbClient.IDAL;

namespace Jxmstc.Sop.DbClient.mysqlDAL
{
    class TriggerManage:ITriggerManage<MySqlConnection>
    {
        Execute exec = new Execute();

        #region ITriggerManage<MySqlConnection> 成员

        public System.Data.DataTable GetAllTriggers(MySqlConnection conn, string dbname, string table)
        {

    string sql__select_all_Trigger = "select Trigger_name as name from information_schema.Triggers where Trigger_schema='{0}' and Event_object_table='{1}';";

    return exec.ExecuteDataTable(conn, string.Format(sql__select_all_Trigger, dbname, table));

        }

        #endregion
    }
}
