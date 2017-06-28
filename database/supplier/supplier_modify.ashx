<%@ WebHandler Language="C#" Class="supplier_modify" %>

using System;
using System.Web;

public class supplier_modify : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string a = context.Request.Form["supplier_name"].ToString();
        string b = context.Request.Form["supplier_sname"].ToString();
        string c = context.Request.Form["modify_id"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.Supplier_Modify(a,b,c);
        context.Response.Write(re_);//查看有無資料
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}