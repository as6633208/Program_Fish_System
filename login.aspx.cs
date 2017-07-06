using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //gvBind(GridView1);
            txt_input.Attributes.Add("OnKeyPress", "");
            txt_input.Attributes.Add("OnKeyPress", "if(event.keyCode>=48 && event.keyCode<=57) return true; else return false;");
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        //SUCCESS
           Response.Write( txt_input.Text + "<hr/>");
    }
}