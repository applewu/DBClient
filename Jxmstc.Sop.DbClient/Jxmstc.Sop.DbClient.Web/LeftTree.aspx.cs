using System;
using System.Collections;
using System.Collections.Generic;
using Jxmstc.Sop.DbClient.Web.Common;
using Jxmstc.Sop.DbClient.Model;

using Jxmstc.Sop.DbClient.BLL;
using System.Data;

namespace Jxmstc.Sop.DbClient.Web
{
    public partial class LeftTree : System.Web.UI.Page
    {
        static ConnectionInfo connInfo = new ConnectionInfo();
        static ManageBase manage;

        protected void Page_Load(object sender, EventArgs e)
        {
            findConn();
            AjaxPro.Utility.RegisterTypeForAjax(typeof(LeftTree));
        }

        #region 显示树的基本方法

        private void findManage()
        {

            if (Session["DBType"].ToString() == "SQL Server2005/2008")
            {
                manage = new Manage();
            }
            else if (Session["DBType"].ToString() == "MySql")
            {

                manage = new ManageForMySQL();
            }
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public string CreateTree()
        {

            if (connInfo == null)
            {

                findConn();
            }

             IList<DataBaseInfo> DBList = Provider.findDBManage(Session["DBType"].ToString(), connInfo);

             string json=InfoToJson.getJson(DBList);

             return json;
        }

        /// <summary>
        /// 为连接信息的实体对象赋值
        /// </summary>
        private void findConn()
        {
            try
            {
                connInfo.Server = Session["Server"].ToString();
                connInfo.UserName = Session["UserName"].ToString();
                connInfo.Pwd = Session["Pwd"].ToString();
            }
            catch (NullReferenceException)
            {

                Server.Transfer("Error.htm");
            }
            //connInfo.Server = @"WWW-955D12A1603\WZT2009";
            //connInfo.UserName = "sa";
            //connInfo.Pwd = "Pass@word";

        }

        #endregion


        //-----仅实现删除数据库、删除表--------------
        [AjaxPro.AjaxMethod]
        public void Delete(string nodeType, string nodeName, string parentNodeName, string preParentNodeName)
        {

            if (connInfo == null)
            {

                findConn();
            }

            if (manage == null) {

                findManage();
            }
            
            if (nodeType == "folder_db")
            {

                manage.DeleteObject(connInfo, nodeName);
            }
            else if ((nodeType == "folder_table") || (nodeType == "folder_view") || (nodeType == "folder_procedure"))
            {

                manage.DeleteObject(connInfo, nodeType, parentNodeName, nodeName);
            }

        }

        [AjaxPro.AjaxMethod]
        public void Create(string nodeName)
        {
            if (manage == null)
            {

                findManage();
            }
            if (connInfo == null)
            {

                findConn();
            }

            manage.CreateObject(connInfo, nodeName);
        }

        //
        [AjaxPro.AjaxMethod]
        public void Rename(string nodeType, string oldName, string newName, string parentNodeName)
        {
            if (manage == null)
            {

                findManage();
            }

            if (connInfo == null)
            {

                findConn();
            }

            if (nodeType == "DataBase")
            {

                manage.RenameObject(connInfo, oldName, newName);
            }
            else if (nodeType == "Table")
            {

                manage.RenameObject(connInfo, oldName, newName, parentNodeName);
            }
        }

        [AjaxPro.AjaxMethod]
        public string ShowScript(string name, string dbName) {

            if (manage == null)
            {

                findManage();
            }

            if (connInfo == null)
            {

                findConn();
            }

            return manage.ShowScript(dbName, connInfo, name);
        }

    }
}
