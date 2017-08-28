<%@ WebHandler Language="C#" Class="measuring_view_one" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class measuring_view_one : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
          if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");
        } else
        {
            string Measuring_id = context.Request.Form["Measuring_id"].ToString();
            Method_Fish method = new Method_Fish();
            DataTable re_ = method.measuring_view_one(Measuring_id);
            string str_json = JsonConvert.SerializeObject(re_, Formatting.Indented);
            context.Response.Write(str_json);//回傳資料
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}