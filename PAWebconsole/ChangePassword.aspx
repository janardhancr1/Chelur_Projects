<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Change Password</div>
    <table width="100%" class="data_table">
        <tr>
            <td width="20%">
                Old Password</td>
            <td width="80%">
                <asp:TextBox ID="OldPassword" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="OldPassword"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                New Password</td>
            <td>
                <asp:TextBox ID="NewPassword" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NewPassword"
                    Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Confirm Password</td>
            <td>
                <asp:TextBox ID="ConfirmPassword" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ConfirmPassword"
                    Display="Dynamic">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="NewPassword"
                    ControlToValidate="ConfirmPassword" Display="Dynamic">New Password & Confirm Password do not match</asp:CompareValidator></td>
        </tr>
    </table>
    <hr />
    <table width="100%">
        <tr>
            <td width="20%">
                &nbsp;
            </td>
            <td width="80%">
                <asp:Label ID="message" runat="server" ForeColor="Red"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph"
                    HeaderText="Please fill all requires fields" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="SaveButton" runat="server" Text="Change" CssClass="button" OnClick="Save_Click" />
                <input type="button" value="Close" onclick="window.location.href='HomePage.aspx';"
                    class="button" />
            </td>
        </tr>
    </table>
</asp:Content>
