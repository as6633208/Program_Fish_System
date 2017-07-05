<%@ WebHandler Language="C#" Class="supplier_search" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class supplier_search : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");

        } else {
            string Fish_company_id = context.Request.Form["Fish_company_id"].ToString();
            Method_Fish method = new Method_Fish();
            DataTable re_ = method.Supplier_Search(Fish_company_id);
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