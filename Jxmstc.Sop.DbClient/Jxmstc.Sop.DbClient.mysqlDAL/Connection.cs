using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Jxmstc.Sop .DbClient .IDAL ;
using Jxmstc.Sop.DbClient.Model;
using MySql.Data.MySqlClient;
using System.Data;

namespace Jxmstc.Sop.DbClient.mysqlDAL
{
    public class Connection:IConnection <MySqlConnection>
    {

        #region IConnection<T> 成员

        /// <summary>
        /// 创建连接
        /// </summary>
        /// <param name="conInfo"></param>
        /// <returns></returns>
        public MySqlConnection CreateConn(ConnectionInfo conInfo)
        {
            MySqlConnection mysqlCon = new MySqlConnection();

            if (conInfo != null)
            {

                string strConn = string.Format("host={0};uid={1};pwd={2};database=information_schema;"
                    , conInfo.Server.Replace(";", ""), conInfo.UserName.Replace(";", ""), conInfo.Pwd.Replace(";", ""));

                mysqlCon.ConnectionString = strConn;

            }

            return mysqlCon;
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <param name="conn"></param>
        public void OpenConn(MySqlConnection conn)
        {
            MySqlConnection mysqlconn = (object)conn as MySqlConnection;

            if (mysqlconn == null) return;

            if (mysqlconn.State != ConnectionState.Open)
            {

                mysqlconn.Open();
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="conn"></param>
        public void CloseConn(MySqlConnection conn)
        {
            if (conn == null) return;

            MySqlConnection sqlcon = (object)conn as MySqlConnection;

            try
            {

                if (sqlcon.State == ConnectionState.Open)
                {

                    sqlcon.Close();
                }
                sqlcon.Dispose();
            }

            catch { }
        }


        public bool CheckConn()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
