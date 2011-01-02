<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="DayBook.aspx.cs" Inherits="DayBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Day Book</div>
    <table width="100%">
        <tr>
            <td width="15%">
                Select the Day
            </td>
            <td width="20%">
                <input type="text" id="DayDate" runat="server" onfocus="showCalendarControl(this);"
                    readonly /></td>
            <td width="65%">
                <asp:Button ID="ViewButton" Text="View" runat="server" OnClick="View_Click" />
                <input id="Button1" type="button" value="Close" runat="server" onclick="window.location.href='HomePage.aspx';" /></td>
        </tr>
    </table>
    <div>
        <table width="98%">
            <tr class="nav_header">
                <td width="5%">
                    SlNo.</td>
                <td width="10%">
                    Date</td>
                <td width="15%">
                    From Ledger</td>
                <td width="15%">
                    To Ledger</td>
                <td width="10%">
                    VoucherType</td>
                <td width="10%">
                    VoucherNo</td>
                <td width="10%">
                    Debit</td>
                <td width="10%">
                    Credit</td>
                <td width="15%">
                    Naration</td>
            </tr>
        </table>
    </div>
    <div style="height: 350px; overflow-y: auto; overflow-x: hidden">
        <table width="98%" id="DayTable" runat="Server">
        </table>
    </div>
</asp:Content>
