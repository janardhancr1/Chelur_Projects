<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ViewHundiLoan.aspx.cs" Inherits="ViewHundiLoan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
       function openReport(type)
       {
            var recordID = document.getElementById('ctl00_ContentPlaceHolder1_RecordID').value;
            var dcloanno = document.getElementById('ctl00_ContentPlaceHolder1_HLLoanNo').innerText;
            var closed = document.getElementById('ctl00_ContentPlaceHolder1_ClosedType').value;
            var amt = document.getElementById('ctl00_ContentPlaceHolder1_LoanAmount').innerText
            
            var win = window.open('HundiLoanReport.aspx?recordid=' + recordID + '&dcloanno=' + dcloanno + '&closed=' + closed + '&amt=' + amt, 'RepoWind', 'top=100,left=250,height=600,width=600,status=yes,resizable=yes');
            win.focus();
       }
    </script>

    <div id="content" class="nav_header" runat="server">
        View Hundi Loan</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td width="25%">
                HL Loan No</td>
            <td width="25%">
                <b>
                    <asp:Label ID="HLLoanNo" runat="server"></asp:Label></b>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Date
            </td>
            <td>
                <b>
                    <asp:Label ID="LoanDate" runat="server"></asp:Label></b>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Customer Name</td>
            <td>
                <b>
                    <asp:Label ID="CustomerName" runat="server"></asp:Label></b>
            </td>
            <td colspan="2">
                Account No: <b>
                    <asp:Label ID="AccountNo" runat="Server"></asp:Label></b></td>
        </tr>
        <tr>
            <td>
                Father/Husband Name</td>
            <td>
                <b>
                    <asp:Label ID="FatherName" runat="server"></asp:Label></b>
            </td>
            <td colspan="2">
                Address <b>
                    <asp:Label ID="Address" runat="Server"></asp:Label></b></td>
        </tr>
        <tr>
            <td>
                CoObligent</td>
            <td>
                <b>
                    <asp:Label ID="CoObligent" runat="server"></asp:Label></b>
            </td>
            <td colspan="2">
                CoObligent Address : <b>
                    <asp:Label ID="CoObligentAddress" runat="server"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td>
                Loan Amount
            </td>
            <td>
                <b>
                    <asp:Label ID="LoanAmount" runat="server"></asp:Label></b></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Rate
            </td>
            <td>
                <b>
                    <asp:Label ID="Rate" runat="server"></asp:Label></b>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <a href="#" id="Link1" runat="server" class="acolor">View Details</a>&nbsp;&nbsp;&nbsp;
                <a href="#" id="Link2" runat="server" class="acolor">View Interest Details</a>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <asp:Label ID="message" runat="server" ForeColor="red"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <input type="button" value="Print" onclick="javascript:openReport();" />
                <asp:Button ID="DeleteButton" Text="Delete" runat="server" OnClick="Delete_Click"
                    OnClientClick="javascript:return confirm('Are you sure to Delete?');" />
                <input type="button" value="Close" onclick="window.location.href='HundiLoans.aspx';" />
            </td>
            <td colspan="2">
                <input type="hidden" id="ClosedType" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="nav_header">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
