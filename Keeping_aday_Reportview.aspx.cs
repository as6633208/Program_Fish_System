using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using Fish;

public partial class Keeping_aday_Reportview : System.Web.UI.Page
{
    #region 報表載入
    protected void Page_Load(object sender, EventArgs e)
    {
        string str1 = "";
        string []str2 = null;
        str2 = Request.PathInfo.Split('/'); 
        foreach (string i in str2)
        {
            str1 = i.ToString();
        }
        HttpCookie cookie = new HttpCookie("Feed_aday_time");    //定義cookie對象以及名為Info的項
        cookie.Value = str1;
        DateTime dtNow = DateTime.Now;
        TimeSpan tsMinute = new TimeSpan(0, 0, 3, 0);
        cookie.Expires = dtNow + tsMinute;
        Response.Cookies.Add(cookie);
        if (!IsPostBack)
        {
            string RPP = str1;
            ReportParameter RP1 = new ReportParameter("Feed_Time", RPP);
            Keeping_aday_Viewer.LocalReport.SetParameters(new ReportParameter[] { RP1 });
            this.Keeping_aday_Viewer.LocalReport.Refresh();//重整
        }
        this.Keeping_aday_Viewer.LocalReport.Refresh();//重整      
    }
    #endregion
}