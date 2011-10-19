using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jxmstc.Sop.DbClient.Model
{
    public class DataBaseInfo
    {
        private string dbName;
        private IList<TableInfo> tableList;
        private IList<ViewInfo> viewList;
        private IList<ProcedureInfo> procList; 

        public string DbName
        {
            get { return dbName; }
            set { dbName = value; }
        }

        public IList<TableInfo> TableList
        {
            get { return tableList; }
            set { tableList = value; }
        }

        public IList<ViewInfo> ViewList
        {
            get { return viewList; }
            set { viewList = value; }
        }

        public IList<ProcedureInfo> ProcList
        {
            get { return procList ; }
            set { procList = value; }
        }

    }
}
