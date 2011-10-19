using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jxmstc.Sop.DbClient.Model
{
    /// <summary>
    /// Login基本信息
    /// </summary>
    public class ConnectionInfo
    {

        private string server = "";
        private string username = "";
        private string pwd = "";

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Server
        {

            get { return server; }
            set { server = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {

            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd
        {

            get { return pwd; }
            set { pwd = value; }
        }
    }
}
