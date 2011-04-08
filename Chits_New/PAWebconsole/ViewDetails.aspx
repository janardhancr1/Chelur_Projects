<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ViewDetails.aspx.cs" Inherits="ViewDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script language="Javascript">
        function checkDate()
        {
            var fromDate = new Date(document.getElementById("<%= FromDate.ClientID %>").value);
            var toDate = new Date(document.getElementById("<%= ToDate.ClientID %>").value);
            if(toDate < fromDate)
            {
                alert("Please check the date period");
                return false;
            }
            return true;
        }
        
        function openReport(type)
        {
            var fromDate = document.getElementById('<%= FromDate.ClientID %>').value;
            var toDate = document.getElementById('<%= ToDate.ClientID %>').value;
            var ledgerName = document.getElementById('<% =LedgerName.ClientID  %>').innerText;
            var type = document.getElementById('<%= LedgerType.ClientID %>').value;
            var ledgerid = document.getElementById('<%= LedgerID.ClientID %>').value;
           
            var win = window.open('PrintLedgerDetails.aspx?fromdate=' + fromDate + '&todate=' + toDate + '&ledger=' + ledgerName + '&ledgerID=' + ledgerid + '&type=' + type, 'RepoWind', 'top=100,left=250,height=600,width=600,status=yes,resizable=yes');
            win.focus();
        }
    </script>
<div id="content" class="nav_header">
        View Ledger Details  -
        <asp:Label ID="LedgerName" runat="server"></asp:Label></div>
    <table width="100%">
        <tr>
            <td width="15%">
                Select the Period From
            </td>
            <td width="20%">
                <input type="text" id="FromDate" runat="server" onfocus="showCalendarControl(this);"
                    readonly /></td>
            <td width="5%" align="center">
                To
            </td>
            <td width="20%">
                <input type="text" id="ToDate" runat="server" onfocus="showCalendarControl(this);"
                    readonly /></td>
            <td width="40%">
                <asp:Button ID="ViewButton" Text="View" runat="server" OnClick="View_Click" />
                <input type="button" value="Print" onclick="javascript:openReport();" />
                <input id="CloseButton" type="button" value="Close" runat="server" />
                <input type="hidden" id="LedgerID" runat="server" value="0"/>
                <input type="hidden" id="LedgerType" runat="server" />
                <input type="hidden" id="GroupName" runat="server" />
            </td>
        </tr>
    </table>
    <div>
    <table width="97%">
        <tr>
            <td width="60%" align="center"><b>Opening Balance</b></td>
            <td width="10%" align="right"><asp:Label ID="OpenDebit" runat="Server"></asp:Label></td>
            <td width="10%" align="right"><asp:Label ID="OpenCredit" runat="Server"></asp:Label></td>
            <td width="20%">&nbsp;</td>
        </tr>
    </table>
    </div>
    <div>
        <table width="97%">
            <tr class="nav_header">
                <td width="5%">
                    SlNo.</td>
                <td width="10%">
                    Date</td>
                <td width="25%">
                    Particulars</td>
                <td width="10%">
                    VoucherType</td>
                <td width="10%">
                    VoucherNo</td>
                <td width="10%">
                    Debit</td>
                <td width="10%">
                    Credit</td>
                <td width="20%">
                    Naration</td>
            </tr>
        </table>
    </div>
    <div style="height: 300px; overflow-y: auto">
        <table width="97%" id="DetailsTable" runat="Server">
        </table>
    </div>
    <div>
        <table width="97%">
            <tr class="nav_header">
                <td width="5%">
                    &nbsp;</td>
                <td width="10%">
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
                <td width="10%">
                    &nbsp;</td>
                <td width="10%">
                    &nbsp;</td>
                <td width="10%">
                    &nbsp;</td>
                <td width="10%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div>
    <table width="97%">
        <tr>
            <td width="60%" align="center"><b>Current Period Total</b></td>
            <td width="10%" align="right"><asp:Label ID="PeriodDebit" runat="Server"></asp:Label></td>
            <td width="10%" align="right"><asp:Label ID="PeriodCredit" runat="Server"></asp:Label></td>
            <td width="20%">&nbsp;</td>
        </tr>
    </table>
    </div>
    <div>
    <table width="97%">
        <tr>
            <td width="60%" align="center"><b>Closing Balance</b></td>
            <td width="10%" align="right"><asp:Label ID="CloseDebit" runat="Server"></asp:Label></td>
            <td width="10%" align="right"><asp:Label ID="CloseCredit" runat="Server"></asp:Label></td>
            <td width="20%">&nbsp;</td>
        </tr>
    </table>
    </div>
</asp:Content>
