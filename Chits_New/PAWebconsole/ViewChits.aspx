<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ViewChits.aspx.cs" Inherits="ViewChits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header">
        <asp:Label ID="ActionTitle" runat="server">View Chits</asp:Label></div>
    <table width="100%">
        <tr>
            <td width="25%">
                Chit No</td>
            <td width="25%">
                <b>
                    <asp:Label ID="ChitNO" runat="server"></asp:Label></b></td>
            <td width="50%">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Chit Name</td>
            <td>
                <b>
                    <asp:Label ID="ChitName" runat="server"></asp:Label></b></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Chit Amount</td>
            <td>
                <b>
                    <asp:Label ID="ChitAmount" runat="server"></asp:Label></b></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Installment Amount</td>
            <td>
                <b>
                    <asp:Label ID="InstallmentAmount" runat="server"></asp:Label></b></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                No Installments</td>
            <td>
                <b>
                    <asp:Label ID="NoInstallments" runat="server"></asp:Label></b></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <a href="#" id="Link1" runat="server" class="acolor">View Details</a>&nbsp;&nbsp;&nbsp;
                <a href="#" id="Link2" runat="server" class="acolor">View Bidding</a>&nbsp;&nbsp;&nbsp;
                <a href="#" id="Link3" runat="server" class="acolor">View Company Bidding</a>&nbsp;&nbsp;&nbsp;
                <a href="#" id="Link4" runat="server" class="acolor">View Members</a>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:Button ID="CloseButton" runat="server" Text="Close this Chit" OnClick="Close_Click" 
                    OnClientClick="javascript:return confirm('Are you sure to Close?');" />
                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CssClass="button" OnClick="Delete_Click"
                    OnClientClick="javascript:return confirm('Are you sure to Delete?');" />
                <asp:Button ID="PrintButton" runat="server" Text="Print" CssClass="button" OnClick="Print_Click" />
                <input type="button" value="Close" onclick="window.location.href='Chits.aspx';" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table width="100%" id="DetailsTable" runat="server">
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="nav_header" colspan="3">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
