<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="TrialBalance.aspx.cs" Inherits="TrialBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Trial Balance</div>
    <table width="100%">
        <tr>
            <td width="5%">
                View</td>
            <td width="10%">
                <asp:DropDownList ID="ViewType" runat="server" OnSelectedIndexChanged="ViewType_Selected"
                    AutoPostBack="true">
                </asp:DropDownList></td>
            <td id="FinancialCell" runat="server" width="50%" align="center">
                Financial Year : <b>
                    <asp:Label ID="FinYearFrom" runat="server"></asp:Label></b> To <b>
                        <asp:Label ID="FinYearTo" runat="server"></asp:Label></b></td>
            <td id="UptoDateCell" runat="server" visible="false" width="50%" align="center">
                From : <b>
                    <asp:Label ID="UptoYearFrom" runat="server"></asp:Label></b> To <b>
                        <input type="text" id="UptoYearTo" runat="server" onfocus="showCalendarControl(this);"
                            readonly /></b>
            </td>
            <td width="35%">
                <asp:Button ID="ViewButton" Text="View" runat="server" OnClick="View_Click" />
                <asp:Button ID="DetailsButton" Text="Details" runat="server" OnClick="Details_Click" />
                <input type="button" value="Print" onclick="javascript:openReport();" />
                <input type="hidden" id="GroupSelected" runat="server" value="0" />
                <input type="hidden" id="ToDate" runat="server" />
                <input type="hidden" id="FromDate" runat="server" /></td>
        </tr>
    </table>
    <hr />
    <br />
    <div style="height: 350px; overflow-y: auto; overflow-x:hidden">
        <table width="98%" id="DetailsTable" runat="server">
            <tr class="nav_header">
                <td>
                    Particulars</td>
                <td align="right">
                    Debit</td>
                <td align="right">
                    Credit</td>
            </tr>
        </table>
    </div>
</asp:Content>
