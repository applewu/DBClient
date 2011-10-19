using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Jxmstc.Sop.DbClient.IDAL;
using System.Data.SqlClient;
using System.Data;

namespace Jxmstc.Sop.DbClient.sqlserverDAL
{
    /// <summary>
    /// SQLServer2005数据库管理相关业务操作
    /// </summary>
    public class DatabaseManage: IDatabaseManage<SqlConnection >
    {

        Execute exec = new Execute();

        /// <summary>
        /// 得到所有数据库名
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <returns></returns>
        public DataSet GetAllDatabase(SqlConnection  conn)
        {
            DataSet dsDBList = null;

            try{

            if (conn.State.ToString () == "Closed") {

                conn.Open();
            }
            
            SqlCommand cmd = conn.CreateCommand();

            //不考虑用户访问权限 获得所有数据库名称列表
            //string sysdb = "'master','model','msdb','tempdb','distribution'";
            //cmd.CommandText = string.Format("select TOP 100 PERCENT [name] from sysdatabases where [name] not in({0}) order by [name]", sysdb);

            //获得当前登录名具有访问权限的数据库名称列表
            cmd.CommandText = "sp_msforeachdb \"Select '?'\"";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();//执行SQL命令后的结果集（多表形式）
            dsDBList = new DataSet();//返回的结果集

            DataTable dt = new DataTable();
            dt.Columns .Add (new DataColumn ("name",typeof (string)));

                
                da.Fill(ds);

                    for (int i = 0; i < ds.Tables.Count ; i++) {

                        string dbName = ds.Tables[i].Rows[0][0].ToString();

                        //若不显示系统数据库 则:
                        //if ((dbName != "master")&&(dbName != "model")&&(dbName != "msdb")&&(dbName != "tempdb"))
                        //{
                            //为dsDBList的数据表添加一行
                            DataRow dr = dt.NewRow();
                            dr[0] = dbName;
                            dt.Rows.Add(dr);
                        
                        //}
                    }

                    dsDBList.Tables.Add(dt);
                    }
            
            catch (SqlException ex) { throw ex; }
            
            finally
            {

                conn.Close();
            }

            System.Threading.Thread.Sleep(500);

            return dsDBList ;
        }

        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbName">数据库名</param>
        public void DelDatabase(SqlConnection  conn, string dbName)
        {
            dbName = dbName.TrimStart('[').TrimEnd(']');
            KillConnBydbName(conn, dbName);
            exec.ExecuteSql(conn, string.Format("DROP DATABASE [{0}]", dbName));

        }

        /// <summary>
        /// 强制关闭连接到指定数据库的用户
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbName">数据库名</param>
        public void KillConnBydbName(SqlConnection  conn, string dbName)
        {
            //SqlCommand cmd = conn.CreateCommand();

            string sql = @"
              declare @dbname varchar(200)
              set @dbname = '{0}'
              declare @sql nvarchar(500)       
              declare @spid nvarchar(20)   

              declare #tb cursor for
                select spid=cast(spid as varchar(20)) from master..sysprocesses where dbid=db_id(@dbname)
              open #tb
              fetch next from #tb into @spid
              while @@fetch_status=0
              begin
                exec('kill   '+@spid)
                fetch next from #tb into @spid
              end  close #tb  deallocate #tb";

            sql = string.Format(sql, dbName);

            exec.ExecuteSql(conn, sql);
        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbName">当前数据库名</param>
        /// <param name="newName">数据库新名称</param>
        public void Rename(SqlConnection conn, string dbName, string newName)
        {

            exec.ExecuteSql(conn, string.Format("sp_renamedb '{0}','{1}'", dbName, newName));
        }

        //添加数据库
        public void AddDatabase(SqlConnection conn, string dbName)
        {

            exec.ExecuteSql(conn, string.Format("create database {0}", dbName));
        }

        /// <summary>
        /// 完整备份数据库
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbName">将备份的数据库名</param>
        /// <param name="fileFullName">备份到</param>
        public void BackupDatabase(SqlConnection conn, string dbName, string fileFullName)
        {
            exec .ExecuteSql (conn,string .Format ("Backup Database {0} To disk='{1}'",dbName ,fileFullName ));
        }


        public bool IsExist(SqlConnection conn, string dbName)
        {
            DataSet ds=exec .ExecuteDataSet (conn ,string.Format("select 1 from sysdatabases where name='{0}'", dbName));
          
            if(ds.Tables [0].Rows .Count ==0){
            
                return false ;
            }else {
            
                return true ;
            }
        }


    }
}
