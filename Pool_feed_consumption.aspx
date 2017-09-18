<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pool_feed_consumption.aspx.cs" Inherits="Pool_feed_consumption" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>恆春海洋養殖股份有限公司</title>
    <meta name="keywords" content="海洋,環保,自然,恆春養殖,恆春海洋養殖,屏東海洋養殖漁業,海金鯧,海鱺"/>
    <meta name="description" content="恆春海洋養殖股份有限公司主要是從事高科技現代化的海上箱網養殖漁業，以生產各式高經濟價值魚類為主。目前漁場設有耐波浪及抗流速的外海可沉式箱網，以高效率、高效能的方式來生產高品質的水產品。海金鯧與海鱺為目前本漁場主要的養殖魚種。網站：http://www.seafarm.com.tw/漁場位址：屏東縣車城鄉海口村外 海服務專線：0800-001300 "/>
    <meta name="copyright" content="Renwu"/>
    <meta name="author" content="恆春海洋養殖股份有限公司"/>
    <!-- Icon -->
    <link rel="icon" href="image/favcon.ico" type="image/x-icon"/>
    <link rel="shortcut icon" href="image/favcon.ico"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="519px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="883px">
            <LocalReport ReportPath="Pool_feed_consumption.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="Pool_feed" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="Pool_feed" runat="server" SelectMethod="Get" TypeName="Pool_feed_consumption">
            <SelectParameters>
                <asp:CookieParameter CookieName="pool_feed" Name="str1" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    </div>
    </form>
</body>
</html>
