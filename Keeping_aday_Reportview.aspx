<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Keeping_aday_Reportview.aspx.cs" Inherits="Keeping_aday_Reportview" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.js"></script>
    <script>
        $("#document").ready(function () {
            var result = ""
            var refDoc = $(window.opener.document)
            var vaule = $("#startDate", refDoc).val()//startDate
            if (vaule != null || typeof vaule != "undefined") {
                result = vaule
            }
            
            $("#xxx").val(result)
            //alert(result)
        });
    </script>
   

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

      

        <rsweb:ReportViewer ID="Keeping_aday_Viewer" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="698px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1376px">
            <LocalReport ReportPath="Feed_Reportview.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="Feeds_Aday" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="Feeds_Aday" runat="server" SelectMethod="Get" TypeName="Feed_Reportview">
            <SelectParameters>
                <asp:CookieParameter CookieName="Feed_aday_time" Name="str1" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>

</body>
</html>