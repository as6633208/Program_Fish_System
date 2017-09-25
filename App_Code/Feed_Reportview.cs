using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
/// <summary>
/// Feed_Reportview 的摘要描述
/// </summary>
public class Feed_Reportview
{
    public Feed_Reportview()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }
    private const String COL_1 = "池號";
    private const String COL_2 = "魚種";
    private const String COL_3 = "魚隻批號";
    private const String COL_4 = "尾數";
    private const String COL_5 = "平均魚重";
    private const String COL_6 = "總重";
    private const String COL_7 = "改魚種日";
    private const String COL_8 = "實投包";
    private const String COL_9 = "飼料商";
    private const String COL_10 = "損益";
    private const String COL_11 = "投餌率";
    private const String COL_12 = "添加物劑量";
    private const String COL_13 = "添加物";
    private const String COL_14 = "生餌";

    public DataTable Get(string str1)//
    {
        string Inventory_check = "";//判斷有無耗損
        #region 撈當日有魚的魚池 池號
        SqlCommand cmd = new SqlCommand(@"SELECT Pool_id FROM Feed WHERE (date = '" + str1 + "') GROUP BY Pool_id");
        DataTable Pools_items = Fish.SqlHelper.cmdTable(cmd);

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
        dt.Columns.Add(new DataColumn(COL_10));
        dt.Columns.Add(new DataColumn(COL_11));
        dt.Columns.Add(new DataColumn(COL_12));
        dt.Columns.Add(new DataColumn(COL_13));
        dt.Columns.Add(new DataColumn(COL_14));
        #endregion
        #region 池號資料顯示
        for (int i = 0; i < Pools_items.Rows.Count; i++)
        {
            #region 撈取當日餵食資料 (放 日池號&日期)
            SqlCommand Feed_data = new SqlCommand(@"SELECT Feed.Pool_id, Feed.Fish_detail_id , Feed.date,  SUM(Feed.medicine_number) AS medicine_number, 
							 (STUFF( (
                             SELECT  ',(' +   CAST([Fodder_number] as varchar) +' ' , [DayTime]   + ')' 
                             FROM   Feed  T2           
                             WHERE  T2.Pool_id = Feed.Pool_id and (date = @time)FOR XML PATH('')), 1, 1, '')) AS [Fodder_number],
							 (STUFF( (
                             SELECT  ',' + [Bait]         
                             FROM   Feed  T2           
                             WHERE  T2.Pool_id = Feed.Pool_id and (date = @time)FOR XML PATH('')), 1, 1, '')) AS [Bait],(
							 STUFF( (
							 SELECT  ',' + [Medicine_name]         
							 FROM   Feed T3 INNER JOIN Medicine ON T3.Medicine_id = Medicine.Medicine_id 
							 WHERE  (T3.date = @time) AND T3.Pool_id = @pools FOR XML PATH('')), 1, 1, '')) AS [Medicine_name],(
							 STUFF( (
							 SELECT  ',' + [Fodder_name]         
							 FROM  Feed T5 INNER JOIN
                                             Fodder ON T5.Fodder_id = Fodder.Fodder_id    
							 WHERE   (T5.date = @time) AND T5.Pool_id = @pools FOR XML PATH('')), 1, 1, '')) AS [Fodder_name], sum( cast (Feed.Bait as float)) as tatal_bait
                             FROM              Feed INNER JOIN
                                                         Fodder ON Feed.Fodder_id = Fodder.Fodder_id INNER JOIN
                                                         Medicine ON Feed.Medicine_id = Medicine.Medicine_id
                             WHERE			  (Feed.date = @time) AND Feed.Pool_id = @pools
                             GROUP BY Feed.date,Feed.Fish_detail_id, Feed.Pool_id");
            Feed_data.Parameters.Add("@time", SqlDbType.DateTime2).Value = str1;
            Feed_data.Parameters.Add("@pools", SqlDbType.NVarChar, 50).Value = Pools_items.Rows[i][0].ToString();
            DataTable dt_Feed_data = Fish.SqlHelper.cmdTable(Feed_data);
            #endregion
            #region 撈取批號、魚種 (日後太慢，修改Measuring.date 給區間)
            SqlCommand Feed_batch = new SqlCommand(@"SELECT b.*
            from(
            	SELECT a.* ,row_number() over (partition by Pool_id order by  date desc) rank
            	from(SELECT          Measuring.Pool_id, Measuring.Fish_detail_id, Measuring.number, Measuring.Fish_AVGweight, Measuring.date, 
                                        Fish_kind.kind_name, Fish.Spawning_date, Fish.Insert_fry_date, Fish.Insert_year, Fish.Fry_weight, 
                                        Fish_detail.Fish_size, Fish_detail.Move_date, Fish_company.company_abbreviation,
										CAST(Measuring.Fish_AVGweight as float) * CAST(Measuring.number as float) AS [Total]
            							FROM              Measuring INNER JOIN
            							                            Fish_detail ON Measuring.Fish_detail_id = Fish_detail.Fish_detail_id INNER JOIN
            							                            Fish ON Fish_detail.Fish_id = Fish.Fish_id INNER JOIN
            							                            Fish_kind ON Fish.Fish_kind_id = Fish_kind.Fish_kind_id INNER JOIN
            							                            Fish_company ON Fish.Fish_company_id = Fish_company.Fish_company_id
            							WHERE          (Measuring.date <=  @time)
            							GROUP BY   Measuring.Pool_id, Measuring.Fish_detail_id, Measuring.number, Measuring.Fish_AVGweight, Measuring.date, 
                                        Fish_kind.kind_name, Fish.Spawning_date, Fish.Insert_fry_date, Fish.Insert_year, Fish.Fry_weight, 
                                        Fish_detail.Fish_size, Fish_detail.Move_date, Fish_company.company_abbreviation
            		)a
            )b
            where b.rank ='1' and b.Pool_id=@pools");
            Feed_batch.Parameters.Add("@time", SqlDbType.DateTime2).Value = str1;
            Feed_batch.Parameters.Add("@pools", SqlDbType.NVarChar, 50).Value = Pools_items.Rows[i][0].ToString();
            DataTable dt_Feed_batch = Fish.SqlHelper.cmdTable(Feed_batch);
            #endregion
            #region 撈取 改魚種日 (日後太慢，修改Measuring.date 給區間)
            SqlCommand Feed_WeightChange = new SqlCommand(@"SELECT top(1) *
                from(
                	SELECT a.* ,row_number() over (partition by Pool_id order by  date desc) rank
                	from(SELECT Measuring.Pool_id, Measuring.date,status
                			from Measuring
                			WHERE          (date <= @time) 
                			group by   Measuring_id, Pool_id, date,status
                		)a
                )b
                 where (b.status='測量'or b.status='入苗') and  b.Pool_id= @pools
                  order by b.date desc ");
            Feed_WeightChange.Parameters.Add("@time", SqlDbType.DateTime2).Value = str1;
            Feed_WeightChange.Parameters.Add("@pools", SqlDbType.NVarChar, 50).Value = Pools_items.Rows[i][0].ToString();
            DataTable dt_Feed_WeightChange = Fish.SqlHelper.cmdTable(Feed_WeightChange);
            #endregion
            #region 損益數量
            SqlCommand cmd_Inventory = new SqlCommand(@"SELECT Loss_or_Profit_Num FROM Inventory WHERE
                date >= '" + str1 + "T00:00:00.000' AND  date <= '" + str1 + "T23:59:59.999' AND Pool_id ='" + Pools_items.Rows[i][0].ToString() + "' ");
            DataTable Inventory_items = Fish.SqlHelper.cmdTable(cmd_Inventory);
            #endregion   
            /**開始加入資料囉**/
            DataRow dr = dt.NewRow();
            dr[COL_1]  = Pools_items.Rows[i][0];
            dr[COL_2]  = dt_Feed_batch.Rows[0][5];
            dr[COL_3]  = Convert.ToDateTime(dt_Feed_batch.Rows[0][6].ToString()).ToString("MMdd") + "-" + Convert.ToDateTime(dt_Feed_batch.Rows[0][7].ToString()).ToString("MMdd") +
                    "-" + dt_Feed_batch.Rows[0][8] + "-" + dt_Feed_batch.Rows[0][9] + "-" + dt_Feed_batch.Rows[0][12] + "-" + dt_Feed_batch.Rows[0][10] + "-" + Convert.ToDateTime(dt_Feed_batch.Rows[0][11].ToString()).ToString("MMdd");
            dr[COL_4]  = dt_Feed_batch.Rows[0][2];
            dr[COL_5]  = dt_Feed_batch.Rows[0][3];
            dr[COL_6]  = dt_Feed_batch.Rows[0][13]; //總重
            /**防呆 魚種日(暫時尚無入苗實資料)**/
            string WeightChange_check = "";
            if (dt_Feed_WeightChange.Rows.Count > 0){
                WeightChange_check = Convert.ToDateTime(dt_Feed_WeightChange.Rows[0][1]).ToString("MMdd");
            }
            else{
                WeightChange_check = "Null";
            }
            dr[COL_7]  = WeightChange_check;//Convert.ToDateTime(dt_Feed_WeightChange.Rows[0][1]).ToString("MMdd");//改魚重日
            dr[COL_8]  = dt_Feed_data.Rows[0][4];//實投包數
            dr[COL_9]  = dt_Feed_data.Rows[0][7];//供應商
            /**判斷有無耗損**/
            if (Inventory_items.Rows.Count > 0){
                Inventory_check = Inventory_items.Rows[0][0].ToString();              
            }else{
                Inventory_check = "0";
            }
            dr[COL_10] = Inventory_check;//損益
            dr[COL_11] = (
                            (  Int32.Parse(dt_Feed_batch.Rows[0][13].ToString()) +
                             ( Int32.Parse(dt_Feed_data.Rows[0][8].ToString())  *0.25 )    
                                                                                     )  
                            /  Int32.Parse(dt_Feed_batch.Rows[0][2].ToString())
                            / Int32.Parse(dt_Feed_batch.Rows[0][3].ToString()) * 100  
                         ).ToString();
            //投餌率  ( 總重 + 生餌/4 ) / 尾數 / 平均魚種 * 100
            dr[COL_12] = dt_Feed_data.Rows[0][3];//添加物劑量
            dr[COL_13] = dt_Feed_data.Rows[0][6];//添加物名稱
            dr[COL_14] = dt_Feed_data.Rows[0][5];//生餌
            dt.Rows.Add(dr);
        }
 

        #endregion
        return dt;
    }
}