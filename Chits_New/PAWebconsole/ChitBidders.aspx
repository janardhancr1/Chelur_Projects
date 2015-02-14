<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ChitBidders.aspx.cs" Inherits="ChitBidders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="Javascript">
     function openReport()
       {
            var chitNo = document.getElementById('<%= ChitNO.ClientID %>').value;
            var printType = document.getElementById('<%= PrintType.ClientID %>').value;
            var chitName = document.getElementById('<%= ChitName.ClientID %>').value;
            var win = window.open('PrintChitBidders.aspx?chitno=' + chitNo + '&type=' + printType + '&chitName=' + chitName, 'RepoWind', 'top=100,left=250,height=600,width=600,status=yes,resizable=yes');
            win.focus();
       }
    </script>
    <div id="content" class="nav_header" runat="server">
        Chit Bidders</div>
    <table width="100%">
        <tr>
            <td>
                Chit No</td>
            <td>
                <input type="text" id="ChitNO" runat="server" readonly /></td>
        </tr>
        <tr>
            <td>
                Chit Name</td>
            <td>
                <input type="text" id="ChitName" runat="server" readonly />
            </td>
        </tr>
        <tr>
            <td>
                Chit Amount</td>
            <td>
                <input type="text" id="ChitAmount" runat="server" readonly /></td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%" id="MembersTable" runat="server">
                    <tr class="nav_header">
                        <th align="left">CustomerName</th>
                        <th align="left">CustomerAddress</th>
                        <th align="left">Installment No</th>
                        <th align="left">Bid Date</th>
                        <th align="left">Paid Date</th>
                        <th align="left">Bid Amount</th>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <input type="button" value="Print" onclick="javascript:openReport();" />
                <input type="button" value="Close" id="CloseButton" runat="Server" />
                <input type="hidden" id="PrintType" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="nav_header">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
