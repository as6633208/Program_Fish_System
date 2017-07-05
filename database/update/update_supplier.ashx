<%@ WebHandler Language="C#" Class="update_supplier" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class update_supplier : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string company_id = context.Request.Form["company_time"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.company_time(company_id);
        context.Response.Write(re_);//查看有無資料

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}