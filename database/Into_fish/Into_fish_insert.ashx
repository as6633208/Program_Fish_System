<%@ WebHandler Language="C#" Class="Into_fish_insert" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Into_fish_insert : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string fish_classification =  context.Request.Form["fish_classification"].ToString();
        string supplier_classification =  context.Request.Form["supplier_classification"].ToString();
        string into_time1 =  context.Request.Form["into_time1"].ToString();
        string into_time2 =  context.Request.Form["into_time2"].ToString();
        string fry_weight =  context.Request.Form["fry_weight"].ToString();
        string start_number =  context.Request.Form["start_number"].ToString();
        Method_Fish method = new Method_Fish();
        int re_ = method.Into_Fish_Insert(fish_classification,supplier_classification,into_time1,into_time2,fry_weight,start_number);
        string str_json = JsonConvert.SerializeObject(re_, Formatting.Indented);
        context.Response.Write(str_json);//查看有無資料

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}