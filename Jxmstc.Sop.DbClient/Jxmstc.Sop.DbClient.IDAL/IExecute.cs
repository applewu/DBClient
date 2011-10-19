using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Jxmstc.Sop.DbClient.Model;
using System.Data;

namespace Jxmstc.Sop.DbClient.IDAL
{
    /// <summary>
    /// 执行sql的业务层接口
    /// </summary>
    /// <typeparam name="T">连接对象的类型</typeparam>
    public interface IExecute<T>
    {
        void ExecuteSql(T conn, string sql);

        void ExecuteSql(T conn, string sql, string dbName);

        void ExecuteSql(T conn, string sql, string dbName, int timeout);

        DataSet ExecuteDataSet(T conn, string sql, string dbName, int timeout);

        DataSet ExecuteDataSet(T conn, string sql, string dbName);

        DataSet ExecuteDataSet(T conn, string sql);

        DataTable ExecuteDataTable(T conn, string sql, string dbName, int timeout);

        DataTable ExecuteDataTable(T conn, string sql, string dbName);

        DataTable ExecuteDataTable(T conn, string sql, int timeout);

        DataTable ExecuteDataTable(T conn, string sql);

    }
}
