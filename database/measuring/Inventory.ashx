<%@ WebHandler Language="C#" Class="Inventory" %>

using System;
using System.Web;

public class Inventory : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string pool_id = context.Request.Form["Pool_id"].ToString();
        string fish_detail_id = context.Request.Form["Fish_detail_id"].ToString();
        string number = context.Request.Form["number"].ToString();
        string date = DateTime.Now.ToShortDateString();
        Method_Fish method = new Method_Fish();
        //string re_ = method.Inventory(pool_id, Int32.Parse(fish_detail_id), 0, Int32.Parse(number), date);
        context.Response.Write(re_);   
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}