<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="AddLedgers.aspx.cs" Inherits="AddLedgers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        <asp:Label ID="ActionTitle" runat="server">Add Ledgers</asp:Label></div>
    <table width="100%" class="data_table">
        <tr>
            <td>
                Ledger Name</td>
            <td>
                <asp:TextBox ID="LedgerName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LedgerName"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Opening Balance</td>
            <td>
                <asp:TextBox ID="OpeningBalance" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="OpeningBalance"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Balance Type</td>
            <td>
                <asp:DropDownList ID="BalanceType" runat="server" Width="153px">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                    <asp:ListItem Value="Cr">Cr</asp:ListItem>
                    <asp:ListItem Value="Dr">Dr</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="BalanceType"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Group</td>
            <td>
                <asp:DropDownList ID="GroupID" runat="server" Width="153px">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="GroupID"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
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
                    onclick="window.location.href='AddLedgers.aspx';" />
                <asp:Button ID="SaveButton" runat="server" OnClick="Save_Click" Text="Save"></asp:Button>
                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CssClass="button" OnClick="Delete_Click"
                    OnClientClick="javascript:return confirm('Are you sure to Delete?');" Visible="false" />
                <input type="button" value="Close" onclick="window.location.href='HomePage.aspx';" />
            </td>
        </tr>
    </table>
    <input type="hidden" runat="server" id="LedgerID" />
    <input type="hidden" id="mode" runat="server" />
</asp:Content>
