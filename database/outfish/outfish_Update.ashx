
<%@ WebHandler Language="C#" Class="outfish_Update" %>

using System;
using System.Web;

public class outfish_Update : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
    string Out_id=context.Request.Form["Out_id"].ToString();
    string Fish_detail_id=context.Request.Form["Fish_detail_id"].ToString();
    string total=context.Request.Form["total"].ToString();
    string Pool_id=context.Request.Form["Pool_id"].ToString();
    string Out_nubmer=context.Request.Form["Out_nubmer"].ToString();
    string Out_Fish_AVGweight=context.Request.Form["Out_Fish_AVGweight"].ToString();

        Method_Fish metod = new Method_Fish();
        string re_ = metod.Out_Update(Out_id,Fish_detail_id,total,Pool_id,Out_nubmer,Out_Fish_AVGweight);
        context.Response.Write(re_);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}