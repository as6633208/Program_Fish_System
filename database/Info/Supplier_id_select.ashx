<%@ WebHandler Language="C#" Class="Into_fish_detail" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Into_fish_detail : IHttpHandler {

    public void ProcessRequest (HttpContext context) 
        {
            Method_Fish method = new Method_Fish();
            DataTable re_ = method.Supplier_ID_Fish();
            string str_json = JsonConvert.SerializeObject(re_, Formatting.Indented);
            context.Response.Write(str_json);//回傳資料
        }
    

    public bool IsReusable {
        get {
            return false;
        }
    }

}