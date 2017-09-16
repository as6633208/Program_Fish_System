<%@ WebHandler Language="C#" Class="feed_insert" %>

using System;
using System.Web;

public class feed_insert : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string Fodder_id = context.Request.Form["Fodder_id"].ToString();
        string Pool_id =context.Request.Form["Pool_id"].ToString();
        string Fish_detail_id = context.Request.Form["Fish_detail_id"].ToString();
        string Fodder_number = context.Request.Form["Fodder_number"].ToString();
        string date = context.Request.Form["data"].ToString();
        string Bait = context.Request.Form["Bait"].ToString();
        string Medicine_id =context.Request.Form["Medicine_id"].ToString();
        string medicine_number = context.Request.Form["medicine_number"].ToString();


        Method_Fish method = new Method_Fish();
        string re_ = method.feed_insert(Fodder_id,Pool_id,Fish_detail_id,Fodder_number,date,Bait,Medicine_id,medicine_number);
        context.Response.Write(re_);

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}