<%@ WebHandler Language="C#" Class="admin_delete" %>

using System;
using System.Web;

public class admin_delete : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");
        }else
        {       
            string authority_id = context.Request.Form["authority_id"].ToString();
            Method_Fish method = new Method_Fish();
            string re_ = method.Account_Delete(authority_id);
            context.Response.Write(re_);//查看有無資料
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}