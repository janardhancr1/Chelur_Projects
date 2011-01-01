<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="AddCity.aspx.cs" Inherits="AddCity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Add New Village</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td width="25%">
                Village Name</td>
            <td width="25%">
                <asp:TextBox ID="VillageName" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="VillageName">*</asp:RequiredFieldValidator></td>
            <td width="50%">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                City Name</td>
            <td>
                <asp:TextBox ID="CityName" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CityName">*</asp:RequiredFieldValidator></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                State Name</td>
            <td>
                <asp:DropDownList ID="StateName" runat="Server" Width="153px">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                    <asp:ListItem Value="Karnataka">Karnataka</asp:ListItem>
                    <asp:ListItem Value="Andra Pradesh">Andra Pradesh</asp:ListItem>
                    <asp:ListItem Value="TamilName">Tamil Nadu</asp:ListItem>
                    <asp:ListItem Value="Kerala">Kerala</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="StateName">*</asp:RequiredFieldValidator></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Pincode</td>
            <td>
                <asp:TextBox ID="Pincode" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Pincode">*</asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <asp:Label ID="message" runat="server" ForeColor="red"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph"
                    HeaderText="Please fill all the required fields" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <asp:Button ID="SaveButton" Text="Save" runat="server" OnClick="Save_Click" />
                <input type="button" value="Close" onclick="window.location.href='Cities.aspx';" />
                <input type="hidden" id="mode" runat="server" />
                <input type="hidden" id="CityID" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="nav_header" colspan="3">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
