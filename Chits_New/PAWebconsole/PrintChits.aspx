<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="PrintChits.aspx.cs" Inherits="PrintChits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script language="Javascript">
     function openReport(type)
       {
            if(type == 1)
            {
                var fromDate = document.getElementById('<%= FromDate.ClientID %>').value;
                var toDate = document.getElementById('<%= ToDate.ClientID %>').value;
                var closed = document.getElementById('<%= ClosedType.ClientID %>').value;
                if(fromDate == "" || toDate == "")
                {
                    alert("Please select the period");
                }
                else
                {             
                    var win = window.open('PrintChitsReport.aspx?fromdate=' + fromDate + '&todate=' + toDate + '&closed=' + closed, 'RepoWind', 'top=100,left=250,height=600,width=600,status=yes,resizable=yes');
                    win.focus();
                }
           }
           else if(type == 2)
           {
                var win = window.open('PrintChitMonthlyStmt.aspx', 'RepoWind', 'top=100,left=250,height=600,width=600,status=yes,resizable=yes');
                win.focus();
           }
           else if(type == 3)
           {
                var win = window.open('PrintCompanyBidding.aspx', 'RepoWind', 'top=100,left=250,height=600,width=600,status=yes,resizable=yes');
                win.focus();
           }
       }
       
       
    </script>

    <div id="content" class="nav_header" runat="server">
        Print Chits</div>
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
            <td width="20%">
                <input type="button" value="Print" onclick="javascript:openReport(1);" />
                <input type="button" value="Close" onclick="window.location.href='Chits.aspx';" />
            </td>
        </tr>
        <tr>
            <td colspan="6"><a href="javascript:void(0);" onclick= "javascript:openReport(2);">Monthly Due Statement</a></td>
        </tr>
        <tr>
            <td colspan="6"><a href="javascript:void(0);" onclick= "javascript:openReport(3);">Company Bidding Statement</a></td>
        </tr>
        <tr class="nav_header">
            <td colspan="6">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
