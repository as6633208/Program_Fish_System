<%@ WebHandler Language="C#" Class="medicine_insert" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class medicine_insert : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string a = context.Request.Form["fodder_name"].ToString();
        string b = context.Request.Form["supplier_name"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.Medicine_Insert(a, b);
        context.Response.Write(re_);//查看有無資料
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}