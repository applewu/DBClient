using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Jxmstc.Sop.DbClient.Model;
using System.Data.SqlClient;
using Jxmstc.Sop.DbClient.IDAL;

namespace Jxmstc.Sop.DbClient.sqlserverDAL
{
   /// <summary>
    /// SQLServer2005连接相关业务操作
   /// </summary>
    /// <typeparam name="T">连接对象的类型</typeparam>
    public class Connection: IConnection<SqlConnection> 
    {
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <param name="conInfo"></param>
        /// <returns></returns>
        public SqlConnection CreateConn(ConnectionInfo conInfo)
        {
            SqlConnection sqlCon = new SqlConnection();

            if (conInfo != null)
            {

                string strConn = string.Format("server={0};uid={1};pwd={2};database=master;Asynchronous Processing=true;"
                    , conInfo.Server.Replace(";", ""), conInfo.UserName.Replace(";", ""), conInfo.Pwd.Replace(";", ""));

                sqlCon.ConnectionString = strConn;

            }

            return sqlCon ;

        }

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <param name="conn"></param>
        public void OpenConn(SqlConnection conn)
        {
            
            SqlConnection sqlconn=(object)conn as SqlConnection ;

            if (sqlconn == null) return;

                if(sqlconn .State!=ConnectionState .Open ){
                
                    sqlconn .Open ();
                }
  
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="conn"></param>
        public void CloseConn(SqlConnection conn)
        {

            if (conn == null) return;

            SqlConnection sqlcon = (object)conn as SqlConnection;

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


        /// <summary>
        /// 检查连接
        /// </summary>
        /// <returns>连接是否打开</returns>
        public bool CheckConn()
        {
            return true ;
        }

    }
}
