﻿<%@ WebHandler Language="C#" Class="feedPurchase_insert" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class feedPurchase_insert : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string a = context.Request.Form["Fodder_id"].ToString();
        string b = context.Request.Form["number"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.FeedPurchase_Insert(a, b);
        context.Response.Write(re_);//查看有無資料
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}