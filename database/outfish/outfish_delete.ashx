<%@ WebHandler Language="C#" Class="outfish_delete" %>

using System;
using System.Web;

public class outfish_delete : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
    string Out_id=context.Request.Form["Out_id"].ToString();
    string Fish_detail_id=context.Request.Form["Fish_detail_id"].ToString();
    string total=context.Request.Form["total"].ToString();
    string Pool_id=context.Request.Form["Pool_id"].ToString();

        Method_Fish metod = new Method_Fish();
        string re_ = metod.Out_delete(Out_id,Fish_detail_id,total,Pool_id);
        context.Response.Write(re_);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}