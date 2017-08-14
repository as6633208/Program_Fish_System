<%@ WebHandler Language="C#" Class="medicinePurchase_insert" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class medicinePurchase_insert : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string a = context.Request.Form["Fodder_id"].ToString();
        string b = context.Request.Form["number"].ToString();
        string c = context.Request.Form["Purchase_time"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.MedicinePurchase_Insert(a, b, c);
        context.Response.Write(re_);//查看有無資料
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}