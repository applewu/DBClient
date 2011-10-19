using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Jxmstc.Sop.DbClient.Model;
using System.Text;

namespace Jxmstc.Sop.DbClient.Web.Common
{
    public class InfoToJson
    {
        /// <summary>
        /// IList<DataBaseInfo>转化为基于jsTree的Json格式字符串
        /// </summary>
        /// <param name="dblist">IList<DataBaseInfo>对象</param>
        /// <returns>基于jsTree的Json格式字符串</returns>
        public static string getJson(IList<DataBaseInfo> dblist)
        {
            StringBuilder str = new StringBuilder();

            str.Append("[");

            str.Append("{attributes:{id:\"folder_db\",rel:\"root\"},state:\"open\",data:\"数据库\",children:[");

            for (int i = 0; i < dblist.Count; i++)
            {

                DataBaseInfo dbInfo = dblist[i];

                if ((dbInfo.TableList != null) || (dbInfo.ViewList != null))
                {

                    str.Append("{attributes:{id:\"" + dbInfo.DbName + "\"");
                    str.Append(",type:\"" + "DataBase\",rel:\"folder\"");
                    str.Append("},state:\"close\",data:\"" + dbInfo.DbName + "\" ,");

                    str.Append("children:[");

                    str.Append("{attributes:{id:\"folder_table\",type:\"folder_table\",rel:\"folder\"},state:\"close\",data:\"表\",children:[");

                    #region 数据库中的表信息
                    IList<TableInfo> tableList = dbInfo.TableList;

                    for (int j = 0; j < tableList.Count; j++)
                    {

                        TableInfo table = tableList[j];
                        str.Append("{attributes:{id:\"" + table.TableName + "\"");
                        str.Append(",type:\"" + "Table\",rel:\"folder\"");
                        str.Append("},state:\"close\",data:\"" + table.TableName + "\" ,");

                        //string iconTable = @"../image/obj_table.gif";

                        //str.Append("\"},state:\"close\",data:{title:\"" + table.TableName + "\" ,icon:\""+iconTable+"\"},");
                        str.Append("children:[");

                        str.Append("{attributes:{id:\"folder_col\",rel:\"root\"},state:\"open\",data:\"列\",children:[");

                        #region 表的列信息
                        if (table.ColList != null)
                        {

                            IList<ColumnInfo> colList = table.ColList;
                            for (int k = 0; k < colList.Count; k++)
                            {

                                ColumnInfo col = colList[k];
                                str.Append("{attributes:{id:\"" + col.Name + "\"");

                                string IsNull = null;

                                if (col.Isnullable == "0")
                                {
                                    IsNull = "not null";

                                }
                                else
                                {

                                    IsNull = "null";
                                }

                                str.Append(",type:\"" + "Column\",rel:\"root2\"");
                                str.Append("},state:\"close\",data:\"" + col.Name + "(" + col.Typestring + "," + IsNull + ")");
                                str.Append("\" " + "}");

                                if (k < colList.Count - 1)
                                {
                                    str.Append(",");
                                }
                            }

                            str.Append("]},");

                        }


                        #endregion

                        str.Append("{attributes:{id:\"folder_tri\",rel:\"root\"},state:\"open\",data:\"触发器\",children:[");

                        #region 表的触发器信息
                        if (table.TriList != null)
                        {

                            IList<TriggerInfo> triList = table.TriList;
                            for (int m = 0; m < triList.Count; m++)
                            {

                                TriggerInfo tri = triList[m];
                                str.Append("{attributes:{id:\"" + tri.Name + "\"");
                                str.Append(",type:\"" + "Trigger\"");
                                str.Append("},state:\"close\",data:\"" + tri.Name);
                                str.Append("\" " + "}");

                                if (m < triList.Count - 1)
                                {
                                    str.Append(",");
                                }
                            }

                            str.Append("]},");
                        }

                        #endregion


                        str.Append("]");
                        str.Append("}");

                        if (j < tableList.Count - 1)
                        {

                            str.Append(",");
                        }
                    }

                    #endregion


                    str.Append("]},");


                    str.Append("{attributes:{id:\"folder_view\",rel:\"folder\"},state:\"close\",data:\"视图\",children:[");

                    #region 数据库中的视图信息
                    IList<ViewInfo> viewList = dbInfo.ViewList;

                    for (int m = 0; m < viewList.Count; m++)
                    {

                        ViewInfo view = viewList[m];
                        str.Append("{attributes:{id:\"" + view.Name + "\"");
                        str.Append(",type:\"" + "View\",rel:\"folder\"");
                        str.Append("},state:\"close\",data:\"" + view.Name + "\" }");

                        if (m < viewList.Count - 1)
                        {

                            str.Append(",");
                        }
                    }



                    #endregion

                    str.Append("]},");


                    str.Append("{attributes:{id:\"folder_procedure\",rel:\"folder\"},state:\"close\",data:\"存储过程\",children:[");

                    #region 数据库中的存储过程信息

                    IList<ProcedureInfo> procList = dbInfo.ProcList;


                    for (int n = 0; n < procList.Count; n++)
                    {

                        ProcedureInfo proc = procList[n];
                        str.Append("{attributes:{id:\"" + proc.Name + "\"");
                        str.Append(",type:\"" + "Procedure\",rel:\"folder\"");
                        str.Append("},state:\"close\",data:\"" + proc.Name + "\" }");

                        if (n < procList.Count - 1)
                        {

                            str.Append(",");
                        }
                    }

                    #endregion

                        str.Append("]}");

                    str.Append("]}");


                    if (i < dblist.Count - 1)
                    {

                        str.Append(",");
                    }

                }

            }

            str.Append("]}]");

            return str.ToString();

        }
    }
}
