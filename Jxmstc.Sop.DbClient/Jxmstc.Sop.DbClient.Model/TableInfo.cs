using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jxmstc.Sop.DbClient.Model
{
    public class TableInfo
    {
        private string tableName;
        private IList<ColumnInfo> colList;
        private IList<TriggerInfo> triList;

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
 
        public IList<ColumnInfo> ColList
        {
            get { return colList; }
            set { colList = value; }
        }

        public IList<TriggerInfo> TriList
        {
            get { return triList; }
            set { triList = value; }
        }


    }
}
