﻿<%@ WebHandler Language="C#" Class="login" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class login : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");
        }
        else
        {
            string a = context.Request.Form["username"].ToString();
            string b = context.Request.Form["password"].ToString();
            Method_Fish method = new Method_Fish();
            DataTable re_ = method.Login(a, b);
            string str_json = JsonConvert.SerializeObject(re_, Formatting.Indented);
            context.Response.Write(str_json);//回傳資料 
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}