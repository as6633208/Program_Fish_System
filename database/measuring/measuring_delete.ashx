<%@ WebHandler Language="C#" Class="measuring_delete" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class measuring_delete : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
            if (context.Request.ServerVariables["HTTP_REFERER"] == null)
        {
            context.Response.Write("Error");
        } else
        {
            string Pool_id = context.Request.Form["Pool_id"].ToString();
            string Fish_detail_id=context.Request.Form["Fish_detail_id"].ToString();
            string id=context.Request.Form["id"].ToString();
            Method_Fish method = new Method_Fish();
            string re_ = method.Measuring_delete(Pool_id,Fish_detail_id,id);
        
            context.Response.Write(re_);//回傳資料
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}