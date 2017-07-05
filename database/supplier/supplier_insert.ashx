<%@ WebHandler Language="C#" Class="supplier_insert" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class supplier_insert : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string a = context.Request.Form["supplier_name"].ToString();
        string b = context.Request.Form["supplier_sname"].ToString();
        string c = context.Request.Form["supplier_classification"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.Supplier_Insert(a,b,c);
        context.Response.Write(re_);//查看有無資料
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}