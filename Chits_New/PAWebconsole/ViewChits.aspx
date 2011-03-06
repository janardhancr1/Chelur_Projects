<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ViewChits.aspx.cs" Inherits="ViewChits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header">
        <asp:Label ID="ActionTitle" runat="server">View ATKT</asp:Label></div>
    <table width="100%">
        <tr>
            <td>
                Chit No</td>
            <td>
                <asp:TextBox ID="ChitNO" runat="server" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Chit Name</td>
            <td>
                <asp:TextBox ID="ChitName" runat="server" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Chit Amount</td>
            <td>
                <asp:TextBox ID="ChitAmount" runat="server" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Installment Amount</td>
            <td>
                <asp:TextBox ID="InstallmentAmount" runat="server" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                No Installments</td>
            <td>
                <asp:TextBox ID="NoInstallments" runat="server" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <input type="button" value="Close" onclick="window.location.href='Chits.aspx';" />
            </td>
        </tr>
        <tr>
            <td class="nav_header" colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
