<%@ WebHandler Language="C#" Class="measuring_update" %>
using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class measuring_update : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");
        } else
        {
        string Pool_id =context.Request.Form["Pool_id"].ToString();
        int Fish_detail_id = Int32.Parse(context.Request.Form["Fish_detail_id"].ToString());
        //使用者輸入資料
        int number =Int32.Parse(context.Request.Form["number"].ToString());
        string Fish_AVGweight = context.Request.Form["Measuring_Fish_AVGweight"].ToString(); 
        string date =context.Request.Form["date"].ToString();
        string before_number =context.Request.Form["Fish_detail_before_number"].ToString();
        string before_Fish_AVGweight =context.Request.Form["Fish_detail_before_Fish_AVGweight"].ToString();
        string status = context.Request.Form["status"].ToString();
        string id=context.Request.Form["id"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.Measuring_update(Pool_id,Fish_detail_id,number,Fish_AVGweight,date,before_number,before_Fish_AVGweight,id,status);        
        context.Response.Write(re_);//回傳資料
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}