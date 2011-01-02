<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="AddFixedDesposit.aspx.cs" Inherits="AddFixedDesposit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Add Fixed Deposit</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td width="25%">
                FD No</td>
            <td width="25%">
                <asp:TextBox ID="FDNo" runat="server" MaxLength="10" ReadOnly="true"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FDNo">*</asp:RequiredFieldValidator>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Date
            </td>
            <td>
                <input type="text" id="FDDate" runat="server" onfocus="showCalendarControl(this);"
                    readonly />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FDDate">*</asp:RequiredFieldValidator>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Customer Name</td>
            <td>
                <asp:DropDownList ID="CustomerName" runat="server" Width="153px" AutoPostBack="true"
                    OnSelectedIndexChanged="CustomerName_Selected">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="CustomerName">*</asp:RequiredFieldValidator>
            </td>
            <td colspan="2">
                Account No: <b>
                    <asp:Label ID="AccountNo" runat="Server"></asp:Label></b></td>
        </tr>
        <tr>
            <td>
                Nominee Name</td>
            <td>
                <asp:TextBox ID="NomineeName" runat="server" MaxLength="100"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Nominee Relationship:
            </td>
            <td>
                <asp:TextBox ID="NomineeRelation" runat="server" MaxLength="100"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                FD Amount
            </td>
            <td>
                <asp:TextBox ID="FDAmount" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="FDAmount">*</asp:RequiredFieldValidator></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Rate
            </td>
            <td>
                <asp:TextBox ID="Rate" runat="server" MaxLength="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Rate">*</asp:RequiredFieldValidator></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <asp:Label ID="ErrorMessage" runat="server" ForeColor="red"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph"
                    HeaderText="Please fill all the required fields" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="SaveButton" Text="Save" runat="server" OnClick="Save_Click" />
                <input type="button" value="Close" onclick="window.location.href='FixedDesposits.aspx';" /></td>
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
