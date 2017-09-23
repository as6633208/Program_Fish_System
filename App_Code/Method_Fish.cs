using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Method_Fish 的摘要描述
/// </summary>
public class Method_Fish
{
    string Fish_sql = WebConfigurationManager.ConnectionStrings["Fish_sql"].ConnectionString;
    SqlConnection conn;
    public Method_Fish()
    {
        conn = new SqlConnection(Fish_sql);
    }
    #region 帳戶操作方法

    #region 登入判斷
    public DataTable Login(string a, string b)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Admin_List WHERE (Account = @a) AND (Password = @b)");
        cmd.Parameters.Add("@a", SqlDbType.NVarChar, 50).Value = a;
        cmd.Parameters.Add("@b", SqlDbType.NVarChar, 50).Value = b;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 帳戶顯示
    public DataTable Login()
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Admin_List");
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 新增用戶
    public string Account_Insert(string a, string b, string c)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"INSERT INTO Admin_List
                            (Account, Password, authority) VALUES
                        (@Account,@Password,@authority)");
        cmd.Parameters.Add("@Account", SqlDbType.NVarChar, 50).Value = a;
        cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = b;
        cmd.Parameters.Add("@authority", SqlDbType.Int).Value = c;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 修改用戶
    public string Account_Modify(string a, string b, string c, string d)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Admin_List SET
        Account = @Account, Password = @Password ,authority=@authority
            WHERE (Admin_id = @Admin_id)");
        cmd.Parameters.Add("@Account", SqlDbType.NVarChar, 50).Value = a;
        cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = b;
        cmd.Parameters.Add("@authority", SqlDbType.Int).Value = c;
        cmd.Parameters.Add("@Admin_id", SqlDbType.Int).Value = d;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 刪除用戶
    public string Account_Delete(string a)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"DELETE FROM Admin_List WHERE (Admin_id = @Original_Admin_id)");
        cmd.Parameters.Add("@Original_Admin_id", SqlDbType.Int).Value = a;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #endregion
    #region 供應商操作
    #region 供應商新增
    public string Supplier_Insert(string a, string b, string c)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"INSERT INTO Fish_company
                            (company_name, company_abbreviation, company_item, last_use_time) VALUES
                        (@company_name,@company_abbreviation,@company_item,@last_use_time)");
        cmd.Parameters.Add("@company_name", SqlDbType.NVarChar, 50).Value = a;
        cmd.Parameters.Add("@company_abbreviation", SqlDbType.NVarChar, 50).Value = b;
        cmd.Parameters.Add("@company_item", SqlDbType.NVarChar, 50).Value = c;
        cmd.Parameters.Add("@last_use_time", SqlDbType.DateTime2, 7).Value = DateTime.Now.ToShortDateString();
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 供應商顯示
    public DataTable Supplier_View()
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Fish_company ");
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion 
    #region 供應商修改名稱
    public string Supplier_Modify(string a, string b, string c)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Fish_company SET
        company_name = @company_name, company_abbreviation = @company_abbreviation
            WHERE (Fish_company_id = @Original_Fish_company_id)");
        cmd.Parameters.Add("@company_name", SqlDbType.NVarChar, 10).Value = a;
        cmd.Parameters.Add("@company_abbreviation", SqlDbType.NVarChar, 10).Value = b;
        cmd.Parameters.Add("@Original_Fish_company_id", SqlDbType.Int).Value = c;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 選擇供應商種類(魚苗or飼料)顯示
    public DataTable Supplier_View(string a)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT Fish_company_id, company_name, company_abbreviation, company_item, last_use_time
                        FROM Fish_company WHERE (company_item = @a) ORDER BY   last_use_time DESC");
        cmd.Parameters.Add("@a", SqlDbType.NVarChar, 10).Value = a;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #endregion
    #region 魚種操作
    #region 魚種顯示
    public DataTable Fish_Kind_View()
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Fish_kind ");
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 魚種修改名稱
    public string Fish_Kind_Modify(string a, string b)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Fish_kind SET kind_name = @kind_name
                WHERE (Fish_kind_id = @Original_Fish_kind_id)");
        cmd.Parameters.Add("@kind_name", SqlDbType.NVarChar, 20).Value = a;
        cmd.Parameters.Add("@Original_Fish_kind_id", SqlDbType.Int).Value = b;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 魚種新增(renwu)
    public string Fish_Kind_Insert(string a)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"INSERT INTO Fish_kind (kind_name) VALUES (@kind_name)");
        cmd.Parameters.Add("@kind_name", SqlDbType.NVarChar, 50).Value = a;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 魚種查詢(sen)
    public DataTable Fish_Kind_Search(string Fish_kind_id)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Fish_kind WHERE Fish_kind_id = @Fish_kind_id");
        cmd.Parameters.Add("@Fish_kind_id", SqlDbType.Int, 50).Value = Fish_kind_id;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #endregion
    #region 測量及損益操作
    #region 測量新增(建)
    public string Measuring_insert(string Pool_id, int Fish_detail_id, int number, string Fish_AVGweight, string date, string before_number, string before_Fish_AVGweight)
    {
        int old_fish_number;
        string result = "";
        //呼叫所有測量記錄
        DataTable select = measuring_view(Pool_id, Fish_detail_id.ToString());
        Console.Write(select);
        //判斷是否為全新紀錄
        if (select.Rows.Count < 1)
        {

            SqlCommand cmd = new SqlCommand(@"INSERT INTO Measuring
                            (Pool_id, Fish_detail_id, number, Fish_AVGweight,date,before_number,before_Fish_AVGweight) OUTPUT INSERTED.Measuring_id VALUES 
                        (@Pool_id,@Fish_detail_id,@number,@Fish_AVGweight,@date,@before_number,@before_Fish_AVGweight)");
            cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = Pool_id;
            cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int).Value = Fish_detail_id;
            cmd.Parameters.Add("@number", SqlDbType.Int).Value = number;
            cmd.Parameters.Add("@Fish_AVGweight", SqlDbType.NVarChar, 10).Value = Fish_AVGweight;
            cmd.Parameters.Add("@date", SqlDbType.DateTime2, 7).Value = date;
            cmd.Parameters.Add("@before_number", SqlDbType.NVarChar, 10).Value = before_number;
            cmd.Parameters.Add("@before_Fish_AVGweight", SqlDbType.NVarChar, 10).Value = before_Fish_AVGweight;
            int Measuring_id = Fish.SqlHelper.Return_IDENTITY(cmd);
            result = (Measuring_id != 0) ? "success" : "fail";
            //抓取原本的數量 
            SqlCommand cmd2 = new SqlCommand(@"SELECT Pool_number From Pool WHERE Pool_id = @Pool_id");
            cmd2.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = Pool_id;
            DataTable dt = Fish.SqlHelper.cmdTable(cmd2);
            old_fish_number = Int32.Parse(dt.Rows[0][0].ToString());
            //判斷是否有損益 數量不為零的狀態，有損益存在必須新增，數量不變則不用
            if (number - old_fish_number != 0)
            {
                Inventory(Pool_id, Fish_detail_id, number, old_fish_number, date, Measuring_id);
                Measuring_UP_Pool(Pool_id, number);
            }
            //更新魚群細節資料
            //如果是損益 平均重-1
            if(Fish_AVGweight == "-1")
            {
                Measuring_UP_FishDetail(Fish_detail_id, before_Fish_AVGweight, number);
            }
            else
            {
                Measuring_UP_FishDetail(Fish_detail_id, Fish_AVGweight, number);
            }

            return result;
        }
        else
        {
            int last_one = select.Rows.Count - 1;
            //日期最晚的測量記錄
            string[] last_time_spilt;
            last_time_spilt = select.Rows[last_one][5].ToString().Split(' ');
            DateTime last_time = Convert.ToDateTime(last_time_spilt[0]);
            //新增的測量記錄日期
            DateTime now_date = Convert.ToDateTime(date);
            //判斷時間前後，若新增之日期為最後一筆，則無需考慮前後資料問題
            if (now_date > last_time)
            {

                SqlCommand cmd = new SqlCommand(@"INSERT INTO Measuring
                            (Pool_id, Fish_detail_id, number, Fish_AVGweight,date,before_number,before_Fish_AVGweight) OUTPUT INSERTED.Measuring_id VALUES 
                        (@Pool_id,@Fish_detail_id,@number,@Fish_AVGweight,@date,@before_number,@before_Fish_AVGweight)");
                cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = Pool_id;
                cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int).Value = Fish_detail_id;
                cmd.Parameters.Add("@number", SqlDbType.Int).Value = number;
                cmd.Parameters.Add("@Fish_AVGweight", SqlDbType.NVarChar, 10).Value = Fish_AVGweight;
                cmd.Parameters.Add("@date", SqlDbType.DateTime2, 7).Value = date;
                cmd.Parameters.Add("@before_number", SqlDbType.NVarChar, 10).Value = before_number;
                cmd.Parameters.Add("@before_Fish_AVGweight", SqlDbType.NVarChar, 10).Value = before_Fish_AVGweight;
                int Measuring_id = Fish.SqlHelper.Return_IDENTITY(cmd);
                result = (Measuring_id != 0) ? "success" : "fail";
                //抓取原本的數量 
                SqlCommand cmd2 = new SqlCommand(@"SELECT Pool_number From Pool WHERE Pool_id = @Pool_id");
                cmd2.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = Pool_id;
                DataTable dt = Fish.SqlHelper.cmdTable(cmd2);
                old_fish_number = Int32.Parse(dt.Rows[0][0].ToString());
                //判斷是否有損益 數量不為零的狀態，有損益存在必須新增，數量不變則不用
                if (number - old_fish_number != 0)
                {
                    Inventory(Pool_id, Fish_detail_id, number, old_fish_number, date, Measuring_id);
                    Measuring_UP_Pool(Pool_id, number);
                }
                //更新魚群細節資料
                //如果是損益 平均重-1
                if (Fish_AVGweight == "-1")
                {
                    Measuring_UP_FishDetail(Fish_detail_id, before_Fish_AVGweight, number);
                }
                else
                {
                    Measuring_UP_FishDetail(Fish_detail_id, Fish_AVGweight, number);
                }
                return result;
            }
            else
            {
                var new_position = 0;
                for (int i = 0; i < select.Rows.Count; i++)
                {

                    //日期切割
                    string[] select_time_spilt;
                    select_time_spilt = select.Rows[i][5].ToString().Split(' ');
                    DateTime select_time = Convert.ToDateTime(select_time_spilt[0]);
                    if (now_date > select_time)
                    {
                    }
                    else
                    {
                        new_position = i;
                        break;
                    }
                }
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Measuring
                            (Pool_id, Fish_detail_id, number, Fish_AVGweight,date,before_number,before_Fish_AVGweight) OUTPUT INSERTED.Measuring_id VALUES 
                        (@Pool_id,@Fish_detail_id,@number,@Fish_AVGweight,@date,@before_number,@before_Fish_AVGweight)");
                cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = Pool_id;
                cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int).Value = Fish_detail_id;
                cmd.Parameters.Add("@number", SqlDbType.Int).Value = number;
                cmd.Parameters.Add("@Fish_AVGweight", SqlDbType.NVarChar, 10).Value = Fish_AVGweight;
                cmd.Parameters.Add("@date", SqlDbType.DateTime2, 7).Value = date;
                cmd.Parameters.Add("@before_number", SqlDbType.NVarChar, 10).Value = select.Rows[new_position - 1][3].ToString();
                cmd.Parameters.Add("@before_Fish_AVGweight", SqlDbType.NVarChar, 10).Value = select.Rows[new_position - 1][4].ToString();
                int Measuring_id = Fish.SqlHelper.Return_IDENTITY(cmd);
                result = (Measuring_id != 0) ? "success" : "fail";
                //新增損益紀錄
                string insert_Inventory = Inventory(Pool_id, Fish_detail_id, number, Int32.Parse(select.Rows[new_position - 1][3].ToString()), date, Measuring_id);
                //修改下筆資料
                DataTable Next_Inventory = select_Inventory(Pool_id, Fish_detail_id, select.Rows[new_position][5].ToString());
                int id = Int32.Parse(Next_Inventory.Rows[0][0].ToString());
                Updata_Inventory(Next_Inventory.Rows[0][0].ToString(), Int32.Parse(select.Rows[new_position][3].ToString()) - number);
                //如果是損益 平均重-1
                if (Fish_AVGweight == "-1")
                {
                    Updata_Measuring(select.Rows[new_position][0].ToString(), number.ToString(), before_Fish_AVGweight);
                }
                else
                {
                    Updata_Measuring(select.Rows[new_position][0].ToString(), number.ToString(), Fish_AVGweight);
                }
               
                return result;
            }
        }
    }
    #endregion
    #region 尋找損益紀錄(單筆)(建)
    public DataTable select_Inventory(string Pool_id, int Fish_detail_id, string date)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Inventory WHERE (Pool_id = @Pool_id) AND ( Fish_detail_id=@Fish_detail_id) AND(date=@date) ");
        cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = Pool_id;
        cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int, 50).Value = Fish_detail_id;
        cmd.Parameters.Add("@date", SqlDbType.DateTime2, 7).Value = date;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;

    }

    #endregion
    #region 更新損益紀錄(單筆)(建)
    public void Updata_Inventory(string id, int number)
    {
        SqlCommand cmd = new SqlCommand(@"UPDATE Inventory SET Loss_or_Profit_Num = @number
                WHERE (Inventory_id = @Inventory_id)");
        cmd.Parameters.Add("@Inventory_id", SqlDbType.Int).Value = id;
        cmd.Parameters.Add("@number", SqlDbType.Int, 20).Value = number;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);


    }
    #endregion
    #region 更新測量紀錄(單筆)(建)
    public void Updata_Measuring(string id, string number, string Fish_AVGweight)
    {
        SqlCommand cmd = new SqlCommand(@"UPDATE Measuring SET before_number = @before_number,before_Fish_AVGweight = @before_Fish_AVGweight
                WHERE (Measuring_id = @Measuring_id)");
        cmd.Parameters.Add("@Measuring_id", SqlDbType.Int).Value = id;
        cmd.Parameters.Add("@before_number", SqlDbType.NVarChar, 10).Value = number;
        cmd.Parameters.Add("@before_Fish_AVGweight", SqlDbType.NVarChar, 10).Value = Fish_AVGweight;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
    }
    #endregion
    #region 新增損益紀錄(建)
    public string Inventory(string a, int b, int c, int d, string e, int f)
    {
        //a=池編號 b=魚池細節編號 c=分養池加總的數量 d=被分養池原本數量 e日期
        int number = c - d;
        string result = "";
        SqlCommand cmd = new SqlCommand(@"INSERT INTO Inventory
                            (Pool_id, Fish_detail_id, Loss_or_Profit_Num, date,Measuring_id) VALUES
                        (@Pool_id,@Fish_detail_id,@Loss_or_Profit_Num,@date,@Measuring_id)");
        cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = a;
        cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int).Value = b;
        cmd.Parameters.Add("@Loss_or_Profit_Num", SqlDbType.Int).Value = number;
        cmd.Parameters.Add("@date", SqlDbType.DateTime2, 7).Value = e;
        cmd.Parameters.Add("@Measuring_id", SqlDbType.Int).Value = f;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 測量後更新魚池資料(建)
    public string Measuring_UP_Pool(string Pool_id, int number)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Pool SET
        Pool_number = @Pool_number
        WHERE (Pool_id = @Pool_id)");
        cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = Pool_id;
        cmd.Parameters.Add("@Pool_number", SqlDbType.Int).Value = number;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 測量後更新魚群細節資料(建)
    public string Measuring_UP_FishDetail(int Fish_detail_id, string Fish_AVGweight, int number)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Fish_detail SET
        Fish_AVGweight = @Fish_AVGweight, number = @number
        WHERE (Fish_detail_id = @Fish_detail_id)");
        cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int).Value = Fish_detail_id;
        cmd.Parameters.Add("@Fish_AVGweight", SqlDbType.NVarChar, 10).Value = Fish_AVGweight;
        cmd.Parameters.Add("@number", SqlDbType.Int).Value = number;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 測量刪除(建)
    public string Measuring_delete(string Pool_id, string Fish_detail_id, string id)
    {
        DataTable select = measuring_view(Pool_id, Fish_detail_id);
        var new_position = 0;
        for (int i = 0; i < select.Rows.Count; i++)
        {
            if (select.Rows[i][0].ToString() == id)
            {
                new_position = i;
                break;
            }
            else
            {
            }
        }
        int check_num;
        //判斷是否為刪除最後一筆資料，是直接刪除，否修正下筆紀錄及刪除。
        if (new_position == (select.Rows.Count - 1))
        {
            DataTable sel_Inventory = select_Inventory(Pool_id, Int32.Parse(Fish_detail_id), select.Rows[new_position][5].ToString());
            //刪除該筆測量記錄
            SqlCommand cmd = new SqlCommand(@"DELETE Measuring  WHERE (Measuring_id = @Measuring_id)");
            cmd.Parameters.Add("@Measuring_id", SqlDbType.Int).Value = id;
            check_num = Fish.SqlHelper.cmdCheck(cmd);
            //更新魚群細節資訊    
            string b = Measuring_UP_FishDetail(Int32.Parse(Fish_detail_id), select.Rows[new_position][7].ToString(), Int32.Parse(select.Rows[new_position][6].ToString()));
            //更新池內數量
            string a = Measuring_UP_Pool(Pool_id, Int32.Parse(select.Rows[new_position][6].ToString()));
            //查詢該筆資料是否有包含損益資料，有刪除無略過
            if (sel_Inventory.Rows.Count > 0)
            {
                Console.Write(sel_Inventory);
                SqlCommand cmd_Inventory = new SqlCommand(@"DELETE Inventory  WHERE (Inventory_id = @Inventory_id)");
                cmd_Inventory.Parameters.Add("@Inventory_id", SqlDbType.Int).Value = sel_Inventory.Rows[0][0];
                check_num = Fish.SqlHelper.cmdCheck(cmd_Inventory);
                return "success";
            }
            else
            {
                return "success";
            }

        }
        else
        {
            DataTable sel_Inventory = select_Inventory(Pool_id, Int32.Parse(Fish_detail_id), select.Rows[new_position][5].ToString());
            //刪除該筆測量記錄
            SqlCommand cmd = new SqlCommand(@"DELETE Measuring  WHERE (Measuring_id = @Measuring_id)");
            cmd.Parameters.Add("@Measuring_id", SqlDbType.Int).Value = id;
            check_num = Fish.SqlHelper.cmdCheck(cmd);
            //修正下筆測量記錄
            Updata_Measuring(select.Rows[new_position + 1][0].ToString(), select.Rows[new_position][6].ToString(), select.Rows[new_position][7].ToString());
            //查詢下筆測量是否包含損益
            DataTable sel_Inventory_next = select_Inventory(Pool_id, Int32.Parse(Fish_detail_id), select.Rows[new_position + 1][5].ToString());
            //計算上筆跟下筆的差異((非此筆資料
            int update_Inventory_num = (Int32.Parse(select.Rows[new_position - 1][3].ToString())) - (Int32.Parse(select.Rows[new_position + 1][3].ToString()));
            if (sel_Inventory_next.Rows.Count > 0)
            {
                SqlCommand cmd_UP_Inventory = new SqlCommand(@"UPDATE Inventory SET Loss_or_Profit_Num = @number,Measuring_id = @Measuring_id
                WHERE (Inventory_id = @Inventory_id)");
                cmd_UP_Inventory.Parameters.Add("@Inventory_id", SqlDbType.Int).Value = sel_Inventory_next.Rows[0][0];
                cmd_UP_Inventory.Parameters.Add("@number", SqlDbType.Int, 20).Value = update_Inventory_num;
                cmd_UP_Inventory.Parameters.Add("@Measuring_id", SqlDbType.Int, 20).Value = select.Rows[new_position + 1][0].ToString();
                check_num = Fish.SqlHelper.cmdCheck(cmd_UP_Inventory);
            }
            else
            {
                SqlCommand cmd_insert_Inventory = new SqlCommand(@"INSERT INTO Pool (Pool_id,Fish_detail_id,Loss_or_Profit_Num,date,Measuring_id) VALUES (@Pool_id,@Fish_detail_id,@Loss_or_Profit_Num,@date,@Measuring_id)");
                cmd_insert_Inventory.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = select.Rows[new_position + 1][1].ToString();
                cmd_insert_Inventory.Parameters.Add("@Fish_detail_id", SqlDbType.Int, 20).Value = select.Rows[new_position + 1][2].ToString();
                cmd_insert_Inventory.Parameters.Add("@Loss_or_Profit_Num", SqlDbType.Int, 20).Value = update_Inventory_num;
                cmd_insert_Inventory.Parameters.Add("@date", SqlDbType.DateTime2, 7).Value = select.Rows[new_position + 1][5].ToString();
                cmd_insert_Inventory.Parameters.Add("@Measuring_id", SqlDbType.Int, 20).Value = select.Rows[new_position + 1][0].ToString();
                check_num = Fish.SqlHelper.cmdCheck(cmd_insert_Inventory);

            }
            //查詢該筆資料是否有包含損益資料，有刪除無略過
            if (sel_Inventory.Rows.Count > 0)
            {
                SqlCommand cmd_Inventory = new SqlCommand(@"DELETE Inventory  WHERE (Inventory_id = @Inventory_id)");
                cmd_Inventory.Parameters.Add("@Inventory_id", SqlDbType.Int).Value = sel_Inventory.Rows[0][0];
                check_num = Fish.SqlHelper.cmdCheck(cmd_Inventory);
                return "success";
            }
            else
            {
                return "success";
            }

        }

    }
    #endregion
    #region 測量更新(建)
    public string Measuring_update(string Pool_id, int Fish_detail_id, int number, string Fish_AVGweight, string date, string before_number, string before_Fish_AVGweight, string id)
    {

        string delest_cheak = Measuring_delete(Pool_id, Fish_detail_id.ToString(), id);
        string insert_cheak = Measuring_insert(Pool_id, Fish_detail_id, number, Fish_AVGweight, date, before_number, before_Fish_AVGweight);
        return "success";
    }
    #endregion
    #region 測量資料查詢
    public DataTable measuring_view(string Pool_id, string Fish_detail_id)
    {

        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Measuring WHERE (Pool_id = @Pool_id) AND ( Fish_detail_id=@Fish_detail_id) ORDER BY date ");
        cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = Pool_id;
        cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int, 50).Value = Fish_detail_id;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 測量資料查詢(單筆)
    public DataTable measuring_view_one(string Measuring_id)
    {

        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Measuring WHERE (Measuring_id = @Measuring_id)  ");
        cmd.Parameters.Add("@Measuring_id", SqlDbType.Int, 50).Value = Measuring_id;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion

    #endregion
    #region 飼料操作
    #region 飼料新增後顯示(renwu)
    public DataTable Fodder_View()
    {
        SqlCommand cmd = new SqlCommand(@"SELECT Fodder.Fodder_name, Fodder.Fodder_id, Fish_company.company_name
                    FROM Fodder INNER JOIN Fish_company ON Fodder.Fish_company_id = Fish_company.Fish_company_id");
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 飼料庫存新增(翔)
    public string Fodder_Insert(string a, string b)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"INSERT INTO Fodder
                            (Fish_company_id, Fodder_name,Fodder_number) VALUES
                        (@Fish_company_id, @Fodder_name,0)");
        cmd.Parameters.Add("@Fodder_name", SqlDbType.NVarChar, 50).Value = a;
        cmd.Parameters.Add("@Fish_company_id", SqlDbType.Int, 50).Value = b;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 飼料庫存修改(翔)
    public string Fodder_Modify(string a, string b)
    {
        //Update Products Set ProductName = ProductName + ',6' Where ProductID = 1
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Fodder SET Fodder_number = Fodder_number+@Fodder_number
            WHERE (Fodder_id = @Fodder_id)");
        cmd.Parameters.Add("@Fodder_id", SqlDbType.Int).Value = a;
        cmd.Parameters.Add("@Fodder_number", SqlDbType.Float).Value = b;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 飼料進貨查詢 (翔)
    public DataTable Fodder_detail_view()
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Fodder INNER JOIN Feed_Purchase ON (Fodder.Fodder_id = Feed_Purchase.Fodder_id)
                                        LEFT JOIN Fish_company ON (Fodder.Fish_company_id = Fish_company.Fish_company_id) WHERE company_item = '飼料商'");
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 飼料總量修改 (翔)
    public string Fodder_number_modify(string a, string b)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Fodder SET Fodder_number = @Fodder_number WHERE (Fodder_id = @Fodder_id)");
        cmd.Parameters.Add("@Fodder_id", SqlDbType.Int).Value = a;
        cmd.Parameters.Add("@Fodder_number", SqlDbType.NVarChar, 10).Value = b;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 飼料歷史紀錄修改 (翔)
    public string FeedPurchase_modify(string a, string b)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Feed_Purchase SET number = @number WHERE     
                    (Feed_Purchase_id = @Feed_Purchase_id)");
        cmd.Parameters.Add("@Feed_Purchase_id", SqlDbType.Int).Value = a;
        cmd.Parameters.Add("@number", SqlDbType.Float).Value = b;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 飼料進貨新增 FeedPurchase 歷史表(翔)
    public string FeedPurchase_Insert(string a, string b, string c)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"INSERT INTO Feed_Purchase
                            (Fodder_id, number, date) VALUES
                        (@Fodder_id, @feedPurchase_number, @feedPurchase_date)");
        cmd.Parameters.Add("@Fodder_id", SqlDbType.Int).Value = a;
        cmd.Parameters.Add("@feedPurchase_number", SqlDbType.Float).Value = b;
        cmd.Parameters.Add("@feedPurchase_date", SqlDbType.DateTime2, 7).Value = c;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 飼料進貨包數修改(翔)//尚未使用
    public string FeedPurchase_Modify(string a, string b)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Feed_Purchase SET
        number = @Feed_Purchase_number, date = @feedPurchase_date
            WHERE (Fodder_id = @Original_Fodder_id)");
        cmd.Parameters.Add("@Feed_Purchase_number", SqlDbType.Float).Value = a;
        cmd.Parameters.Add("@Original_Fodder_id", SqlDbType.Int).Value = b;
        cmd.Parameters.Add("@feedPurchase_date", SqlDbType.DateTime2, 7).Value = DateTime.Now.ToShortDateString();
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 飼料包數顯示(Renwu)
    public DataTable FeedPurchase_View()
    {
        SqlCommand cmd = new SqlCommand(@"SELECT Fish_company.company_name, Fodder.Fodder_name, Fodder.Fodder_number
                    FROM Fodder INNER JOIN Fish_company ON Fodder.Fish_company_id = Fish_company.Fish_company_id 
                ORDER BY   Fish_company.company_name");
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 飼料更新數量(翔)
    public DataTable SelectSum(string a)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT SUM (number) Feed_Purchase FROM Feed_Purchase WHERE Fodder_id = @Fodder_id");
        cmd.Parameters.Add("@Fodder_id", SqlDbType.Int).Value = a;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 飼料Fodder catch id(Renwu)
    public DataTable FeedPurchase_catch_id(string supplier, string feed)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Fodder WHERE (Fish_company_id = @supplier) AND (Fodder_name = @feed)");
        cmd.Parameters.Add("@supplier", SqlDbType.Int).Value = supplier;
        cmd.Parameters.Add("@feed", SqlDbType.NVarChar, 10).Value = feed;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 飼料庫存修改(翔)
    public string Medicine_Modify(string a, string b)
    {
        //Update Products Set ProductName = ProductName + ',6' Where ProductID = 1
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Medicine SET number = number+@Medicine_number
            WHERE (Medicine_id = @Medicine_id)");
        cmd.Parameters.Add("@Medicine_id", SqlDbType.Int).Value = a;
        cmd.Parameters.Add("@Medicine_number", SqlDbType.Float).Value = b;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #endregion
    #region 添加物總量修改 (翔)
    public string Medicine_number_modify(string a, string b)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Medicine SET number = @number WHERE (Medicine_id = @Medicine_id)");
        cmd.Parameters.Add("@Medicine_id", SqlDbType.Int).Value = a;
        cmd.Parameters.Add("@number", SqlDbType.NVarChar, 10).Value = b;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 添加物歷史紀錄修改 (翔)
    public string MedicinePurchase_modify(string a, string b)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Medicine_Purchase SET number = @number WHERE     
                    (Medicine_Purchase_id = @Medicine_Purchase_id)");
        cmd.Parameters.Add("@Medicine_Purchase_id", SqlDbType.Int).Value = a;
        cmd.Parameters.Add("@number", SqlDbType.Float).Value = b;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 添加物操作
    #region 添加物進貨新增 MedicinePurchase 歷史表(翔)
    public string MedicinePurchase_Insert(string a, string b, string c)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"INSERT INTO Medicine_Purchase
                            (Medicine_id, number, date) VALUES
                        (@Medicine_id, @MedicinePurchase_number, @MedicinePurchase_date)");
        cmd.Parameters.Add("@Medicine_id", SqlDbType.Int).Value = a;
        cmd.Parameters.Add("@MedicinePurchase_number", SqlDbType.Float).Value = b;
        cmd.Parameters.Add("@MedicinePurchase_date", SqlDbType.DateTime2, 7).Value = c;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 添加物進貨包數修改(翔)//尚未使用
    public string MedicinePurchase_Modify(string a, string b)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Medicine_Purchase SET
        number = @Medicine_Purchase_number, date = @Medicine_Purchase_date
            WHERE (Medicine_id = @Original_Medicine_id)");
        cmd.Parameters.Add("@Medicine_Purchase_number", SqlDbType.Int).Value = a;
        cmd.Parameters.Add("@Original_Medicine_id", SqlDbType.Int).Value = b;
        cmd.Parameters.Add("@Medicine_Purchase_date", SqlDbType.DateTime2, 7).Value = DateTime.Now.ToShortDateString();
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 添加物 MedicinePurchase_catch_id(翔)
    public DataTable MedicinePurchase_catch_id(string supplier, string feed)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Medicine WHERE (Fish_company_id = @supplier) AND (Medicine_name = @medicine)");
        cmd.Parameters.Add("@supplier", SqlDbType.Int).Value = supplier;
        cmd.Parameters.Add("@medicine", SqlDbType.NVarChar, 10).Value = feed;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 添加物總進貨查詢 (翔)
    public DataTable Medicine_detail_view()
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Medicine INNER JOIN Medicine_Purchase ON (Medicine.Medicine_id = Medicine_Purchase.Medicine_id)
                                        LEFT JOIN Fish_company ON (Medicine.Fish_company_id = Fish_company.Fish_company_id) WHERE company_item = '添加物供應商'");
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 添加物包數顯示(翔)
    public DataTable MedicinePurchase_View()
    {
        SqlCommand cmd = new SqlCommand(@"SELECT Fish_company.company_name, Medicine.Medicine_name, Medicine.number
                    FROM Medicine INNER JOIN Fish_company ON Medicine.Fish_company_id = Fish_company.Fish_company_id 
                ORDER BY   Fish_company.company_name");
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 添加物新增後顯示(翔)
    public DataTable Medicine_View()
    {
        SqlCommand cmd = new SqlCommand(@"SELECT Medicine.Medicine_name, Medicine.Medicine_id, Fish_company.company_name
                    FROM Medicine INNER JOIN Fish_company ON Medicine.Fish_company_id = Fish_company.Fish_company_id");
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 添加物更新數量(翔)
    public DataTable MedicineSelectSum(string a)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT SUM (number) Medicine_Purchase FROM Medicine_Purchase WHERE Medicine_id = @Medicine_id");
        cmd.Parameters.Add("@Medicine_id", SqlDbType.Int).Value = a;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 添加物庫存新增(翔)
    public string Medicine_Insert(string a, string b)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"INSERT INTO Medicine
                            (Fish_company_id, Medicine_name, number) VALUES
                        (@Fish_company_id, @Medicine_name,0)");
        cmd.Parameters.Add("@Medicine_name", SqlDbType.NVarChar, 50).Value = a;
        cmd.Parameters.Add("@Fish_company_id", SqlDbType.Int, 50).Value = b;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 連動供應商的添加物名稱顯示(翔)
    public DataTable MedicinePurchaseName_View(string a)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Medicine where Fish_company_id =@Fish_company_id ");
        cmd.Parameters.Add("@Fish_company_id", SqlDbType.Int).Value = a;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #endregion   
    #region 魚池操作
    #region 魚池顯示(Renwu)
    public DataTable Pool_View()
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Pool");
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 魚池新增(Renwu)
    public string Pool_Insert(string a)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"INSERT INTO Pool (Pool_id) VALUES (@Pool_id)");
        cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = a;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 魚池查詢(sen)
    public DataTable Pool_serch(string Pool_id)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Pool WHERE Pool_id = @Pool_id");
        cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 50).Value = Pool_id;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 更新魚池狀態(Renwu)
    public void Pool_status(string Pool_id, string number, int Fish_detail_id)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Pool SET Pool_status = @Pool_status, Pool_number =@Pool_number,
                    Fish_detail_id = @Fish_detail_id WHERE (Pool_id = @Pool_id)");
        cmd.Parameters.Add("@Pool_status", SqlDbType.Bit).Value = true;
        cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = Pool_id;
        cmd.Parameters.Add("@Pool_number", SqlDbType.Int).Value = number;
        cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int).Value = Fish_detail_id;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";


    }
    #endregion
    #endregion
    #region 供應商操作
    #region 連動供應商的飼料名稱顯示(Renwu)
    public DataTable FeedPurchaseName_View(string a)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Fodder where Fish_company_id =@Fish_company_id ");
        cmd.Parameters.Add("@Fish_company_id", SqlDbType.Int).Value = a;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 供應商查詢(sen)
    public DataTable Supplier_Search(string Fish_company_id)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Fish_company WHERE Fish_company_id = @Fish_company_id");
        cmd.Parameters.Add("@Fish_company_id", SqlDbType.Int, 50).Value = Fish_company_id;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion 
    #region 更新廠商時間(Renwu)
    public string company_time(string a)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Fish_company SET last_use_time = @last_use_time WHERE     
                    (Fish_company_id = @Original_Fish_company_id)");
        cmd.Parameters.Add("@Original_Fish_company_id", SqlDbType.Int).Value = a;
        cmd.Parameters.Add("@last_use_time", SqlDbType.DateTime2, 7).Value = DateTime.Now.ToShortDateString();
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 廠商飼料名稱修改(Renwu)
    public string feed_name_modify(string a, string b)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Fodder SET Fodder_name = @Fodder_name WHERE     
                    (Fodder_id = @Fodder_id)");
        cmd.Parameters.Add("@Fodder_id", SqlDbType.Int).Value = a;
        cmd.Parameters.Add("@Fodder_name", SqlDbType.NVarChar, 10).Value = b;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #endregion
    #region 入漁操作
    #region 新增入魚資料(Renwu)
    public int Into_Fish_Insert(string fish_classification, string supplier_classification, string into_time1, string into_time2, string fry_weight, string start_number)
    {

        DateTime cdate = DateTime.Parse(into_time2);//轉型
        SqlCommand cmd = new SqlCommand(@"INSERT INTO Fish (Fish_kind_id, Fish_company_id, Spawning_date, Insert_fry_date, Insert_year, Fry_weight, number) OUTPUT INSERTED.Fish_id
                VALUES (@Fish_kind_id,@Fish_company_id,@Spawning_date,@Insert_fry_date,@Insert_year,@Fry_weight,@number) ");
        cmd.Parameters.Add("@Fish_kind_id", SqlDbType.Int).Value = fish_classification;
        cmd.Parameters.Add("@Fish_company_id", SqlDbType.Int).Value = supplier_classification;
        cmd.Parameters.Add("@Spawning_date", SqlDbType.DateTime2, 7).Value = into_time1;
        cmd.Parameters.Add("@Insert_fry_date", SqlDbType.DateTime2, 7).Value = into_time2;
        cmd.Parameters.Add("@Insert_year", SqlDbType.NVarChar, 50).Value = Convert.ToInt16(cdate.AddYears(-1911).Year).ToString();
        cmd.Parameters.Add("@Fry_weight", SqlDbType.NVarChar, 20).Value = fry_weight;
        cmd.Parameters.Add("@number", SqlDbType.Int).Value = start_number;

        int result = Fish.SqlHelper.Return_IDENTITY(cmd);
        return result;
    }
    #endregion
    #region 抓入魚最大id(Renwu)
    public DataTable Into_Fish_MaxId()
    {
        SqlCommand cmd = new SqlCommand(@"SELECT MAX(Fish_id) AS Fish_id FROM Fish");
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 新增入魚細節(Renwu)
    public string Into_Fish_Detail(string Fish_id, string number, string Pool_id, string into_time2, string Fish_AVGweight)
    {

        SqlCommand cmd = new SqlCommand(@"INSERT INTO Fish_detail (Fish_id, number,Move_date,Fish_AVGweight,Stay_Pools)  OUTPUT INSERTED.Fish_detail_id  VALUES  
                (@Fish_id,@number,@Move_date,@Fish_AVGweight,@Stay_Pools)");
        cmd.Parameters.Add("@Fish_id", SqlDbType.Int).Value = Fish_id;
        cmd.Parameters.Add("@number", SqlDbType.Int).Value = number;
        cmd.Parameters.Add("@Move_date", SqlDbType.DateTime2).Value = into_time2;
        cmd.Parameters.Add("@Fish_AVGweight", SqlDbType.NVarChar, 10).Value = Fish_AVGweight;
        cmd.Parameters.Add("@Stay_Pools", SqlDbType.NVarChar, 10).Value = Pool_id;
        int result = Fish.SqlHelper.Return_IDENTITY(cmd);
        /**加入入苗測量資料**/
        SqlCommand cmd_measure = new SqlCommand(@"INSERT INTO Measuring (Pool_id, Fish_detail_id, number, Fish_AVGweight, date, before_number, before_Fish_AVGweight, status)
            VALUES (@id,@Fish_detail_id,@number,@Fish_AVGweight,@date,@before_number,@before_Fish_AVGweight,@status)");
        cmd_measure.Parameters.Add("@id", SqlDbType.NVarChar,10).Value = Pool_id;
        cmd_measure.Parameters.Add("@Fish_detail_id", SqlDbType.Int).Value = result;
        cmd_measure.Parameters.Add("@number", SqlDbType.Int).Value = number;
        cmd_measure.Parameters.Add("@Fish_AVGweight", SqlDbType.NVarChar, 10).Value = Fish_AVGweight;
        cmd_measure.Parameters.Add("@date", SqlDbType.DateTime2).Value = into_time2;
        cmd_measure.Parameters.Add("@before_number", SqlDbType.NVarChar, 10).Value = number;
        cmd_measure.Parameters.Add("@before_Fish_AVGweight", SqlDbType.NVarChar, 10).Value = Fish_AVGweight;
        cmd_measure.Parameters.Add("@status", SqlDbType.NVarChar, 10).Value = "入苗";
        int measure = Fish.SqlHelper.cmdCheck(cmd_measure);
        /**end加入入苗測量資料**/
        if (result != 0)
        {
            Pool_status(Pool_id, number, result);
        }
       
        return "success";

    }
    #endregion
    #endregion  
    #region 餵食操作
    #region 餵養紀錄新增(建)
    public string feed_insert(string Fodder_id, string Pool_id, string Fish_detail_id, string Fodder_number, string date, string Bait, string Medicine_id, string medicine_number,string DayTime)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand
       (@"INSERT INTO Feed (Fodder_id, Pool_id, Fish_detail_id, Fodder_number, date, Bait, Medicine_id,medicine_number,DayTime)
                VALUES (@Fodder_id,@Pool_id,@Fish_detail_id,@Fodder_number,@date,@Bait,@Medicine_id,@medicine_number,@DayTime)");
        cmd.Parameters.Add("@Fodder_id", SqlDbType.Int).Value = Fodder_id;
        cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = Pool_id;
        cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int).Value = Fish_detail_id;
        cmd.Parameters.Add("@Fodder_number", SqlDbType.NVarChar, 10).Value = Fodder_number;
        cmd.Parameters.Add("@date", SqlDbType.DateTime2).Value = date;
        cmd.Parameters.Add("@Bait", SqlDbType.NVarChar, 10).Value = Bait;
        cmd.Parameters.Add("@Medicine_id", SqlDbType.Int).Value = Medicine_id;
        cmd.Parameters.Add("@medicine_number", SqlDbType.NVarChar, 10).Value = medicine_number;
        cmd.Parameters.Add("@DayTime", SqlDbType.NVarChar, 10).Value = DayTime;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }

    #endregion
    #region 餵食紀錄飼料減少(建)
    public string Fodder_Cut(string Fodder_id, string Fodder_number)
    {
        //Update Products Set ProductName = ProductName + ',6' Where ProductID = 1
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Fodder SET Fodder_number = Fodder_number-@Fodder_number
            WHERE (Fodder_id = @Fodder_id)");
        cmd.Parameters.Add("@Fodder_id", SqlDbType.Int).Value = Fodder_id;
        cmd.Parameters.Add("@Fodder_number", SqlDbType.Float).Value = Fodder_number;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 餵食紀錄添加物減少(建)
    public string Medicine_Cut(string Medicine_id, string number)
    {
        //Update Products Set ProductName = ProductName + ',6' Where ProductID = 1
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Medicine SET number = (number-@number)
            WHERE (Medicine_id = @Medicine_id)");
        cmd.Parameters.Add("@Medicine_id", SqlDbType.Int).Value = Medicine_id;
        cmd.Parameters.Add("@number", SqlDbType.Int).Value = number;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 餵食紀錄刪除
    public string feed_delete(string feed_id)
    {
        //Update Products Set ProductName = ProductName + ',6' Where ProductID = 1
        string result = "";
        SqlCommand cmd = new SqlCommand(@"Delete Feed  WHERE (Feed_id = @feed_id)");
        cmd.Parameters.Add("@feed_id", SqlDbType.Int).Value = feed_id;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 餵食資料查詢
    public DataTable feed_view(string Pool_id, string Fish_detail_id)
    {

        SqlCommand cmd = new SqlCommand(@"SELECT Feed.Feed_id, Feed.Fodder_id,Feed.DayTime, Fish_company.company_name AS F_Comp_name, Fodder.Fodder_name, 
                                                 Feed.Pool_id, Feed.Fish_detail_id, Feed.Fodder_number, Feed.date, Feed.Bait, Feed.Medicine_id, 
                                                 Fish_company_1.company_name AS M_Comp_name, Medicine.Medicine_name, Feed.medicine_number, 
                                                 Medicine.Fish_company_id
                                                 FROM Feed
                                                 INNER JOIN
                                                 Fodder ON Feed.Fodder_id = Fodder.Fodder_id INNER JOIN
                                                 Medicine ON Feed.Medicine_id = Medicine.Medicine_id INNER JOIN
                                                 Fish_company ON Fodder.Fish_company_id = Fish_company.Fish_company_id INNER JOIN
                                                 Fish_company AS Fish_company_1 ON Medicine.Fish_company_id = Fish_company_1.Fish_company_id 
                                                 WHERE (Pool_id = @Pool_id) AND ( Fish_detail_id=@Fish_detail_id)");
        cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = Pool_id;
        cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int, 50).Value = Fish_detail_id;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 餵食資料查詢(單一)
    public DataTable feed_view_one(string feed_id)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Feed  WHERE (Feed_id = @feed_id) ");
        cmd.Parameters.Add("@feed_id", SqlDbType.Int, 10).Value = feed_id;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #endregion
    #region 魚群操作(sen)
    #region 魚群細節查詢(sen)
    public DataTable Fish_detail_view(string Fish_detail_id)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Fish_detail WHERE Fish_detail_id = @Fish_detail_id");
        cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int, 50).Value = Fish_detail_id;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 魚群查詢(sen)
    public DataTable Fish_view(string Fish_id)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Fish WHERE Fish_id = @Fish_id");
        cmd.Parameters.Add("@Fish_id", SqlDbType.Int, 50).Value = Fish_id;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #endregion
    #region 分養(sen)
    #region 更新魚池狀態(sen)
    public string Pool_update(string Pool_id, bool Pool_status, int number, string Fish_detail_id)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Pool SET Pool_status = @Pool_status, Pool_number =@Pool_number,
                    Fish_detail_id = @Fish_detail_id WHERE (Pool_id = @Pool_id)");
        cmd.Parameters.Add("@Pool_status", SqlDbType.Bit).Value = Pool_status;
        cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = Pool_id;
        cmd.Parameters.Add("@Pool_number", SqlDbType.Int).Value = number;
        cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int).Value = Fish_detail_id;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 更新魚群細節(sen)
    public string Fish_detail_update(int Fish_detail_id, int number)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Fish_detail SET number = @number WHERE (Fish_detail_id = @Fish_detail_id)");
        cmd.Parameters.Add("@number", SqlDbType.Int).Value = number;
        cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int).Value = Fish_detail_id;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 新增分養表(sen)
    public string Distribution_Insert(string Past_pool_id, string Past_Fish_detail_id, string move_date, string move_pool_id, string Fish_AVGweight, string Fish_size, string Fish_number)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"    INSERT INTO Distribution  (Past_pool_id, Past_Fish_detail_id, move_date, move_pool_id, Fish_AVGweight, Fish_size, Fish_number) VALUES 
            (@Past_pool_id, @Past_Fish_detail_id, @move_date, @move_pool_id, @Fish_AVGweight, @Fish_size, @Fish_number)");
        cmd.Parameters.Add("@Past_pool_id", SqlDbType.NVarChar, 10).Value = Past_pool_id;
        cmd.Parameters.Add("@Past_Fish_detail_id", SqlDbType.Int, 10).Value = Past_Fish_detail_id;
        cmd.Parameters.Add("@move_date", SqlDbType.DateTime2, 10).Value = move_date;
        cmd.Parameters.Add("@move_pool_id", SqlDbType.NVarChar, 10).Value = move_pool_id;
        cmd.Parameters.Add("@Fish_AVGweight", SqlDbType.NVarChar, 10).Value = Fish_AVGweight;
        cmd.Parameters.Add("@Fish_size", SqlDbType.NVarChar, 10).Value = Fish_size;
        cmd.Parameters.Add("@Fish_number", SqlDbType.Int, 10).Value = Fish_number;

        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion


    #region 魚群細節新增回傳編號
    public int Fish_detail_insert(string Fish_id, string Origin_Fish_detail_id, string number, string Fish_AVGweight, string Fish_size, string Move_date)
    {
        SqlCommand cmd = new SqlCommand(@"INSERT INTO Fish_detail
                            (Fish_id, Fish_AVGweight,Move_date,number,Origin_Fish_detail_id,Fish_size) OUTPUT INSERTED.Fish_detail_id VALUES
                        (@Fish_id, @Fish_AVGweight,@Move_date,@number,@Origin_Fish_detail_id,@Fish_size)");
        cmd.Parameters.Add("@Fish_id", SqlDbType.Int, 50).Value = Fish_id;
        cmd.Parameters.Add("@Fish_AVGweight", SqlDbType.NVarChar, 10).Value = Fish_AVGweight;
        cmd.Parameters.Add("@Move_date", SqlDbType.DateTime2).Value = Move_date;
        cmd.Parameters.Add("@number", SqlDbType.Int, 50).Value = number;
        cmd.Parameters.Add("@Origin_Fish_detail_id", SqlDbType.Int, 50).Value = Origin_Fish_detail_id;
        cmd.Parameters.Add("@Fish_size", SqlDbType.NVarChar, 10).Value = Fish_size;
        int result = Fish.SqlHelper.Return_IDENTITY(cmd);
        return result;
    }
    #endregion
    #endregion
    #region 魚池詳細
    #region 魚池查供應商
    public DataTable Supplier_ID_Fish()
    {
        //1. Pool表單、Fish_detail表單 >> Fish_detail_id   
        //2. Fish_detail表單、Fish表單 >> Fish_id
        //3. Fish表單 >> Fish_company_id
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Pool INNER JOIN Fish_detail ON (Pool.Fish_detail_id = Fish_detail.Fish_detail_id)
                                        LEFT JOIN Fish ON (Fish_detail.Fish_id = Fish.Fish_id)
                                        LEFT JOIN Fish_company ON (Fish.Fish_company_id = Fish_company.Fish_company_id)");
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #endregion
    #region 出魚操作
    #region 出魚記錄新增
    public string Outfish_Insert(string Pool_id, string number, string Fish_AVGweight, string Waistline, string bust, string Tail, string Fish_detail_id, string Outside_date)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand(@"INSERT INTO Out
                            (Pool_id,number,Fish_AVGweight,Waistline,bust,Tail,Fish_detail_id,Outside_date) VALUES
                        (@Pool_id,@number,@Fish_AVGweight,@Waistline,@bust,@Tail,@Fish_detail_id,@Outside_date)");
        cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 50).Value = Pool_id;
        cmd.Parameters.Add("@number", SqlDbType.Int, 10).Value = number;
        cmd.Parameters.Add("@Fish_AVGweight", SqlDbType.NVarChar, 50).Value = Fish_AVGweight;
        cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int, 10).Value = Fish_detail_id;
        cmd.Parameters.Add("@Outside_date", SqlDbType.DateTime2, 10).Value = Outside_date;
        cmd.Parameters.Add("@Waistline", SqlDbType.NVarChar, 10).Value = Waistline;
        cmd.Parameters.Add("@bust", SqlDbType.NVarChar, 10).Value = bust;
        cmd.Parameters.Add("@Tail", SqlDbType.NVarChar, 10).Value = Tail;


        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    #region 出魚查詢
    public DataTable Outfish_View(string Pool_id, string Fish_detail_id)
    {
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Out  WHERE (Pool_id = @Pool_id) AND ( Fish_detail_id=@Fish_detail_id)");
        cmd.Parameters.Add("@Pool_id", SqlDbType.NVarChar, 10).Value = Pool_id;
        cmd.Parameters.Add("@Fish_detail_id", SqlDbType.Int, 50).Value = Fish_detail_id;
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        return dt;
    }
    #endregion
    #region 出魚紀錄刪除
    public string Out_delete(string Out_id, string Fish_detail_id, string total, string Pool_id)
    {
        //Update Products Set ProductName = ProductName + ',6' Where ProductID = 1
        string result = "";
        SqlCommand cmd = new SqlCommand(@"Delete Out  WHERE (Outside_id = @Out_id)");
        cmd.Parameters.Add("@Out_id", SqlDbType.Int).Value = Out_id;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        string re_ = Fish_detail_update(Int32.Parse(Fish_detail_id), Int32.Parse(total));
        string re_2 = Measuring_UP_Pool(Pool_id, Int32.Parse(total));
        if (Equals(re_, "success"))
        {
            if (Equals(re_2, "success"))
            {
                return result;
            }
            else
            {
                return "fail";
            }
        }
        else
        {
            return "fail";
        }

    }
    #endregion
    #region 出魚紀錄修改
    public string Out_Update(string Out_id, string Fish_detail_id, string total, string Pool_id, string out_number, string Out_Fish_AVGweight, string Waistline, string bust, string Tail)
    {
        //Update Products Set ProductName = ProductName + ',6' Where ProductID = 1
        string result = "";
        SqlCommand cmd = new SqlCommand(@"UPDATE Out SET number=@number,Waistline=@Waistline,bust=@bust,Tail=@Tail, Fish_AVGweight=@Fish_AVGweight WHERE (Outside_id = @Out_id)");
        cmd.Parameters.Add("@Out_id", SqlDbType.Int).Value = Out_id;
        cmd.Parameters.Add("@number", SqlDbType.Int).Value = out_number;
        cmd.Parameters.Add("@Fish_AVGweight", SqlDbType.NVarChar).Value = Out_Fish_AVGweight;
        cmd.Parameters.Add("@Waistline", SqlDbType.NVarChar, 10).Value = Waistline;
        cmd.Parameters.Add("@bust", SqlDbType.NVarChar, 10).Value = bust;
        cmd.Parameters.Add("@Tail", SqlDbType.NVarChar, 10).Value = Tail;
        int check_num = Fish.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        string re_ = Fish_detail_update(Int32.Parse(Fish_detail_id), Int32.Parse(total));
        string re_2 = Measuring_UP_Pool(Pool_id, Int32.Parse(total));
        if (Equals(re_, "success"))
        {
            if (Equals(re_2, "success"))
            {
                return result;
            }
            else
            {
                return "fail";
            }
        }
        else
        {
            return "fail";
        }

    }
    #endregion
    #endregion


    #region Measuring修改status欄位  .bat
    public string modify_ststus()
    {
        int result = 0;
        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Measuring ");
        DataTable dt = Fish.SqlHelper.cmdTable(cmd);
        for(int i = 0; i< dt.Rows.Count;i++)
        {
            if(dt.Rows[i][4].ToString() == "-1")
            {
                SqlCommand cmd_modify = new SqlCommand(@"UPDATE Measuring SET
                    status = @status WHERE (Measuring_id = @Measuring_id)");
                cmd_modify.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = "耗損";
                cmd_modify.Parameters.Add("@Measuring_id", SqlDbType.Int).Value = dt.Rows[i][0];
                int check_num = Fish.SqlHelper.cmdCheck(cmd_modify);
                result += 1;
            }else
            {
                SqlCommand cmd_modify = new SqlCommand(@"UPDATE Measuring SET
                    status = @status WHERE (Measuring_id = @Measuring_id)");
                cmd_modify.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = "測量";
                cmd_modify.Parameters.Add("@Measuring_id", SqlDbType.Int).Value = dt.Rows[i][0];
                int check_num = Fish.SqlHelper.cmdCheck(cmd_modify);
                result += 1;
            }
        }
        return result.ToString();
    }
    #endregion
    }