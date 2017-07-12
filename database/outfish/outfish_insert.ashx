<%@ WebHandler Language="C#" Class="outfish_insert" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class outfish_insert : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");

        } else {
            string Pool_id = context.Request.Form["Fish_company_id"].ToString();
            string number = context.Request.Form["Fish_company_id"].ToString();
            string Fish_AVGweight = context.Request.Form["Fish_company_id"].ToString();
            string Fish_detail_id = context.Request.Form["Fish_company_id"].ToString();
            string Outside_date = context.Request.Form["Fish_company_id"].ToString();
            Method_Fish method = new Method_Fish();
            string re_ = method.Outfish_Insert(Pool_id,number,Fish_AVGweight,Fish_detail_id,Outside_date);          
            context.Response.Write(re_);//回傳資料
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}