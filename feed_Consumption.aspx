<%@ Page Language="C#" AutoEventWireup="true" CodeFile="feed_Consumption.aspx.cs" Inherits="feed_Consumption" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.js"></script>
        <script>
            $("#document").ready(function () {
                var result_time = ""
                var result_supplier = ""
                var result_feed = ""
                var refDoc = $(window.opener.document)
                var vaule = $("#startDate", refDoc).val()
                var supplier = $("#supplier_classification", refDoc).val()
                var feed = $("#feed_classification", refDoc).val()
                if (vaule != null || typeof vaule != "undefined") {
                    result = vaule
                }
                //$("#xxx").val(result)
                //alert(result_time )
            });
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="698px" Width="1376px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="Feed_consumption.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="Consumption" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="Consumption" runat="server" SelectMethod="Get" TypeName="Consumption">
            <SelectParameters>
                <asp:CookieParameter CookieName="Feed_consumption" Name="str1" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    </div>
    </form>
</body>
</html>
