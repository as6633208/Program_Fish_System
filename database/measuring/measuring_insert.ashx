<%@ WebHandler Language="C#" Class="Measuring_insert" %>

using System;
using System.Web;

public class Measuring_insert : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        //魚池資料
        string Pool_id = "A1";
        int Fish_detail_id = 1;
        //使用者輸入資料
        int number = 1050;
        string Fish_AVGweight = "14";
        string date = DateTime.Now.ToShortDateString();
        Method_Fish method = new Method_Fish();
        string re_ = method.Measuring_insert(Pool_id,Fish_detail_id,number,Fish_AVGweight,date);
        context.Response.Write(re_);//查看有無資料

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}