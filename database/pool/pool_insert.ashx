<%@ WebHandler Language="C#" Class="pool_insert" %>

using System;
using System.Web;

public class pool_insert : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string a = context.Request.Form["Pool_id"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.Pool_Insert(a);
        context.Response.Write(re_);//查看有無資料
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}