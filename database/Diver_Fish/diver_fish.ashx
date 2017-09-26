<%@ WebHandler Language="C#" Class="diver_fish" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
public class diver_fish : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        string array = context.Request.Form["Fish_Diver_Arrray"].ToString();
        string Fish_detail_Fish_id = context.Request.Form["Fish_detail_Fish_id"].ToString(); //魚群編號
        string Fish_detail_id = context.Request.Form["Fish_detail_id"].ToString(); //
        string Page_Pool_id = context.Request.Form["Page_Pool_id"].ToString(); //原池編號
        string Fish_amount = context.Request.Form["count"].ToString(); //總魚數
        string Fish_Pool_number = context.Request.Form["Fush_Pool_Pool_number"].ToString(); //魚池數量
        string status = context.Request.Form["status"].ToString(); //status
        string[] fish_diver = array.Split('&');
        ArrayList all_varible = new ArrayList();
        foreach (var fish in fish_diver)
        {
            string[] diver = fish.Split('=');
            all_varible.Add(diver[1]);
        }
        //新增分養資料
        for (int index = 0; index < all_varible.Count; index += 5)
        {
            Method_Fish method = new Method_Fish();
            all_varible[index].ToString();
            DataTable dt = method.Pool_serch(all_varible[index].ToString()); //抓出份過去魚池資訊
            DataTable dt_fish_detail = method.Fish_detail_view(Fish_detail_id); //抓出原魚池 魚群細節
            if (dt.Rows[0]["Pool_status"].ToString() == "False")  //狀態為fasle 沒魚
            {
                string size = "0";
                if (dt_fish_detail.Rows[0]["Fish_size"].ToString() == "0")
                {
                    size = all_varible[index + 3].ToString();
                }else if(all_varible[index + 3].ToString() == "null"){  //轉池 沒體態
                    size = dt_fish_detail.Rows[0]["Fish_size"].ToString();
                }
                else
                {
                    size = dt_fish_detail.Rows[0]["Fish_size"].ToString() + all_varible[index + 3].ToString();
                }
                int re_ = method.Fish_detail_insert(Fish_detail_Fish_id,
                Fish_detail_id,
                all_varible[index + 1].ToString(), //分數量
                all_varible[index + 2].ToString(), //魚重量
                size, //體態
                all_varible[index + 4].ToString()); //時間
                //編輯魚池
                string result_pool = method.Pool_update(all_varible[index].ToString(),
                    true,
                    int.Parse(all_varible[index + 1].ToString()),
                    re_.ToString());
            }
            else   //狀態為true 有魚
            {
                if (int.Parse(dt.Rows[0]["Pool_number"].ToString()) > int.Parse(all_varible[index + 1].ToString()))  //魚池 > 分養
                {
                    //編輯魚池 分過去魚池
                    string re_ = method.Pool_update(all_varible[index].ToString(),
                        true,
                        int.Parse(dt.Rows[0]["Pool_number"].ToString()) + int.Parse(all_varible[index + 1].ToString()),
                        dt.Rows[0]["Fish_detail_id"].ToString());
                    //更新魚群細節數量
                    string fish_detail_update_result = method.Fish_detail_update(
                        int.Parse(dt.Rows[0]["Fish_detail_id"].ToString()),
                        int.Parse(dt.Rows[0]["Pool_number"].ToString()) + int.Parse(all_varible[index + 1].ToString()));
                }
                else //魚池 <= 分養
                {
                    string size = "0";
                    if (dt_fish_detail.Rows[0]["Fish_size"].ToString() == "0")
                    {
                        size = all_varible[index + 3].ToString();
                    }
                    else if ( all_varible[index + 3].ToString() == "null")
                    {  //轉池 沒體態
                        size = dt_fish_detail.Rows[0]["Fish_size"].ToString();
                    }
                    else
                    {
                        size = dt_fish_detail.Rows[0]["Fish_size"].ToString() + all_varible[index + 3].ToString();
                    }
                    //新增細節
                    int result_num = method.Fish_detail_insert(Fish_detail_Fish_id,
                    Fish_detail_id,
                    all_varible[index + 1].ToString(),
                    all_varible[index + 2].ToString(),
                    size,
                    all_varible[index + 4].ToString());
                    //編輯魚池
                    string result_pool = method.Pool_update(all_varible[index].ToString(),
                        true,
                        int.Parse(dt.Rows[0]["Pool_number"].ToString()) + int.Parse(all_varible[index + 1].ToString()),
                        result_num.ToString());
                }
            }
            //新增分養表
            string distribution_result = method.Distribution_Insert( Page_Pool_id,
                Fish_detail_id,
                all_varible[index + 4].ToString(),
                all_varible[index].ToString(),
                dt_fish_detail.Rows[0]["Fish_size"].ToString(),
                all_varible[index + 2].ToString(),
                all_varible[index + 1].ToString(),
                status);
        }
        Method_Fish method_pool = new Method_Fish();
        //更新原魚池
        string source_pool = method_pool.Pool_update(Page_Pool_id,
            true,
             int.Parse(Fish_Pool_number) -  int.Parse(Fish_amount),
            Fish_detail_id);
        //計算損益   a=池編號 b=魚池細節編號 c=分養池加總的數量 d=被分養池原本數量 e日期
        /*  method_pool.Inventory(Page_Pool_id,
              int.Parse(Fish_detail_id),
              int.Parse(Fish_amount), 
              int.Parse(Fish_Pool_number), 
              DateTime.Now.ToString());*/
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}