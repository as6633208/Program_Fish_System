﻿<%@ WebHandler Language="C#" Class="Medicine_detail_view" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class Medicine_detail_view : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
      /*  if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");

        } else*/
        {
            Method_Fish method = new Method_Fish();
            DataTable re_ = method.Medicine_detail_view();
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