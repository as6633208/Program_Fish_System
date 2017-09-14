<%@ WebHandler Language="C#" Class="feed_name_modify" %>

using System;
using System.Web;

public class feed_name_modify : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string a = context.Request.Form["modify_id"].ToString();
        string b = context.Request.Form["modify_number"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.FeedPurchase_modify(a, b);
        context.Response.Write(re_);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}