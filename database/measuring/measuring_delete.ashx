<%@ WebHandler Language="C#" Class="measuring_delete" %>

using System;
using System.Web;

public class measuring_delete : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}