<%@ WebHandler Language="C#" Class="admin_insert" %>

using System;
using System.Web;

public class admin_insert : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");
        }else
        {
            string authority_acc = context.Request.Form["authority_acc"].ToString();
            string authority_pass = context.Request.Form["authority_pass"].ToString();
            string authority_classification = context.Request.Form["authority_classification"].ToString();
            Method_Fish method = new Method_Fish();
            string re_ = method.Account_Insert(authority_acc, authority_pass, authority_classification);
            context.Response.Write(re_);
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}