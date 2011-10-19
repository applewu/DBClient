using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Jxmstc.Sop.DbClient.Model
{
    /// <summary>
    /// 执行Sql语句的基本信息
    /// </summary>
    public  class ExecSqlInfo
    {
        private bool success=false ;

        private string message=String .Empty ;

        private DataSet ds=null ;

        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool Success {

            get { return success; }
            set { success = value; }
        }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message {

            get { return message; }
            set { message = value; }
        }

        /// <summary>
        /// 执行后返回的数据集
        /// </summary>
        public DataSet DataSet {

            get { return ds; }
            set { ds = value; }
        }

    }
}
