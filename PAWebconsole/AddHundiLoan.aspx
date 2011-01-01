<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="AddHundiLoan.aspx.cs" Inherits="AddHundiLoan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Add Hundi Loan</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td width="25%">
                Hundi Loan No</td>
            <td width="25%">
                <asp:TextBox ID="HLLoanNo" runat="server" MaxLength="10" ReadOnly="true"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="HLLoanNo">*</asp:RequiredFieldValidator>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Date
            </td>
            <td>
                <input type="text" id="LoanDate" runat="server" onfocus="showCalendarControl(this);"
                    readonly />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LoanDate">*</asp:RequiredFieldValidator>
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
                Pay Mode
            </td>
            <td>
                <asp:RadioButtonList ID="PayMode" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                    OnSelectedIndexChanged="PayMode_Selected">
                    <asp:ListItem Value="1" Selected="True">Cash</asp:ListItem>
                    <asp:ListItem Value="2">Cheque</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr id="ChequeRow" runat="server" visible="false">
            <td>
                Cheque No</td>
            <td>
                <asp:TextBox ID="ChequeNo" runat="server" MaxLength="10"></asp:TextBox></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr id="BankRow" runat="server" visible="false">
            <td>
                Bank Name</td>
            <td>
                <asp:DropDownList ID="BankName" runat="server" Width="153px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                CoObligent</td>
            <td>
                <asp:TextBox ID="CoObligent" runat="server" MaxLength="50"></asp:TextBox>
            </td>
            <td colspan="2">
                Address: <b>
                    <asp:Label ID="Address" runat="Server"></asp:Label></b></td>
        </tr>
        <tr>
            <td>
                CoObligent Address</td>
            <td>
                <asp:TextBox ID="CoObligentAddress" runat="server" MaxLength="100"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Loan Amount
            </td>
            <td>
                <asp:TextBox ID="LoanAmount" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="LoanAmount">*</asp:RequiredFieldValidator></td>
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
                <input type="button" value="Close" onclick="window.location.href='HundiLoans.aspx';" /></td>
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
