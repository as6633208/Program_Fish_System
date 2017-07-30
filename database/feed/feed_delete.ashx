<%@ WebHandler Language="C#" Class="feed_delete" %>

using System;
using System.Web;

public class feed_delete : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
       string Feed_id=context.Request.Form["Feed_id"].ToString();
        Method_Fish metod = new Method_Fish();
        string re_ = metod.feed_delete(Feed_id);
        context.Response.Write(re_);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}