<%@ WebHandler Language="C#" Class="Into_fish_detail" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Into_fish_detail : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string Fish_id =  context.Request.Form["Fish_id"].ToString();
        string number =  context.Request.Form["number"].ToString();
        string Pool_id =  context.Request.Form["Pool_id"].ToString();
        string into_time2 =  context.Request.Form["into_time2"].ToString();
        string Fish_AVGweight =  context.Request.Form["Fish_AVGweight"].ToString();

        Method_Fish method = new Method_Fish();
        string re_ = method.Into_Fish_Detail(Fish_id, number,Pool_id,into_time2,Fish_AVGweight);
        context.Response.Write(re_);//查看有無資料
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}