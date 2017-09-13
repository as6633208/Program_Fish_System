<%@ WebHandler Language="C#" Class="Fish_detail_view"%>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Fish_detail_view : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");
        }
        else
        {
            string Fish_detail_id = context.Request.Form["Fish_detail_id"].ToString();
            Method_Fish method = new Method_Fish();
            try
            {
                DataTable re_ = method.Fish_detail_view(Fish_detail_id);
                string str_json = JsonConvert.SerializeObject(re_, Formatting.Indented);
                context.Response.Write(str_json);//回傳資料
            }
            catch (Exception e)
            {
                context.Response.Write("null");//回傳資料
            }
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}