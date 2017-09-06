<%@ WebHandler Language="C#" Class="feed_name_modify" %>

using System;
using System.Web;

public class feed_name_modify : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string a = context.Request.Form["modify_id"].ToString();
        string b = context.Request.Form["fish_kind_name"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.feed_name_modify(a, b);
        context.Response.Write(re_);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}