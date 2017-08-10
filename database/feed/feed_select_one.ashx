<%@ WebHandler Language="C#" Class="feed_select_one" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class feed_select_one : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
     

            string feed_id = context.Request.Form["feed_id"].ToString();
            Method_Fish method = new Method_Fish();
            DataTable re_ = method.feed_view_one(feed_id);
            string str_json = JsonConvert.SerializeObject(re_, Formatting.Indented);
            context.Response.Write(str_json);//回傳資料
        
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}