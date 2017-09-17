<%@ WebHandler Language="C#" Class="MedicinePurchase_modify2" %>

using System;
using System.Web;

public class MedicinePurchase_modify2 : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string a = context.Request.Form["Medicine_Purchase_id"].ToString();
        string b = context.Request.Form["number"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.MedicinePurchase_modify(a, b);
        context.Response.Write(re_);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}