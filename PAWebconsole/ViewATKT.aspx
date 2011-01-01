<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ViewATKT.aspx.cs" Inherits="ViewATKT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header">
        <asp:Label ID="ActionTitle" runat="server">View ATKT</asp:Label></div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td width="25%">
                ATKTNO</td>
            <td width="25%">
                <asp:TextBox ID="ATKTNO" runat="server" ReadOnly="true"></asp:TextBox></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                PartyName</td>
            <td>
                <asp:TextBox ID="PartyName" runat="server" ReadOnly="true"></asp:TextBox></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                ATKTDate</td>
            <td>
                <asp:TextBox ID="ATKTDate" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                TranType</td>
            <td>
                <asp:TextBox ID="TranType" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Amount</td>
            <td>
                <asp:TextBox ID="Amount" runat="server" ReadOnly="true"></asp:TextBox></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Remarks</td>
            <td>
                <asp:TextBox ID="Remarks" runat="server" ReadOnly="true"></asp:TextBox></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Closed</td>
            <td>
                <asp:TextBox ID="Closed" runat="server" ReadOnly="true"></asp:TextBox></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                ClosedDate</td>
            <td>
                <asp:TextBox ID="ClosedDate" runat="server" ReadOnly="true"></asp:TextBox></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="CloseButton" runat="server" Text="Receive" OnClick="Closed_Click" />
                <asp:Button ID="DeleteButton" Text="Delete" runat="server" OnClick="Delete_Click"
                    OnClientClick="javascript:return confirm('Are you sure to Delete?');" />
                <input type="button" value="Close" onclick="window.location.href='ATKT.aspx';" /></td>
            <td colspan="2">
                <input type="hidden" id="RecordID" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="nav_header" colspan="4">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
