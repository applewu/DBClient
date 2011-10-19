using System;
using System.Data;
using System.Web.UI;


using Jxmstc.Sop.DbClient.Model;
using Jxmstc.Sop.DbClient.Utility;
using Jxmstc.Sop.DbClient.BLL;
using System.IO;
using System.Text;

namespace Jxmstc.Sop.DbClient.Web
{
    public partial class execSql : System.Web.UI.Page
    {
        //前台textarea的值
        static string sqltext;

        private Type tt;

        //去掉标签后的sql语句的值
        private static string likeSqltext;

        protected void Page_Load(object sender, EventArgs e)
        {
            tt = this.GetType();
           sqltext = Request["txtsql1"] as string;

           ClientScript.RegisterStartupScript(tt, "aa", " document.getElementById(\"txtsql1\").value=\"" +sqltext + "\";", true);

           likeSqltext = sqltext;
            
           if (!String.IsNullOrEmpty(likeSqltext)) {

               likeSqltext = likeSqltext.Replace("\r\n", "wzt").Replace ("\n","wzt");
               likeSqltext = likeSqltext.Replace("\"", "dbClient");
           }


            if (!Page.IsPostBack)
            {

                try
                {

                    ConnectionInfo connInfo = this.getConnInfo();
                    DataSet ds = Provider.getDBList(Session["DBType"].ToString(), connInfo);

                    if (ds.Tables.Count > 0) {

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            ddlDBList.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                        }
                    }

                }
                catch (IndexOutOfRangeException)
                {

                    Server.Transfer("Error.htm");
                    
                }

            }
        }

        private ConnectionInfo getConnInfo() {

            ConnectionInfo connInfo = new ConnectionInfo();

            try
            {
                connInfo.Server = Session["Server"].ToString();
                connInfo.UserName = Session["UserName"].ToString();
                connInfo.Pwd = Session["Pwd"].ToString();
            }
            catch (NullReferenceException) {

                Server.Transfer("Error.htm");
            }

            return connInfo;
        }

        /// <summary>
        ///  执行SQL语句
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRun_Click(object sender, ImageClickEventArgs e)
        {

            ClientScript.RegisterStartupScript(tt, "aa", " document.getElementById(\"txtsql1\").value=\"" + likeSqltext + "\";", true);

            gvResult.DataSource = null;
            gvResult.DataBind();
            lblMessage.Text = null;

            ExecSqlInfo execsqlInfo = new ExecSqlInfo();

            SQLMessage sqlMsg = new SQLMessage();

            sqlMsg.SqlMsgList.Clear();

            //---用户对左侧树做操作 均会使本页面下拉列表刷新 故可不做此判断---
                //判断在下拉列表中选择的数据库是否存在
                //if (!IsExistDB(ddlDBList.SelectedValue))
                //{

                //    Response.Write("<script>alert('所选择的数据库不存在(可能在您登陆后被删除了)!')</script>");
                //}
                //else
                //{

                    try
                    {
                        execsqlInfo.DataSet = Provider.findExec(Session["DBType"].ToString(), sqltext, ddlDBList.SelectedValue);


                        if (execsqlInfo.DataSet != null && execsqlInfo.DataSet.Tables.Count > 0)
                        {

                            gvResult.DataSource = execsqlInfo.DataSet;
                            gvResult.DataBind();

                            #region 影响行数
                            for (int i = 0; i < execsqlInfo.DataSet.Tables.Count; i++)
                            {

                                int rowLength = execsqlInfo.DataSet.Tables[i].Rows.Count;
                                sqlMsg.SqlMsgList.Add("(所影响的行数为：" + rowLength.ToString() + "行)");

                            }
                            #endregion

                        }

                        execsqlInfo.Success = true;
                        execsqlInfo.Message = sqlMsg.IsEmptySQLMessage() ? "命令已成功完成。" : sqlMsg.GetSQLMessageString();

                     
                    }
                    catch (Exception exception) {

                        execsqlInfo.Success = false;
                        execsqlInfo.Message = exception.Message;

                    }   
                        //在标签中显示影响行数
                    lblMessage.Text = execsqlInfo.Message;
                //}


        }

        #region 数据库是否存在

        ////数据库是否存在
        //private bool IsExistDB(string p)
        //{
        //    Manage manage=new Manage ();
        //    ConnectionInfo connInfo=this.getConnInfo ();

        //    return manage.IsExistDataBase(p, connInfo);
        //}

        #endregion

        /// <summary>
        /// 将执行结果导出到Excel中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            Response.Clear();
            Response.Buffer = false;
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=Result.xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.ContentType = "application/ms-excel";
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            this.EnableViewState = false;

            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            HtmlTextWriter oHtmlTextWriter = new HtmlTextWriter(oStringWriter);

            gvResult.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }


    }
}
