<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="AddVouchers.aspx.cs" Inherits="AddVouchers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        <asp:Label ID="ActionTitle" runat="server">Add Voucher</asp:Label></div>
    <table width="100%" class="data_table">
        <tr>
            <td>
                Voucher Date</td>
            <td>
                <input type="text" id="VoucherDate" runat="server" readonly onfocus="showCalendarControl(this);" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="VoucherDate"
                    Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Voucher Type</td>
            <td>
                <asp:DropDownList ID="VoucherType" runat="server" Width="153px" AutoPostBack="true"
                    OnSelectedIndexChanged="VoucherType_Selected">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="VoucherType"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Voucher NO</td>
            <td>
                <asp:TextBox ID="VoucherNO" runat="server" MaxLength="10" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                From Ledger</td>
            <td>
                <asp:DropDownList ID="FromLedger" runat="server" Width="153px">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="FromLedger"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                To Ledger</td>
            <td>
                <asp:DropDownList ID="ToLedger" runat="server" Width="153px">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ToLedger"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Amount</td>
            <td>
                <asp:TextBox ID="Amount" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Amount"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Narration</td>
            <td>
                <asp:TextBox ID="Narration" runat="server" MaxLength="500"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph"
                    HeaderText="Fill all Required Fields" />
                <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <input type="button" id="NewButton" runat="server" value="Add New" visible="false"
                    onclick="window.location.href='AddVouchers.aspx';" />
                <asp:Button ID="SaveButton" runat="server" OnClick="Save_Click" Text="Save"></asp:Button>
                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CssClass="button" OnClick="Delete_Click"
                    OnClientClick="javascript:return confirm('Are you sure to Delete?');" Visible="false" />
                <input type="button" value="Close" onclick="window.location.href='HomePage.aspx';" />
            </td>
        </tr>
    </table>
    <input type="hidden" runat="server" id="VoucherID" />
    <input type="hidden" id="mode" runat="server" />
</asp:Content>
