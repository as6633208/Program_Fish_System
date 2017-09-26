using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// fish_change 的摘要描述
/// </summary>
public class fish_change
{
    public fish_change()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }
    private const String COL_1 = "入魚";
    private const String COL_2 = "轉池收益";
    private const String COL_3 = "分養收益";
    private const String COL_4 = "出魚";
    private const String COL_5 = "總重";
    private const String COL_6 = "轉池耗損";
    private const String COL_7 = "分養耗損";
    private const String COL_8 = "耗損";
    private const String COL_9 = "餘額";

    public DataTable Get()//string str1
    {
        string date = "2015/06/";
        string pool = "F2"; 
        int total_num = 0;
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn(COL_1));
        dt.Columns.Add(new DataColumn(COL_2));
        dt.Columns.Add(new DataColumn(COL_3));
        dt.Columns.Add(new DataColumn(COL_4));
        dt.Columns.Add(new DataColumn(COL_5));
        dt.Columns.Add(new DataColumn(COL_6));
        dt.Columns.Add(new DataColumn(COL_7));
        dt.Columns.Add(new DataColumn(COL_8));
        dt.Columns.Add(new DataColumn(COL_9));
        #region 變動 入魚日期(增加)
        SqlCommand into_fish = new SqlCommand(@"SELECT Pool_id, sum(number) as number, date
            FROM           Measuring
            WHERE         (status = '入苗') AND  (CONVERT(varchar(10), date, 111) LIKE '%' + @time + '%') and [Pool_id]= @pool
            GROUP BY   Pool_id,  date");
        into_fish.Parameters.Add("@pool", SqlDbType.NVarChar, 10).Value = pool;
        into_fish.Parameters.Add("@time", SqlDbType.NVarChar, 7).Value = date;

        DataTable Fish_Into = Fish.SqlHelper.cmdTable(into_fish);
        #endregion
        #region 變動 轉入轉出(增加 ，減少)
        SqlCommand change = new SqlCommand(@"SELECT          Past_pool_id, move_date, move_pool_id, Fish_number, Fish_AVGweight,Fish_size
            FROM            Distribution
            WHERE          (CONVERT(varchar(10), move_date, 111) LIKE '%%') AND (move_pool_id = 'C4')
            UNION ALL
            SELECT          Past_pool_id, move_date, move_pool_id, Fish_number, Fish_AVGweight,Fish_size
            FROM              Distribution AS Distribution_1
            WHERE          (CONVERT(varchar(10), move_date, 111) LIKE '%%') AND (Past_pool_id = 'C4')
            ORDER BY   move_date

            ");
        //cmd.Parameters.Add("@time", SqlDbType.NVarChar, 30).Value = substrings[2] + "/" + substrings[3];
        //cmd.Parameters.Add("@Fodder_name", SqlDbType.NVarChar, 10).Value = substrings[1];
        //cmd.Parameters.Add("@company_name", SqlDbType.NVarChar, 30).Value = substrings[0];
        DataTable Change_Fish = Fish.SqlHelper.cmdTable(change);
        #endregion
        #region 變動表  損耗(減少)
        SqlCommand Loss = new SqlCommand(@"SELECT [Pool_id]
                  ,sum([Loss_or_Profit_Num]) as Loss_or_Profit_Num
                  ,[date]
            	   FROM [Inventory] WHERE (CONVERT(varchar(10), date, 111) LIKE '%2016/03%') and [Pool_id]='F2'
             GROUP BY [Pool_id],[date]
             order by  [Pool_id]");
        //cmd.Parameters.Add("@time", SqlDbType.NVarChar, 30).Value = substrings[2] + "/" + substrings[3];
        //cmd.Parameters.Add("@Fodder_name", SqlDbType.NVarChar, 10).Value = substrings[1];
        //cmd.Parameters.Add("@company_name", SqlDbType.NVarChar, 30).Value = substrings[0];
        DataTable Loss_Fish = Fish.SqlHelper.cmdTable(Loss);
        #endregion
        #region 變動 出魚(減少)
        SqlCommand sell = new SqlCommand(@"SELECT           Pool_id,   Outside_date,sum([number]) as number
            FROM             Out
            where (CONVERT(varchar(10), Outside_date, 111) LIKE '%2016/03%') and Pool_id='F2'
            GROUP BY  Pool_id,   Outside_date
            order by Outside_date");
        //cmd.Parameters.Add("@time", SqlDbType.NVarChar, 30).Value = substrings[2] + "/" + substrings[3];
        //cmd.Parameters.Add("@Fodder_name", SqlDbType.NVarChar, 10).Value = substrings[1];
        //cmd.Parameters.Add("@company_name", SqlDbType.NVarChar, 30).Value = substrings[0];
        DataTable Sell_Fish = Fish.SqlHelper.cmdTable(sell);
        #endregion
        #region 月份輪迴  (帶入)

        #endregion
        #region 月份輪迴        

        for (int i = 1; i < 32; i++)
        {
            DataRow dr = dt.NewRow();
            /**入魚**/
            for(int into=0; into < Fish_Into.Rows.Count; into++)
            {
                if(Convert.ToDateTime(Fish_Into.Rows[0][2]).ToString("yyyy/MM/dd").ToString() == date + i)
                {
                    dr[COL_1] = Fish_Into.Rows[0][1];
                }
            }
          
            for (int add1 = 0; add1 < Change_Fish.Rows.Count; add1++)
            { 
                /**轉入轉出 轉池(增)  **/
                if (Change_Fish.Rows[add1][2].ToString() == "C4" && Change_Fish.Rows[add1][5].ToString() == "0" && (Convert.ToDateTime(Change_Fish.Rows[add1][1]).ToString("yyyy-MM-dd")).ToString() == "2016-03-" + i)
                {
                    dr[COL_2] = Change_Fish.Rows[add1][3];
                }
                /**轉入轉出 分養(增)  **/
                if (Change_Fish.Rows[add1][2].ToString() == "C4" && Change_Fish.Rows[add1][5].ToString() != "0" && (Convert.ToDateTime(Change_Fish.Rows[add1][1]).ToString("yyyy-MM-dd")).ToString() == "2016-03-" + i)
                {
                    dr[COL_3] = Change_Fish.Rows[add1][3];
                }
                /**轉入轉出 轉池(減)  **/
                if (Change_Fish.Rows[add1][0].ToString() == "C4" && Change_Fish.Rows[add1][5].ToString() == "0" && (Convert.ToDateTime(Change_Fish.Rows[add1][1]).ToString("yyyy-MM-dd")).ToString() == "2016-01-" + i)
                {
                    dr[COL_6] = Change_Fish.Rows[add1][3];
                }
                ///**轉入轉出 分養(減)  **/
                if (Change_Fish.Rows[add1][0].ToString() == "C4" && Change_Fish.Rows[add1][5].ToString() != "0" && (Convert.ToDateTime(Change_Fish.Rows[add1][1]).ToString("yyyy-MM-dd")).ToString() == "2016-03-" + i)
                {
                    dr[COL_7] = Change_Fish.Rows[add1][3];
                }
            }

            for (int reduce = 0; reduce < Sell_Fish.Rows.Count; reduce++)
            {
                ///**出魚(減)  **/
                if ((Convert.ToDateTime(Sell_Fish.Rows[reduce][1]).ToString("yyyy-MM-dd")).ToString() == "2016-03-" + i)
                {
                    dr[COL_4] = Sell_Fish.Rows[reduce][2];
                    dr[COL_5] = (Int32.Parse(Sell_Fish.Rows[reduce][2].ToString()) * 30).ToString();
                }                
            }
            for (int loss = 0; loss < Loss_Fish.Rows.Count; loss++)
            {
                ///**耗損 (減)  **/ (沒負數)
                if ((Convert.ToDateTime(Loss_Fish.Rows[loss][2]).ToString("yyyy-MM-dd")).ToString() == "2016-03-" + i)
                {
                    dr[COL_8] = Loss_Fish.Rows[loss][1].ToString();
                }
            }
            
            ///**total**/
            dr[COL_9] = "123";
            dt.Rows.Add(dr);
        }
        #endregion
        return dt;
    }

}