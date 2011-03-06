<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="AddChits.aspx.cs" Inherits="AddChits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header">
        <asp:Label ID="ActionTitle" runat="server">Add ATKT</asp:Label></div>
    <table width="100%">
        <tr>
            <td>
                Chit No</td>
            <td>
                <asp:TextBox ID="ChitNO" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Chit Name</td>
            <td>
                <asp:TextBox ID="ChitName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Chit Amount</td>
            <td>
                <asp:TextBox ID="ChitAmount" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Installment Amount</td>
            <td>
                <asp:TextBox ID="InstallmentAmount" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                No Installments</td>
            <td>
                <asp:TextBox ID="NoInstallments" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="SaveButton" runat="server" OnClick="Save_Click" Text="Save"></asp:Button>
                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CssClass="button" OnClick="Delete_Click"
                    OnClientClick="javascript:return confirm('Are you sure to Delete?');" Visible="false" />
                <input type="button" value="Close" onclick="window.location.href='Chits.aspx';" />
                <input type="hidden" id="mode" runat="server" />
                <input type="hidden" id="RecordID" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="nav_header" colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
