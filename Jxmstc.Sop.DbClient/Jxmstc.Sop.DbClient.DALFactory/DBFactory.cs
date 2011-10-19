using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

using Jxmstc.Sop .DbClient.IDAL ;
using System.Configuration;
using System.Reflection;
using System.Data.SqlClient;
using System.Collections.Specialized;

namespace Jxmstc.Sop.DbClient.DALFactory
{
    /// <summary>
    /// 数据库工厂类
    /// </summary>
    public class DBFactory
    {
        //程序集路径
        private static string assemblyPath;

        //当前架构
        private static readonly string architecture = ConfigurationManager.AppSettings["Architecture"];

        #region 缓存处理
        /// <summary>
        /// 创建对象或从缓存获取
        /// </summary>
        public static object CreateObject(string assemblyPath, string className)
        {
            className = assemblyPath + className;

            if (architecture.Trim().ToUpper() == "C/S")
            {
                return Assembly.Load(assemblyPath).CreateInstance(className);
            }

            object objType = GetCache(className);//从缓存读取
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(assemblyPath).CreateInstance(className);//反射创建

                    SetCache(className, objType);// 写入缓存
                }
                catch(Exception e)
                {
                    throw new AbandonedMutexException(e.ToString());
                }
            }
            return objType;
        }
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey">缓存标记</param>
        /// <returns>对象</returns>
        public static object GetCache(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey">缓存标记</param>
        /// <param name="obj">对象</param>
        public static void SetCache(string cacheKey, object obj)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, obj);
        }

        #endregion

        /// <summary>
        /// 访问相应的命名空间
        /// </summary>
        /// <param name="type">泛型对象类型</param>
        public static  void FindAssemblyPath(string type) {


            NameValueCollection nc = (NameValueCollection)ConfigurationSettings.GetConfig("DBType");

            for (int i = 0; i < nc.Keys.Count; i++)
            {

                if (type == nc.AllKeys[i].ToString())
                {

                    assemblyPath = nc[type];
                }
            }
        }

        /// <summary>
        /// 创建连接
        /// </summary>
        /// <typeparam name="T">连接对象类型 如:SqlConnection</typeparam>
        /// <returns></returns>
        public static IConnection<T> CreateConnnection<T>() {

            string type = typeof(T).Name;

            FindAssemblyPath(type);

            string className = ".Connection";
            object objType = CreateObject(assemblyPath, className);
            return (IConnection<T>)objType;

        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <typeparam name="T">连接对象类型 如:SqlConnection</typeparam>
        /// <returns></returns>
        public static IExecute<T> CreateExecute<T>() {

            string type = typeof(T).Name;

            FindAssemblyPath(type);

            string className = ".Execute";
            object objType = CreateObject(assemblyPath, className);
            return (IExecute<T>)objType;
        }

        /// <summary>
        /// 数据库管理
        /// </summary>
        /// <typeparam name="T">连接对象类型 如:SqlConnection</typeparam>
        /// <returns></returns>
        public static IDatabaseManage<T> CreateDatabaseManage<T>() {

            string type = typeof(T).Name;

            FindAssemblyPath(type);

            string className = ".DatabaseManage";
            object objType = CreateObject(assemblyPath, className);
            return (IDatabaseManage<T>)objType;
        }

        /// <summary>
        /// 表管理
        /// </summary>
        /// <typeparam name="T">连接对象类型 如:SqlConnection</typeparam>
        /// <returns></returns>
        public static ITableManage<T> CreateTableManage<T>() {

            string type = typeof(T).Name;

            FindAssemblyPath(type);

            string className = ".TableManage";
            object objType = CreateObject(assemblyPath, className);
            return (ITableManage<T>)objType;
        }

        /// <summary>
        /// 列管理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IColumnManage<T> CreateColumnManage<T>() {

            string type = typeof(T).Name;

            FindAssemblyPath(type);

            string className = ".ColumnManage";
            object objType = CreateObject(assemblyPath, className);
            return (IColumnManage<T>)objType;
        }

        /// <summary>
        /// 触发器管理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ITriggerManage<T> CreateTriggerManage<T>()
        {

            string type = typeof(T).Name;

            FindAssemblyPath(type);

            string className = ".TriggerManage";
            object objType = CreateObject(assemblyPath, className);
            return (ITriggerManage<T>)objType;
        }

        /// <summary>
        /// 视图管理
        /// </summary>
        /// <typeparam name="T">连接对象类型 如:SqlConnection</typeparam>
        /// <returns></returns>
        public static IViewManage<T> CreateViewManage<T>() {

            string type = typeof(T).Name;

            FindAssemblyPath(type);

            string className = ".ViewManage";
            object objType = CreateObject(assemblyPath, className);
            return (IViewManage<T>)objType;
        }

        public static IProcedureManage <T> CreateProcManage<T>(){

            string type = typeof(T).Name;

            FindAssemblyPath(type);

            string className = ".ProcedureManage";
            object objType = CreateObject(assemblyPath, className);
            return (IProcedureManage<T>)objType;
        }

    }
}
