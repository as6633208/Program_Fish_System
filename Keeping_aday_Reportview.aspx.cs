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
        //Feed_aday a = new Feed_aday();
        //Get(str1);
        //SqlCommand cmd = new SqlCommand(@"SELECT Pool_id, date FROM Feed WHERE (date = '2017-07-18') GROUP BY Pool_id, date");
        //cmd.Parameters.Add("@date", SqlDbType.DateTime2).Value = "2017-07-18";//日期
        //DataTable Keeping_aday_Items = Fish.SqlHelper.cmdTable(cmd);

        //DataTable Keeping_aday_Items = SqlHelper.ExcuteTable(@"SELECT Pool_id, date FROM Feed WHERE (date = '2017-07-18') GROUP BY Pool_id, date");//撈單號 顯示
        //ReportDataSource source = new ReportDataSource("Feed_aday", Keeping_aday_Items); //db關聯來源
        //this.Keeping_aday_Viewer.LocalReport.DataSources.Clear();//將資料清空
        //this.Keeping_aday_Viewer.LocalReport.DataSources.Add(source);//將資料加進去
        //foreach (DataRow dr in Keeping_aday_Items.Rows)//同意思for (int i = 0; i < Shippers_Items.Rows.Count; i++)
        //{
        //    //total_price += Int32.Parse(dr["new_price"].ToString());
        //    //phone_number = dr["cust_phone"].ToString();
        //
        //    ReportParameter Pools = new ReportParameter("pools", dr["Pool_id"].ToString());//打印Pools
        //    this.Keeping_aday_Viewer.LocalReport.SetParameters(new ReportParameter[] { Pools });
        //}

        //this.Keeping_aday_Viewer.LocalReport.Refresh();//重整
        //this.Keeping_aday_Viewer.RefreshReport();
        //Response.Write("<Script language='JavaScript'>alert('" + str1 + "');</Script>");
        /*
         *         SqlCommand cmd = new SqlCommand(@"SELECT COUNT(*) AS count FROM Admin_List WHERE (Password = @a) AND (Account = @b)");
        cmd.Parameters.Add("@a", SqlDbType.NVarChar, 50).Value = a;
        cmd.Parameters.Add("@b", SqlDbType.NVarChar, 50).Value = b;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
         */
    }
    #endregion
   

    /* 篩選日期與池號
SELECT          Pool_id, date
FROM              Feed
WHERE          (date = '2017 / 7 / 27')
GROUP BY   Pool_id, date
*/
    /*篩選所有資料
     SELECT Feed.Pool_id,Feed.date,Feed.Fish_detail_id,SUM(medicine_number) AS medicine_number, 
     Fish_detail.Fish_AVGweight, Fish_detail.Move_date, Fish_detail.number, Fish_detail.Fish_size, Fish.Spawning_date, 
                                Fish.Insert_fry_date, Fish.Insert_year, Fish.Fry_weight, Fish_company.company_abbreviation, 
                                Fish_kind.kind_name,


    SUM(Feed.Fodder_number) AS Fodder_number,(  
    STUFF( (           
             SELECT  ',' + [Bait]         
             FROM   Feed  T2           
             WHERE  T2.Pool_id = Feed.Pool_id and (date = '2017 / 7 / 27') 
             FOR XML PATH('')          
             ), 1, 1, ''       
           )
    ) AS [Bait],(  
    STUFF( (           
             SELECT  ',' + [Medicine_name]         
             FROM   Feed T4 INNER JOIN
                                Medicine ON T4.Medicine_id = Medicine.Medicine_id 
             WHERE  (T4.date = '2017 / 7 / 27') AND T4.Pool_id = 'A5' 
             FOR XML PATH('')          
             ), 1, 1, ''       
           )
    ) AS [Medicine_name],(  
    STUFF( (           
             SELECT ',' + Fodder.[Fodder_name] 
             FROM     Feed T3 INNER JOIN
                                Fodder ON T3.Fodder_id = Fodder.Fodder_id  
            WHERE  (T3.date = '2017 / 7 / 27') AND T3.Pool_id = 'A5' 
             FOR XML PATH('')          
             ), 1, 1, ''       
           )
    ) AS [Fodder_name]
    FROM Feed   INNER JOIN
                                Fish_detail ON Feed.Fish_detail_id = Fish_detail.Fish_detail_id INNER JOIN
                                Fish ON Fish_detail.Fish_id = Fish.Fish_id INNER JOIN
                                Fish_company ON Fish.Fish_company_id = Fish_company.Fish_company_id INNER JOIN
                                Fish_kind ON Fish.Fish_kind_id = Fish_kind.Fish_kind_id

    WHERE (date = '2017 / 7 / 27') AND Pool_id = 'A5' 
    GROUP BY Feed.Pool_id,Feed.date,Feed.Fish_detail_id,Fish_detail.Fish_AVGweight, Fish_detail.Fish_AVGweight, Fish_detail.Move_date, Fish_detail.number, Fish_detail.Fish_size, Fish.Spawning_date, 
                                Fish.Insert_fry_date, Fish.Insert_year, Fish.Fry_weight, Fish_company.company_abbreviation, 
                                Fish_kind.kind_name

     */



}