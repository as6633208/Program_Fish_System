using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Pool_feed_consumption 的摘要描述
/// </summary>
public class Pool_feed_consumption
{
    public Pool_feed_consumption()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //

    }
    private const String COL_1 = "日期";
    private const String COL_2 = "摘要";
    private const String COL_3 = "時段";
    private const String COL_4 = "包數";
    private const String COL_5 = "公斤數";
    public DataTable Get(string str1)//
    {
        string str2 = HttpUtility.UrlDecode(str1);
        Char delimiter = '-';
        String[] substrings = str2.Split(delimiter);//切割字串
        float packs_statistics = 0;//packs  statistics
        float kg_statistics = 0;//kg statistics
        #region 飼料使用(池)
        SqlCommand cmd = new SqlCommand(@"SELECT          Feed.date,sum(Feed.[Fodder_number] )as total, sum(Feed.[Fodder_number]*30 ) as kg,(
                    STUFF( (
                             SELECT ',' + Fodder.[Fodder_name] , ',(' +  CAST(T2.[Fodder_number] AS varchar) + ')'    
                             FROM   Feed  T2      INNER JOIN
							 Fodder ON T2.Fodder_id = Fodder.Fodder_id        
                           WHERE (CONVERT(varchar(10), T2.date, 111) LIKE '%' + @time + '%') and (T2.Pool_id=@pool) and (T2.date = Feed.date) and T2.Fodder_id = Fodder.Fodder_id
						     ORDER BY   T2.DayTime DESC 
						    FOR XML PATH('')), 1, 1, '')) AS [Fodder_number],(
                    STUFF( (
                             SELECT  ',' +  [DayTime] 
                             FROM   Feed  T3        
                           WHERE (CONVERT(varchar(10), T3.date, 111) LIKE '%' + @time + '%') and (T3.Pool_id=@pool) and (T3.date = Feed.date) 
						    ORDER BY   T3.DayTime DESC 
						    FOR XML PATH('')), 1, 1, '')) AS [DayTime]							
                    FROM            Feed 
                    WHERE          (CONVERT(varchar(10), Feed.date, 111) LIKE '%' + @time + '%') and (Feed.Pool_id=@pool)
                     GROUP BY Feed.date");
        cmd.Parameters.Add("@time", SqlDbType.NVarChar, 30).Value = substrings[1] + "/" + substrings[2];
        cmd.Parameters.Add("@pool", SqlDbType.NVarChar, 10).Value = substrings[0];
        DataTable Pool_Feed_items = Fish.SqlHelper.cmdTable(cmd);
        //Convert.ToDateTime(dr["欄位名稱"]).ToString("yyyy/MM/dd HH:mm:ss");
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn(COL_1));
        dt.Columns.Add(new DataColumn(COL_2));
        dt.Columns.Add(new DataColumn(COL_3));
        dt.Columns.Add(new DataColumn(COL_4));
        dt.Columns.Add(new DataColumn(COL_5));
        #endregion
        #region 飼料顯示
        for (int i = 0; i < Pool_Feed_items.Rows.Count; i++)
        {

            DataRow dr = dt.NewRow();
            dr[COL_1] = Convert.ToDateTime(Pool_Feed_items.Rows[i][0]).ToString("yyyy/MM/dd");//Feed_Purchase_items.Rows[i][2];//time
            dr[COL_2] = Pool_Feed_items.Rows[i][3];//摘要
            dr[COL_3] = Pool_Feed_items.Rows[i][4];//時段
            dr[COL_4] = Pool_Feed_items.Rows[i][1];//包數
            dr[COL_5] = Pool_Feed_items.Rows[i][2];//kg數
            dt.Rows.Add(dr);
            packs_statistics += float.Parse(Pool_Feed_items.Rows[i][1].ToString());
            kg_statistics += float.Parse(Pool_Feed_items.Rows[i][2].ToString());
        }
        #endregion
        DataRow dr_tatal = dt.NewRow();
        dr_tatal[COL_1] = "合計";
        dr_tatal[COL_4] = packs_statistics;
        dr_tatal[COL_5] = kg_statistics;
        dt.Rows.Add(dr_tatal);

        return dt;
    }
}

/** 開心
 SELECT          Feed.date,(
                    STUFF( (
                             SELECT  ',' +  [DayTime] , ',' + Fodder.[Fodder_name] ,',' +     CAST(T3.[Fodder_number] AS varchar)     
                             FROM   Feed  T3    INNER JOIN
                Fodder ON T3.Fodder_id = Fodder.Fodder_id         
                           WHERE (CONVERT(varchar(10), T3.date, 111) LIKE '%2016/01%') and (T3.Pool_id='F2') and (T3.date = Feed.date) and T3.Fodder_id = Fodder.Fodder_id
						    ORDER BY   T3.DayTime DESC 
						    FOR XML PATH('')), 1, 1, '')) AS [DayTime]
							
FROM            Feed 
WHERE          (CONVERT(varchar(10), Feed.date, 111) LIKE '%2016/01%') and (Feed.Pool_id='F2')
 GROUP BY Feed.date

 * **/
/**
 * V2開心
SELECT          Feed.date,sum(Feed.[Fodder_number] )as total, sum(Feed.[Fodder_number]*30 ) as kg,(
                    STUFF( (
                             SELECT ',' + Fodder.[Fodder_name] , ',(' +  CAST(T2.[Fodder_number] AS varchar) + ')'    
                             FROM   Feed  T2      INNER JOIN
							 Fodder ON T2.Fodder_id = Fodder.Fodder_id        
                           WHERE (CONVERT(varchar(10), T2.date, 111) LIKE '%' + @time + '%') and (T2.Pool_id=@pool) and (T2.date = Feed.date) and T2.Fodder_id = Fodder.Fodder_id
						     ORDER BY   T2.DayTime DESC 
						    FOR XML PATH('')), 1, 1, '')) AS [Fodder_number],(
                    STUFF( (
                             SELECT  ',' +  [DayTime] 
                             FROM   Feed  T3        
                           WHERE (CONVERT(varchar(10), T3.date, 111) LIKE '%' + @time + '%') and (T3.Pool_id=@pool) and (T3.date = Feed.date) 
						    ORDER BY   T3.DayTime DESC 
						    FOR XML PATH('')), 1, 1, '')) AS [DayTime]

							
FROM            Feed 
WHERE          (CONVERT(varchar(10), Feed.date, 111) LIKE '%' + @time + '%') and (Feed.Pool_id=@pool)
 GROUP BY Feed.date



    */
