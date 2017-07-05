<%@ WebHandler Language="C#" Class="login" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class login : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string a = context.Request.Form["username"].ToString();
        string b = context.Request.Form["password"].ToString();
        Method_Fish method = new Method_Fish();
        DataTable re_ = method.Login(a,b);
        context.Response.Write(re_.Rows[0][0]);//查看有無資料
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}