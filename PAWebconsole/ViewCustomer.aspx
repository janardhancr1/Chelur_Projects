<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ViewCustomer.aspx.cs" Inherits="ViewCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        View Customer</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td>
                Customer Name</td>
            <td>
                <b>
                    <asp:Label ID="CustomerName" runat="server"></asp:Label></b>
        </tr>
        <tr>
            <td>
                Father/Husband Name</td>
            <td>
                <b>
                    <asp:Label ID="FatherHusband" runat="server"></asp:Label></b>
        </tr>
        <tr>
            <td>
                Account No</td>
            <td>
                <b>
                    <asp:Label ID="AccountNo" runat="server"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td>
                Res Address</td>
            <td>
                <b>
                    <asp:Label ID="ResAddress" runat="server"></asp:Label></b>
        </tr>
        <tr>
            <td>
                Res City/Pin</td>
            <td>
                <b>
                    <asp:Label ID="ResCitypin" runat="server"></asp:Label></b>
        </tr>
        <tr>
            <td>
                Res Phone</td>
            <td>
                <b>
                    <asp:Label ID="ResPhone" runat="server"></asp:Label></b></td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%" id="LoanTable" runat="server">
                    <tr class="nav_header">
                        <td>
                            Loan Type</td>
                        <td>
                            Loan No</td>
                        <td>
                            Date</td>
                        <td>
                            Amount</td>
                        <td>
                            Status</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="DeleteButton" Text="Delete" runat="server" OnClick="Delete_Click" />
                <input type="button" value="Close" onclick="window.location.href='Customer.aspx';" />
            </td>
            <td>
                <input type="hidden" id="CustomerID" runat="server" /></td>
        </tr>
        <tr>
            <td class="nav_header" colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
