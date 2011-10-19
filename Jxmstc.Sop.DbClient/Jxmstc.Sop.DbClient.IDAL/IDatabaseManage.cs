using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Jxmstc.Sop.DbClient.IDAL
{
    /// <summary>
    /// 数据库管理业务接口
    /// </summary>
    /// <typeparam name="T">连接对象的类型</typeparam>
    public interface IDatabaseManage<T>
    {
        /// <summary>
        /// 获取当前可访问的所有数据库
        /// </summary>
        /// <param name="conn">连接对象 如：SqlConnection</param>
        /// <returns>数据库列表</returns>
        DataSet GetAllDatabase(T conn);

        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbName">数据库名</param>
        void DelDatabase(T conn, string dbName);

        /// <summary>
        /// 强制关闭连接到指定数据库的用户
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbName">数据库名</param>
        void KillConnBydbName(T conn, string dbName);

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbName">当前数据库名</param>
        /// <param name="newName">数据库新名称</param>
        void Rename(T conn, string dbName, string newName);

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbName">创建的数据库名</param>
        void AddDatabase(T conn, string dbName);

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="dbName">将备份的数据库名</param>
        /// <param name="fileFullName">备份到</param>
        void BackupDatabase(T conn, string dbName, string fileFullName);

        //是否存在
        bool IsExist(T conn,string dbName);

    }
}
