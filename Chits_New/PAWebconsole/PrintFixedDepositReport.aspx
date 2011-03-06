<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintFixedDepositReport.aspx.cs"
    Inherits="PrintFixedDepositReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fixed Deposit Report</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                Height="600px" Width="100%">
                <LocalReport ReportPath="Reports\FDReport.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="FixedDepositInfo" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetFixedDepositInfos"
                TypeName="PALibrary.Library.Component.FixedDepositManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="" Name="fromDate" Type="DateTime" />
                    <asp:Parameter DefaultValue="" Name="toDate" Type="DateTime" />
                    <asp:Parameter DefaultValue="" Name="closed" Type="String" />
                    <asp:Parameter DefaultValue="" Name="orderBy" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <input type="hidden" id="OrderBY" runat="server" />
        <input type="hidden" id="FromDate" runat="server" />
        <input type="hidden" id="ToDate" runat="server" />
        <input type="hidden" id="ClosedType" runat="server" />
    </form>
</body>
</html>