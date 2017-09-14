<%@ WebHandler Language="C#" Class="feed_select" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class feed_select : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        /*if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");

        } else */
        {
            string a = context.Request.Form["FodderID"].ToString();
            string b = context.Request.Form["Fodder_number"].ToString();
            Method_Fish method = new Method_Fish();
            string re_ = method.Fodder_number_modify(a, b);
            context.Response.Write(re_);
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}