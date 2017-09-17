<%@ WebHandler Language="C#" Class="Medicine_number_modify" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Medicine_number_modify : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        /*if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");

        } else */
        {
            string a = context.Request.Form["MedicineID"].ToString();
            string b = context.Request.Form["Medicine_number"].ToString();
            Method_Fish method = new Method_Fish();
            string re_ = method.Medicine_number_modify(a, b);
            context.Response.Write(re_);
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}