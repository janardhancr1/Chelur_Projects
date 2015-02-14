<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintATKTReport.aspx.cs"
    Inherits="PrintATKTReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ATKT Report</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                Height="600px" Width="100%">
                <LocalReport ReportPath="Reports\ATKTReport.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="ATKTInfo" />
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="LedgerDate" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetLedgerDate"
                TypeName="PALibrary.Library.Component.ReportManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="" Name="fromDate" Type="DateTime" />
                    <asp:Parameter DefaultValue="" Name="toDate" Type="DateTime" />
                    <asp:Parameter DefaultValue="" Name="ledgerName" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetATKTInfos"
                TypeName="PALibrary.Library.Component.ATKTManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="" Name="fromDate" Type="DateTime" />
                    <asp:Parameter DefaultValue="" Name="toDate" Type="DateTime" />
                    <asp:Parameter DefaultValue="" Name="closed" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <input type="hidden" id="FromDate" runat="server" />
        <input type="hidden" id="ToDate" runat="server" />
        <input type="hidden" id="ClosedType" runat="server" />
    </form>
</body>
</html>
