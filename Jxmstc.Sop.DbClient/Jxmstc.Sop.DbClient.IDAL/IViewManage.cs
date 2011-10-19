using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Jxmstc.Sop.DbClient.IDAL
{
    public interface IViewManage<T>
    {
        DataTable GetAllView(T conn, string dbname);

        bool ContainsView(T conn, string dbname, string viewname);

        void DelView(T conn, string dbname, string name);

        string ShowView(T conn, string dbname, string name);
    }
}
