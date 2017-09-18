using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Consumption 的摘要描述
/// </summary>
public class Consumption
{
    public Consumption()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }
    private const String COL_1 = "日期";
    private const String COL_2 = "耗用";
    private const String COL_3 = "進料";
    public DataTable Get(string str1)//
    {
        string str2 = HttpUtility.UrlDecode(str1);
        Char delimiter = '-';//Server.UrlDecode(Request.Cookies["Name"].value
        String[] substrings = str2.Split(delimiter);//切割字串
        float use = 0;//使用飼料
        float add = 0;//增加飼料
        string[] arr_pools = new string[5];
        #region 飼料顯示全部
        //SqlCommand cmd = new SqlCommand(@"SELECT Pool_id, date FROM Feed WHERE (date = '" + str1 + "') GROUP BY Pool_id, date");
        SqlCommand cmd = new SqlCommand(@"SELECT          Fodder.Fodder_name, Fish_company.company_name, Feed.date, 'reduce' AS status, SUM(Feed.Fodder_number) 
                            AS Feed, 0 AS Purchase
FROM              Feed INNER JOIN
                            Fodder ON Feed.Fodder_id = Fodder.Fodder_id INNER JOIN
                            Fish_company ON Fodder.Fish_company_id = Fish_company.Fish_company_id
WHERE          (CONVERT(varchar(10), Feed.date, 111) LIKE '%' + @time + '%') AND (Fodder.Fodder_name = @Fodder_name) AND 
                            (Fish_company.company_name = @company_name)
GROUP BY   Feed.date, Fish_company.company_name, Fodder.Fodder_name
UNION ALL
SELECT          Fodder_1.Fodder_name, Fish_company_1.company_name, Feed_Purchase.date, 'increase' AS status, 0 AS Expr1, 
                            SUM(Feed_Purchase.number) AS Expr2
FROM              Feed_Purchase INNER JOIN
                            Fodder AS Fodder_1 ON Feed_Purchase.Fodder_id = Fodder_1.Fodder_id INNER JOIN
                            Fish_company AS Fish_company_1 ON Fodder_1.Fish_company_id = Fish_company_1.Fish_company_id
WHERE          (CONVERT(varchar(10), Feed_Purchase.date, 111) LIKE '%' + @time + '%') AND (Fodder_1.Fodder_name = @Fodder_name) AND 
                            (Fish_company_1.company_name = @company_name)
GROUP BY   Fodder_1.Fodder_name, Fish_company_1.company_name, Feed_Purchase.date
ORDER BY   Feed.date");
        cmd.Parameters.Add("@time", SqlDbType.NVarChar,30).Value = substrings[2]+"/"+substrings[3];
        cmd.Parameters.Add("@Fodder_name", SqlDbType.NVarChar, 10).Value =  substrings[1];
        cmd.Parameters.Add("@company_name", SqlDbType.NVarChar, 30).Value = substrings[0];
        DataTable Feed_Purchase_items = Fish.SqlHelper.cmdTable(cmd);
        //Convert.ToDateTime(dr["欄位名稱"]).ToString("yyyy/MM/dd HH:mm:ss");
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn(COL_1));
        dt.Columns.Add(new DataColumn(COL_2));
        dt.Columns.Add(new DataColumn(COL_3));

        #endregion
        #region 飼料顯示
        for (int i = 0; i < Feed_Purchase_items.Rows.Count; i++)
        {

            DataRow dr = dt.NewRow();
            dr[COL_1] = Convert.ToDateTime(Feed_Purchase_items.Rows[i][2]).ToString("yyyy/MM/dd");//Feed_Purchase_items.Rows[i][2];//time
            dr[COL_2] = Feed_Purchase_items.Rows[i][4];//Feed
            dr[COL_3] = Feed_Purchase_items.Rows[i][5];//Purchase
            dt.Rows.Add(dr);
            use += float.Parse(Feed_Purchase_items.Rows[i][4].ToString());
            add += float.Parse(Feed_Purchase_items.Rows[i][5].ToString());
        }
        #endregion
        DataRow dr_tatal = dt.NewRow();
        dr_tatal[COL_1] = "合計";
        dr_tatal[COL_2] = use;
        dr_tatal[COL_3] = add;
        dt.Rows.Add(dr_tatal);

        return dt;
    }
}
/*
SELECT          Feed.Fodder_number, Fodder.Fodder_name, Fish_company.company_name, Feed.Feed_id, Feed.date
FROM			Feed INNER JOIN
                Fodder ON Feed.Fodder_id = Fodder.Fodder_id INNER JOIN
				Fish_company ON Fodder.Fish_company_id = Fish_company.Fish_company_id
WHERE          (CONVERT(varchar(10), Feed.date, 111) LIKE '%2017/09%') and  Fodder.Fodder_name = '合2' and Fish_company.company_name='三合益'
ORDER BY Feed.date ASC
     */

/*
SELECT          Feed_Purchase.number, Feed_Purchase.Fodder_id, Fodder.Fodder_name, Fish_company.company_name, 
                            Feed_Purchase.date
FROM              Feed_Purchase INNER JOIN
                            Fodder ON Feed_Purchase.Fodder_id = Fodder.Fodder_id INNER JOIN
                            Fish_company ON Fodder.Fish_company_id = Fish_company.Fish_company_id
WHERE             (CONVERT(varchar(10), Feed_Purchase.date, 111) LIKE '%2017/09%') and  Fodder.Fodder_name = '屏2' and Fish_company.company_name='屏科'
 */


/*
SELECT  Fodder.Fodder_name, Fish_company.company_name,  Feed.date,'reduce' status,sum(Feed.Fodder_number) as Feed,0 Purchase
FROM			Feed INNER JOIN
                Fodder ON Feed.Fodder_id = Fodder.Fodder_id INNER JOIN
				Fish_company ON Fodder.Fish_company_id = Fish_company.Fish_company_id
WHERE          (CONVERT(varchar(10), Feed.date, 111) LIKE '%2017/09%') and  Fodder.Fodder_name = '屏2' and Fish_company.company_name='屏科'
group by Feed.date,Fish_company.company_name, Fodder.Fodder_name
UNION ALL
SELECT       Fodder.Fodder_name, Fish_company.company_name,
                            Feed_Purchase.date,'increase' status,0,sum(Feed_Purchase.number)
FROM              Feed_Purchase INNER JOIN
					Fodder ON Feed_Purchase.Fodder_id = Fodder.Fodder_id INNER JOIN
                            Fish_company ON Fodder.Fish_company_id = Fish_company.Fish_company_id
WHERE             (CONVERT(varchar(10), Feed_Purchase.date, 111) LIKE '%2017/09%') and  Fodder.Fodder_name = '屏2' and Fish_company.company_name='屏科'
group by   Fodder.Fodder_name, Fish_company.company_name, Feed_Purchase.date
ORDER BY Feed.date ASC
 */
