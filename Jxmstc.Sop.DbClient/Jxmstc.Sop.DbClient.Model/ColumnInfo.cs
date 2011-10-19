using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jxmstc.Sop.DbClient.Model
{
    public class ColumnInfo
    {
        private string name;
        private string typestring;
        private string isnullable;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Typestring
        {
            get { return typestring; }
            set { typestring = value; }
        }
       
        public string Isnullable
        {
            get { return isnullable; }
            set { isnullable = value; }
        }


    }
}
