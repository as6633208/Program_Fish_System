<%@ WebHandler Language="C#" Class="medicine_select" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class medicine_select : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");

        } else {
            string a = context.Request.Form["Fish_company_id"].ToString();
            Method_Fish method = new Method_Fish();
            DataTable re_ = method.MedicinePurchaseName_View(a);
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