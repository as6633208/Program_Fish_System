<%@ WebHandler Language="C#" Class="fish_kind_insert" %>

using System;
using System.Web;

public class fish_kind_insert : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string a = context.Request.Form["fish_kind_name"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.Fish_Kind_Insert(a);
        context.Response.Write(re_);//查看有無資料
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}