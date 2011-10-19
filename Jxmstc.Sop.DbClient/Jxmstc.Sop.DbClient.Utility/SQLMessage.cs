using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jxmstc.Sop.DbClient.Utility
{
    /// <summary>
    /// SQL消息
    /// </summary>
    public class SQLMessage
    {

        public List<string> SqlMsgList = new List<string>();

        /// <summary>
        /// 消息是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmptySQLMessage()
        {

            if (this.SqlMsgList == null || this.SqlMsgList.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获得消息字符串
        /// </summary>
        /// <returns></returns>
        public string GetSQLMessageString()
        {

            if (SqlMsgList == null || SqlMsgList.Count == 0)
                return string.Empty;

            string str = string.Empty;
            for (int i = 0; i < SqlMsgList.Count; i++)
            {

                str += SqlMsgList[i] + "。";

            }

            return str;

        }
    }
}
