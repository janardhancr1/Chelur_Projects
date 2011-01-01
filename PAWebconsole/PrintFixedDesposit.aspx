<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="PrintFixedDesposit.aspx.cs" Inherits="PrintFixedDesposit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="Javascript">
     function openReport(type)
       {
            var fromDate = document.getElementById('<%= FromDate.ClientID %>').value;
            var toDate = document.getElementById('<%= ToDate.ClientID %>').value;
            var closed = document.getElementById('<%= ClosedType.ClientID %>').value;
            var sort = document.getElementById('<%= OrderBy.ClientID %>').value;
            if(fromDate == "" || toDate == "")
            {
                alert("Please select the period");
            }
            else
            {             
                var win = window.open('PrintFixedDepositReport.aspx?fromdate=' + fromDate + '&todate=' + toDate + '&closed=' + closed + '&sort=' + sort, 'RepoWind', 'top=100,left=250,height=600,width=600,status=yes,resizable=yes');
                win.focus();
            }
       }
    </script>

    <div id="content" class="nav_header" runat="server">
        Print Fixed Deposit</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td width="20%">
                Select the Period From
            </td>
            <td width="15%">
                <input type="text" id="FromDate" runat="server" onfocus="showCalendarControl(this);"
                    readonly /></td>
            <td width="5%" align="center">
                To
            </td>
            <td width="15%">
                <input type="text" id="ToDate" runat="server" onfocus="showCalendarControl(this);"
                    readonly /></td>
            <td width="10%">
                <asp:DropDownList ID="ClosedType" runat="server">
                </asp:DropDownList>
            </td>
            <td width="10%">
                <asp:DropDownList ID="OrderBy" runat="server">
                </asp:DropDownList>
            </td>
            <td width="20%">
                <input type="button" value="Print" onclick="javascript:openReport();" />
                <input type="button" value="Close" onclick="window.location.href='FixedDesposits.aspx';" />
            </td>
        </tr>
        <tr class="nav_header">
            <td colspan="7">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
