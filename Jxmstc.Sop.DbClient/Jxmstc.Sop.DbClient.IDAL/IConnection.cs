using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jxmstc.Sop.DbClient.Model;

namespace Jxmstc.Sop.DbClient.IDAL
{
    /// <summary>
    /// 连接的业务层接口
    /// </summary>
    /// <typeparam name="T">连接对象的类型</typeparam>
    public interface IConnection<T>
    {
        T CreateConn(ConnectionInfo conInfo);

        void OpenConn(T conn);

        void CloseConn(T conn);

        bool CheckConn();
    }
}
