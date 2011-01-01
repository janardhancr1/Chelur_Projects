<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="AddATKT.aspx.cs" Inherits="AddATKT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header">
        <asp:Label ID="ActionTitle" runat="server">Add ATKT</asp:Label></div>
    <table width="100%">
        <tr>
            <td width="25%">
                ATKTNO</td>
            <td width="25%">
                <asp:TextBox ID="ATKTNO" runat="server" MaxLength="7"></asp:TextBox></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                PartyName</td>
            <td>
                <asp:TextBox ID="PartyName" runat="server" MaxLength="50"></asp:TextBox></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                ATKTDate</td>
            <td>
                <input type="text" id="ATKTDate" runat="server" onfocus="showCalendarControl(this);"
                    readonly />
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                TranType</td>
            <td>
                <asp:DropDownList ID="TranType" runat="server" Width="153px">
                </asp:DropDownList></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Amount</td>
            <td>
                <asp:TextBox ID="Amount" runat="server" MaxLength="10"></asp:TextBox></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Remarks</td>
            <td>
                <asp:TextBox ID="Remarks" runat="server" MaxLength="255"></asp:TextBox></td>
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
                <asp:Button ID="SearchButton" runat="server" OnClick="Save_Click" Text="Save"></asp:Button>
                <input type="button" value="Close" onclick="window.location.href='ATKT.aspx';" /></td>
            <td colspan="2">
                <input type="hidden" id="mode" runat="server" />
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
