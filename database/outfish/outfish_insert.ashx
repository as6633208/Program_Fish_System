<%@ WebHandler Language="C#" Class="outfish_insert" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class outfish_insert : IHttpHandler {

    public void ProcessRequest (HttpContext context) {


        string Pool_id = context.Request.Form["Pool_id"].ToString();
        string Out_number = context.Request.Form["Out_number"].ToString();
        string Fish_AVGweight = context.Request.Form["Fish_AVGweight"].ToString();
        string Waistline = context.Request.Form["Waistline"].ToString();
        string bust = context.Request.Form["bust"].ToString();
        string Tail = context.Request.Form["Tail"].ToString();
        string Fish_detail_id = context.Request.Form["Fish_detail_id"].ToString();
        string Outside_date =context.Request.Form["Outside_date"].ToString();
        string old_number=context.Request.Form["old_number"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.Outfish_Insert(Pool_id,Out_number,Fish_AVGweight,Waistline,bust,Tail,Fish_detail_id,Outside_date);
        int cut = Int32.Parse(old_number) - Int32.Parse(Out_number);
        //更新池內數量
        method.Pool_status(Pool_id, (Int32.Parse(old_number) - Int32.Parse(Out_number)).ToString(), Int32.Parse(Fish_detail_id));
        //更新細節數量
        if (method.Fish_detail_update(Int32.Parse(Fish_detail_id), cut) != ("success")) {
            re_ = "fail";
        }
        context.Response.Write(re_);//回傳資料

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}