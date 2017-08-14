using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Feed_aday 的摘要描述
/// </summary>
public class Feed_aday
{
    public Feed_aday()
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
    private const String COL_7 = "實投包";
    private const String COL_8 = "秏損";
    private const String COL_9 = "飼料商";
    private const String COL_10 = "投餌率";
    private const String COL_11 = "添加物";
    private const String COL_12 = "添加物劑量";
    private const String COL_13 = "生餌";
    /// 取得資料
    public DataTable Get(string str1)
    {      
        string[] arr_pools = new string[5];
        #region 魚種顯示
        SqlCommand cmd = new SqlCommand(@"SELECT Pool_id, date FROM Feed WHERE (date = '" + str1 + "') GROUP BY Pool_id, date");
        DataTable Pools_items = Fish.SqlHelper.cmdTable(cmd);
        //string re_Pools = JsonConvert.SerializeObject(Pools_items, Formatting.Indented);

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
        //DataRow dr = dt.NewRow();
        #endregion
        string bait = "";
        #region 魚池與日期進行資料撈取
        for (int i = 0; i < Pools_items.Rows.Count; i++)
        {
            #region  sql All view
            SqlCommand cmd_all = new SqlCommand(@" SELECT Feed.Pool_id,Feed.date,Feed.Fish_detail_id,SUM(medicine_number) AS medicine_number, CAST(Fish_detail.Fish_AVGweight as float) as [Fish_AVGweight], Fish_detail.Move_date, Fish_detail.number, Fish_detail.Fish_size, Fish.Spawning_date, 
                            Fish.Insert_fry_date, Fish.Insert_year, Fish.Fry_weight, Fish_company.company_abbreviation, 
                            Fish_kind.kind_name,SUM(Feed.Fodder_number) AS Fodder_number,(
                    STUFF( (
                             SELECT  ',' + [Bait]         
                             FROM   Feed  T2           
                             WHERE  T2.Pool_id = Feed.Pool_id and (date = @time )FOR XML PATH('')), 1, 1, '')) AS [Bait],(  
                    STUFF( (           
                             SELECT  ',' + [Medicine_name]         
                             FROM   Feed T4 INNER JOIN
                                                Medicine ON T4.Medicine_id = Medicine.Medicine_id 
                             WHERE  (T4.date = @time) AND T4.Pool_id = @pools 
                             FOR XML PATH('')), 1, 1, '')) AS [Medicine_name],(  
                    STUFF( (           
                             SELECT ',' + Fodder.[Fodder_name] 
                             FROM     Feed T3 INNER JOIN
                                                Fodder ON T3.Fodder_id = Fodder.Fodder_id  
                            WHERE  (T3.date = @time) AND T3.Pool_id = @pools
                             FOR XML PATH('')), 1, 1, '')) AS [Fodder_name],
	                CAST(Fish_AVGweight as float) * CAST(Fish_detail.number as float) AS [Total],
                    ROUND(SUM(Feed.Fodder_number) *30 / CAST(Fish_AVGweight as float) / CAST(Fish_detail.number as float) * 100 , 2) AS [Feedrate]
                    FROM Feed   INNER JOIN
                                                Fish_detail ON Feed.Fish_detail_id = Fish_detail.Fish_detail_id INNER JOIN
                                                Fish ON Fish_detail.Fish_id = Fish.Fish_id INNER JOIN
                                                Fish_company ON Fish.Fish_company_id = Fish_company.Fish_company_id INNER JOIN
                                                Fish_kind ON Fish.Fish_kind_id = Fish_kind.Fish_kind_id

                    WHERE (date = @time) AND Pool_id = @pools
                    GROUP BY Feed.Pool_id,Feed.date,Feed.Fish_detail_id,Fish_detail.Fish_AVGweight, Fish_detail.Fish_AVGweight, Fish_detail.Move_date, Fish_detail.number, Fish_detail.Fish_size, Fish.Spawning_date, 
                                                Fish.Insert_fry_date, Fish.Insert_year, Fish.Fry_weight, Fish_company.company_abbreviation, 
                                                Fish_kind.kind_name");
            #endregion
          
            cmd_all.Parameters.Add("@time", SqlDbType.DateTime2).Value = str1;
            cmd_all.Parameters.Add("@pools", SqlDbType.NVarChar, 50).Value = Pools_items.Rows[i][0].ToString();
            DataTable All_items = Fish.SqlHelper.cmdTable(cmd_all);
            for (int j = 0; j < All_items.Rows.Count; j++)
            {
                #region 損益 view
                SqlCommand cmd_Inventory = new SqlCommand(@"SELECT * FROM Inventory WHERE
                date >= '" + str1 + "T00:00:00.000' AND  date <= '" + str1 + "T23:59:59.999' AND Pool_id ='" + All_items.Rows[j][0].ToString() + "' AND  Fish_detail_id = '" + All_items.Rows[j][2].ToString() + "'");
                DataTable Inventory_items = Fish.SqlHelper.cmdTable(cmd_Inventory);
                #endregion
                if(Inventory_items.Rows.Count > 0)
                {
                    bait = Inventory_items.Rows[0][3].ToString();
                    
                }
                else
                {
                  bait = "0";
                }
                //int test = Int32.Parse(All_items.Rows[j][4].ToString());
                DataRow dr = dt.NewRow();
                dr[COL_1] = All_items.Rows[j][0];
                dr[COL_2] = All_items.Rows[j][13];
                //Convert.ToDateTime(All_items.Rows[j][5].ToString()).ToString("MM/dd");//All_items.Rows[j][5].ToString();
                dr[COL_3] = Convert.ToDateTime(All_items.Rows[j][8].ToString()).ToString("MMdd") + "-" + Convert.ToDateTime(All_items.Rows[j][9].ToString()).ToString("MMdd")+
                    "-"+ All_items.Rows[j][10] + "-" + All_items.Rows[j][11] + "-" + All_items.Rows[j][12] + "-" + All_items.Rows[j][7] + "-" + Convert.ToDateTime(All_items.Rows[j][5].ToString()).ToString("MMdd");
                dr[COL_4] = All_items.Rows[j][6];
                dr[COL_5] = All_items.Rows[j][4];
                dr[COL_6] = All_items.Rows[j][18];
                dr[COL_7] = All_items.Rows[j][14];
                dr[COL_8] = bait;
                dr[COL_9] = All_items.Rows[j][17];
                dr[COL_10] = All_items.Rows[j][19];
                dr[COL_11] = All_items.Rows[j][16];
                dr[COL_12] = All_items.Rows[j][3];
                dr[COL_13] = All_items.Rows[j][15];
                dt.Rows.Add(dr);
            }
        }

        #endregion    
        return dt;
    }


}

/* 
           
#region DataTable return
public class Data
    {
        private const String COL_1 = "姓名";
        private const String COL_2 = "性別";
        private const String COL_3 = "電話號碼";

        /// 取得資料
        public DataTable Get()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn(COL_1));
            dt.Columns.Add(new DataColumn(COL_2));
            dt.Columns.Add(new DataColumn(COL_3));

            ///加入一些資料
            for (int i = 0; i < 20; i++)
            {
                String simName = String.Format("JB{0}", i.ToString());
                String simSex = (i % 2 == 0) ? "男" : "女";
                String simPhoneNum = "09XX-123-456";
                DataRow dr = dt.NewRow();
                dr[COL_1] = simName;
                dr[COL_2] = simSex;
                dr[COL_3] = simPhoneNum;
                dt.Rows.Add(dr);
            }
            return dt;
        }
      
    }  
    #endregion
*/

