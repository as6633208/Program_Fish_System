<%@ WebHandler Language="C#" Class="MedicineSelectSum" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MedicineSelectSum : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        /*if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");

        } else */
        {
            string a = context.Request.Form["Medicine_id"].ToString();
            Method_Fish method = new Method_Fish();
            DataTable re_ = method.MedicineSelectSum(a);
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