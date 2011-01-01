<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ViewFixedDeposit.aspx.cs" Inherits="ViewFixedDeposit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        View Fixed Deposit</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td width="25%">
                FD No</td>
            <td width="25%">
                <b>
                    <asp:Label ID="FDNo" runat="server"></asp:Label></b>
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
                    <asp:Label ID="FDDate" runat="server"></asp:Label></b>
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
                Address: <b>
                    <asp:Label ID="Address" runat="Server"></asp:Label></b></td>
        </tr>
        <tr>
            <td>
                Nominee Name</td>
            <td>
                <b>
                    <asp:Label ID="NomineeName" runat="server"></asp:Label></b>
            </td>
            <td colspan="2">
                Nominee Relationship: <b>
                    <asp:Label ID="NomineeRelation" runat="server"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td>
                Loan Amount
            </td>
            <td>
                <b>
                    <asp:Label ID="FDAmount" runat="server"></asp:Label></b></td>
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
                <input type="button" value="Close" onclick="window.location.href='FixedDesposits.aspx';" />
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
