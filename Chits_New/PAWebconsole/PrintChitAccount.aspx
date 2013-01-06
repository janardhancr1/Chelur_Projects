<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintChitAccount.aspx.cs"
    Inherits="PrintChitAccount" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chit Customer Account</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                Height="600px" Width="100%">
                <LocalReport ReportPath="Reports\CustomerAccount.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="ChitsReportInfo"></rsweb:ReportDataSource>
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetChitCustomerAccount"
                TypeName="PALibrary.Library.Component.ChitsReportManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="" Name="chitNo" Type="String" />
                    <asp:Parameter DefaultValue="" Name="customerID" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <input type="hidden" id="ChitNo" runat="server" />
        <input type="hidden" id="CustomerID" runat="server" />
    </form>
</body>
</html>
