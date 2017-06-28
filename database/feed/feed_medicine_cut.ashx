<%@ WebHandler Language="C#" Class="feed_medicine_cut" %>

using System;
using System.Web;

public class feed_medicine_cut : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
         string Medicine_id=context.Request.Form["Medicine_id"].ToString();
        string medicine_number=context.Request.Form["medicine_number"].ToString();
        Method_Fish metod = new Method_Fish();
        string re_ = metod.Medicine_Cut(Medicine_id, medicine_number);
        context.Response.Write(re_);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}