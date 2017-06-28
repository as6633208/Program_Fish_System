<%@ WebHandler Language="C#" Class="feed_fodder_cut" %>

using System;
using System.Web;

public class feed_fodder_cut : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string Fodder_id=context.Request.Form["Fodder_id"].ToString();
        string Fodder_number=context.Request.Form["Fodder_number"].ToString();
        Method_Fish metod = new Method_Fish();
        string re_ = metod.Fodder_Cut(Fodder_id, Fodder_number);
        context.Response.Write(re_);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}