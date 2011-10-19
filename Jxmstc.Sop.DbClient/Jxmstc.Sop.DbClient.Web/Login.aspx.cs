using System;

using Jxmstc.Sop.DbClient.Model;

using Jxmstc.Sop.DbClient.BLL;

namespace Jxmstc.Sop.DbClient.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //为用户名文本框添加失去焦点事件
                txtUserName.Attributes.Add("onBlur", "getValue()");
            }
        }

        /// <summary>
        /// 点击“登陆”按钮的触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string dbType = ddlDBType.SelectedValue;

            ConnectionInfo connInfo = new ConnectionInfo();
            connInfo.Server = txtServer.Text;
            connInfo.UserName = txtUserName.Text;
            connInfo.Pwd = txtPwd.Text;

            this.SavaInfo();


            //记住密码
            if (ChkPwd.Checked == true)
            {
                string strCookieName = txtServer.Text + "_" + txtUserName.Text;

                Response.Cookies[strCookieName].Value = txtPwd.Text;
                Response.Cookies[strCookieName].Expires = DateTime.Now.AddMonths(60);
                //------Response.Cookies["123_123"].Value = "Hi";
            }

            try
            {
                Provider.findConn(dbType, connInfo);
                Response.Redirect("main.html",false );
            }
            catch (Exception ex) {

                lblMessage.Text = ex.Message;
            }

        }

        /// <summary>
        /// 将信息保存到Session中
        /// </summary>
        private void SavaInfo()
        {
            Session["DBType"] = ddlDBType.SelectedValue;
            Session["Server"] = txtServer.Text;
            Session["UserName"] = txtUserName.Text;
            Session["Pwd"] = txtPwd.Text;

        }
    }
}
