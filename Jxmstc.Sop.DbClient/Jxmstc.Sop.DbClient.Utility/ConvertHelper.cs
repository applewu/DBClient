using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Jxmstc.Sop.DbClient.Utility
{
    public class ConvertHelper<T> where T : new()
    {
        /// <summary>
        /// DataTable转换为实体类
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> FillModel(DataTable dt)
        {
            // 定义集合
            IList<T> ts = new List<T>();

            // 获得此模型的类型
            Type type = typeof(T);

            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();

                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;

                    // 检查DataTable是否包含此列
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }

                ts.Add(t);
            }

            return ts;
        }

        #region 将实体类转换成DataTable

        /// <summary> 
        /// 将实体类转换成DataTable 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="i_objlist"></param> 
        /// <returns></returns> 
        //public static DataTable Fill<T>(IList<T> objlist)
        //{
        //    if (objlist == null || objlist.Count <= 0)
        //    {
        //        return null;
        //    }
        //    DataTable dt = new DataTable(typeof(T).Name);
        //    DataColumn column;
        //    DataRow row;

        //    System.Reflection.PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    foreach (T t in objlist)
        //    {
        //        if (t == null)
        //        {
        //            continue;
        //        }

        //        row = dt.NewRow();

        //        for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
        //        {
        //            System.Reflection.PropertyInfo pi = myPropertyInfo[i];

        //            string name = pi.Name;

        //            if (dt.Columns[name] == null)
        //            {
        //                column = new DataColumn(name, pi.PropertyType);
        //                dt.Columns.Add(column);
        //            }

        //            row[name] = pi.GetValue(t, null);
        //        }

        //        dt.Rows.Add(row);
        //    }
        //    return dt;
        //}

        #endregion

        

    }
}
