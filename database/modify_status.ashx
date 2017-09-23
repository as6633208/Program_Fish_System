<%@ WebHandler Language="C#" Class="modify_ststus" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class modify_ststus : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        Method_Fish method = new Method_Fish();
        string re_ = method.modify_ststus();
        context.Response.Write(re_);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}