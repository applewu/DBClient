using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using Jxmstc.Sop.DbClient.Model;
using Jxmstc.Sop.DbClient.BLL;
using System.Globalization;
using System.IO;

namespace Jxmstc.Sop.DbClient.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(_Default));
        }

        public ConnectionInfo  getConnInfo() {

            ConnectionInfo connInfo = new ConnectionInfo();
            connInfo.Server = Session["Server"].ToString();
            connInfo.UserName = Session["UserName"].ToString();
            connInfo.Pwd = Session["Pwd"].ToString();

            return connInfo;
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="dbName">数据库名</param>
        public void BackupDataBase(string dbName)
        {
              Manage manage = new Manage();
            ConnectionInfo conn=this.getConnInfo ();

            if (manage.IsExistDataBase( dbName, conn))
            {

                StringBuilder fileFullName = new StringBuilder();

                fileFullName.Append(Server.MapPath(@"~\Backupfolder\"));//备份文件的路径


                string name = System.DateTime.Now.ToString("yyyyMMdd_HHmmss", DateTimeFormatInfo.InvariantInfo) + ".bak";
                fileFullName.Append(name);//备份文件的文件名


                manage.BackupDataBase(dbName,this.getConnInfo(), fileFullName.ToString());

                this.DownLoad(fileFullName.ToString());
            }
            else {

                Response.Write("<script>alert('该数据库不存在！')</script>");
            }
            

        }

        /// <summary>
        /// 将服务器文件下载到本地
        /// </summary>
        /// <param name="fileFullName"></param>
        public void DownLoad(string fileFullName) {

            try { 
            
                FileInfo DownLoadFile=new FileInfo (fileFullName );

                if(DownLoadFile .Exists ){

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
            
            }catch (Exception){
            
                
            }
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {

            this.BackupDataBase(Hidden1.Value);
        }

        //清空表数据
        [AjaxPro.AjaxMethod]
        public void clearData(string tableName) { 
        
        
        }

    }
}
