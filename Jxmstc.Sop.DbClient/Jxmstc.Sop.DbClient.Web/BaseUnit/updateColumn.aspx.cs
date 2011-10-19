using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jxmstc.Sop.DbClient.Web.BaseUnit
{
    public partial class updateColumn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string data = Request.QueryString["data"];

            int i = data.IndexOf('(');
            int j = data.IndexOf(')');
            string name=data.Substring(0, i);
            string typestring = data.Substring(i + 1, j - i);
            string isnullable=data.Substring (j+2,data.Length -j-3);
            
            
        }
    }
}
