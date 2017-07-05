<%@ WebHandler Language="C#" Class="medicinePurchase_modify" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class medicinePurchase_modify : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string a = "87";// context.Request.Form["supplier_name"].ToString();
        string b = "2";// context.Request.Form["supplier_sname"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.MedicinePurchase_Modify(a, b);
        context.Response.Write(re_);//查看有無資料
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}