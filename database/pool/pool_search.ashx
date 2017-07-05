<%@ WebHandler Language="C#" Class="pool_search" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class pool_search : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string Pool_id = context.Request.Form["Pool_id"].ToString();
        Method_Fish method = new Method_Fish();
        DataTable re_ = method.Pool_serch(Pool_id);
        context.Response.Write(re_);//查看有無資料
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}