<%@ WebHandler Language="C#" Class="Fish_detail_update" %>

using System;
using System.Web;

public class Fish_detail_update : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        string Fish_detail_id = context.Request.Form["Fish_detail_id"].ToString();
        string number = context.Request.Form["number"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.Fish_detail_update(Int32.Parse(Fish_detail_id),Int32.Parse(number));
        context.Response.Write(re_);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}