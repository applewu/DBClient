using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Jxmstc.Sop.DbClient.IDAL
{
    public  interface IProcedureManage<T>
    {
        DataTable GetAllProcedure(T conn, string dbname);

        bool ContainsProcedure(T conn, string dbname, string proname);

        void DelProcedure(T conn, string dbname, string name);

        string ShowProc(T conn, string dbname, string name);
    }
}
