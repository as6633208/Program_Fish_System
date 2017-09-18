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

public partial class Pool_feed_consumption : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string str1 = "";
        string[] str2 = null;
        str2 = Request.PathInfo.Split('/');//Request.Url.Query	
        foreach (string i in str2)
        {
            str1 = i.ToString();
        }
        HttpCookie cookie = new HttpCookie("pool_feed");   
        cookie.Value = HttpUtility.UrlEncode(str1);
        DateTime dtNow = DateTime.Now;
        TimeSpan tsMinute = new TimeSpan(0, 0, 3, 0);
        cookie.Expires = dtNow + tsMinute;
        Response.Cookies.Add(cookie);
        if (!IsPostBack)
        {
            Char delimiter = '-';//Server.UrlDecode(Request.Cookies["Name"].value
            String[] substrings = str1.Split(delimiter);//切割字串

            // string RPP = str1;
            ReportParameter time_get = new ReportParameter("Time", substrings[1] + "年" + substrings[2] + "月");//傳入時間
            ReportParameter company = new ReportParameter("Pool", substrings[0]);//傳入時間[@Abbreviation_Company]
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { time_get, company });
            this.ReportViewer1.LocalReport.Refresh();//重整
        }
    }
}