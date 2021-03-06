<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCompanyBidding.aspx.cs"
    Inherits="PrintCompanyBidding" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Company Bidding Details</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                Height="600px" Width="100%">
                <LocalReport ReportPath="Reports\ChitCompanyBidding.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="ChitsBiddingInfo" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetChitsCompanyBiddingInfos"
                TypeName="PALibrary.Library.Component.ChitsBiddingManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="" Name="fromDate" Type="DateTime" />
                    <asp:Parameter DefaultValue="" Name="toDate" Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <input type="hidden" id="FromDate" runat="server" />
        <input type="hidden" id="ToDate" runat="server" />
    </form>
</body>
</html>
