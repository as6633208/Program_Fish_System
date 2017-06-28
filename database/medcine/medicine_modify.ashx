<%@ WebHandler Language="C#" Class="medicine_modify" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class medicine_modify : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string a = context.Request.Form["Fodder_id"].ToString();
        string b = context.Request.Form["number"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.Medicine_Modify(a, b);
        context.Response.Write(re_);//查看有無資料
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}