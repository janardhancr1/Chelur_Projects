<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="AddCustomer.aspx.cs" Inherits="AddCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Add New Customer</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td>
                Customer Name</td>
            <td>
                <asp:TextBox ID="CustomerName" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CustomerName">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Father/Husband Name</td>
            <td>
                <asp:TextBox ID="FatherHusband" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FatherHusband">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Account No</td>
            <td>
                <asp:TextBox ID="AccountNo" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic"
                    ControlToValidate="AccountNo">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Res Address</td>
            <td>
                <asp:TextBox ID="ResAddress" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ResAddress">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Res City/Pin</td>
            <td>
                <asp:DropDownList ID="ResVillage" runat="server" Width="153px" AutoPostBack="true"
                    OnSelectedIndexChanged="Residence_Selected">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ResVillage">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="FullAddress" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>
                Res Phone</td>
            <td>
                <asp:TextBox ID="ResPhone" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="message" runat="server"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph"
                    HeaderText="Please fill all the required fields" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="SaveButton" Text="Save" runat="server" OnClick="Save_Click" />
                <input type="button" value="Close" onclick="window.location.href='Customer.aspx';" />
                <input type="hidden" id="mode" runat="server" />
                <input type="hidden" id="CustomerID" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="nav_header" colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
