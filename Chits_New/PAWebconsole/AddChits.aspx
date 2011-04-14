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
                <asp:TextBox ID="ChitName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ChitName"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Chit Amount</td>
            <td>
                <asp:TextBox ID="ChitAmount" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ChitAmount"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Installment Amount</td>
            <td>
                <asp:TextBox ID="InstallmentAmount" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="InstallmentAmount"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                No Installments</td>
            <td>
                <asp:TextBox ID="NoInstallments" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="NoInstallments"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph"
                    HeaderText="Please fill all required values." />
                <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="SaveButton" runat="server" OnClick="Save_Click" Text="Save"></asp:Button>
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
