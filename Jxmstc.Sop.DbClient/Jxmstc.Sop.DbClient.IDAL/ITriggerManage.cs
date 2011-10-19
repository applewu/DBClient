using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Jxmstc.Sop.DbClient.IDAL
{
    public interface ITriggerManage<T>
    {
        DataTable GetAllTriggers(T conn, string dbname, string table);


    }
}
