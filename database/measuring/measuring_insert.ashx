<%@ WebHandler Language="C#" Class="Measuring_insert" %>
using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Measuring_insert : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        //魚池資料
        string Pool_id =context.Request.Form["Pool_id"].ToString();
        int Fish_detail_id = Int32.Parse(context.Request.Form["Fish_detail_id"].ToString());
        //使用者輸入資料
        int number =Int32.Parse(context.Request.Form["number"].ToString());
        string Fish_AVGweight = context.Request.Form["Measuring_Fish_AVGweight"].ToString();
        string date =context.Request.Form["date"].ToString();
        string before_number =context.Request.Form["Fish_detail_before_number"].ToString();
        string before_Fish_AVGweight =context.Request.Form["Fish_detail_before_Fish_AVGweight"].ToString();
        string status = context.Request.Form["status"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.Measuring_insert(Pool_id,Fish_detail_id,number,Fish_AVGweight,date,before_number,before_Fish_AVGweight,status);
        string str_json = JsonConvert.SerializeObject(re_, Formatting.Indented);
        context.Response.Write(re_);//查看有無資料

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}