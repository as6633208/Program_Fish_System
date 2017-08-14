<%@ WebHandler Language="C#" Class="admin_modify" %>

using System;
using System.Web;

public class admin_modify : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");
        }else
        { 
            string authority_acc = context.Request.Form["authority_acc"].ToString();
            string authority_pass = context.Request.Form["authority_pass"].ToString();
            string authority_classification = context.Request.Form["authority_classification"].ToString();                
            string authority_id = context.Request.Form["authority_id"].ToString();
            Method_Fish method = new Method_Fish();
            string re_ = method.Account_Modify(authority_acc, authority_pass, authority_classification,authority_id);
            context.Response.Write(re_);//查看有無資料
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}