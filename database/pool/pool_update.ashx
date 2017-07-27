<%@ WebHandler Language="C#" Class="pool_update" %>

using System;
using System.Web;

public class pool_update : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");

        } else
        {
            Method_Fish method = new Method_Fish();
           string Pool_id = context.Request.Form["Pool_id"].ToString();
           bool Pool_status =  Convert.ToBoolean(context.Request.Form["Pool_status"]);
           string number = context.Request.Form["number"].ToString();
           string Fish_detail_id = context.Request.Form["Fish_detail_id"].ToString();
           string re_ = method.Pool_update(Pool_id, Pool_status, 0,Fish_detail_id);
          context.Response.Write(re_);  
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}