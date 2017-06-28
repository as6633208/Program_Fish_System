<%@ WebHandler Language="C#" Class="fish_kind_modify" %>

using System;
using System.Web;

public class fish_kind_modify : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string a = context.Request.Form["fish_kind_name"].ToString();            
        string b = context.Request.Form["modify_id"].ToString();
        Method_Fish method = new Method_Fish();
        string re_ = method.Fish_Kind_Modify(a,b);
        context.Response.Write(re_);//查看有無資料
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}