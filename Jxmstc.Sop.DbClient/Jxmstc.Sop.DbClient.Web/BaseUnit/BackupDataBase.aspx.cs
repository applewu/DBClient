using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Jxmstc.Sop.DbClient.BLL;
using Jxmstc.Sop.DbClient.Model;
using System.Text;
using System.Globalization;
using System.IO;

namespace Jxmstc.Sop.DbClient.Web.BaseUnit
{
    public partial class BackupDataBase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblDBName.Text = Request.QueryString["DBName"].ToString();
        }

        public ConnectionInfo getConnInfo()
        {

            ConnectionInfo connInfo = new ConnectionInfo();
            connInfo.Server = Session["Server"].ToString();
            connInfo.UserName = Session["UserName"].ToString();
            connInfo.Pwd = Session["Pwd"].ToString();

            return connInfo;
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            this.Backup(lblDBName .Text );
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="dbName">数据库名</param>
        public void Backup(string dbName)
        {
            Manage manage = new Manage();
            ConnectionInfo conn = this.getConnInfo();

            if (manage.IsExistDataBase(dbName, conn))
            {

                StringBuilder fileFullName = new StringBuilder();

                fileFullName.Append(Server.MapPath(@"~\Backupfolder\"));//备份文件的路径


                string name = System.DateTime.Now.ToString("yyyyMMdd_HHmmss", DateTimeFormatInfo.InvariantInfo) + ".bak";
                fileFullName.Append(name);//备份文件的文件名


                manage.BackupDataBase(dbName, this.getConnInfo(), fileFullName.ToString());

                this.DownLoad(fileFullName.ToString());
            }
            else
            {

                Response.Write("<script>alert('该数据库不存在！')</script>");
            }


        }

        /// <summary>
        /// 将服务器文件下载到本地
        /// </summary>
        /// <param name="fileFullName"></param>
        public void DownLoad(string fileFullName)
        {

            try
            {

                FileInfo DownLoadFile = new FileInfo(fileFullName);

                if (DownLoadFile.Exists)
                {

                    System.Web.HttpContext.Current.Response.Clear();

                    System.Web.HttpContext.Current.Response.ClearHeaders();
                    System.Web.HttpContext.Current.Response.Buffer = false;
                    System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                    System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(DownLoadFile.FullName, System.Text.Encoding.ASCII));
                    System.Web.HttpContext.Current.Response.AppendHeader("Content-Length", DownLoadFile.Length.ToString());
                    System.Web.HttpContext.Current.Response.WriteFile(DownLoadFile.FullName);
                    System.Web.HttpContext.Current.Response.Flush();
                    System.Web.HttpContext.Current.Response.End();
                }

            }
            catch (Exception)
            {

                //Response.Redirect("../execSql.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Server.Transfer("~/execSql.aspx");
            Response.Redirect("../execSql.aspx");
        }
    }
}
