<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="Ledgers.aspx.cs" Inherits="Ledgers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        View Ledgers</div>
    <br />
    <table width="100%">
        <tr>
            <td width="10%">
                &nbsp;</td>
            <td width="90%">
                <a href="MonthlyLedger.aspx?type=1" class="acolor"><b>Hundi Loan Ledger</b></a></td>
        </tr>
        <tr>
            <td width="10%">
                &nbsp;</td>
            <td width="90%">
                <a href="MonthlyLedger.aspx?type=2" class="acolor"><b>Fixed Deposit Ledger</b></a></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <a href="MonthlyLedger.aspx?type=3" class="acolor"><b>ATKT Ledger</b></a></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <a href="MonthlyLedger.aspx?type=4" class="acolor"><b>Chits Ledger</b></a></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <a href="MonthlyLedger.aspx?type=9" class="acolor"><b>Company Bidding Ledger</b></a></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <a href="MonthlyLedger.aspx?type=7" class="acolor"><b>Chits Commission Ledger</b></a></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <a href="MonthlyLedger.aspx?type=8" class="acolor"><b>Chits Discount Ledger</b></a></td>
        </tr>
         <tr>
            <td width="10%">
                &nbsp;</td>
            <td width="90%">
                <a href="MonthlyLedger.aspx?type=5" class="acolor"><b>Interest Collected Ledger</b></a></td>
        </tr>
        <tr>
            <td width="10%">
                &nbsp;</td>
            <td width="90%">
                <a href="MonthlyLedger.aspx?type=6" class="acolor"><b>Interest Paid Ledger</b></a></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <a href="ViewLedger.aspx" class="acolor"><b>Other Ledgers</b></a></td>
        </tr>
    </table>
</asp:Content>
