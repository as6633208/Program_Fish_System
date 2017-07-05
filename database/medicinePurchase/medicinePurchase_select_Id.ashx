<%@ WebHandler Language="C#" Class="medicinePurchase_select_Id" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class medicinePurchase_select_Id : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string supplier =  context.Request.Form["supplier"].ToString();
        string feed =  context.Request.Form["medicine"].ToString();
        Method_Fish method = new Method_Fish();
        DataTable re_ = method.MedicinePurchase_catch_id(supplier,feed);
        string str_json = JsonConvert.SerializeObject(re_, Formatting.Indented);
        context.Response.Write(str_json);//回傳資料
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}