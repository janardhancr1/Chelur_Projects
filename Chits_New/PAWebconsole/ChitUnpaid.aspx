<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ChitUnpaid.aspx.cs" Inherits="ChitUnpaid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Unpaid Members</div>
    <table width="100%">
        <tr>
            <td>
                Chit No</td>
            <td>
                <input type="text" id="ChitNO" runat="server" readonly /></td>
        </tr>
        <tr>
            <td>
                Chit Name</td>
            <td>
                <input type="text" id="ChitName" runat="server" readonly />
            </td>
        </tr>
        <tr>
            <td>
                Chit Amount</td>
            <td>
                <input type="text" id="ChitAmount" runat="server" readonly /></td>
        </tr>
        <tr>
            <td>
                Installment No</td>
            <td>
                <input type="text" id="InstallmentNo" runat="server" readonly /></td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%" id="MembersTable" runat="server">
                    <tr class="nav_header">
                        <th align="left">CustomerName</th>
                        <th align="left">CustomerAddress</th>
                        <th align="left">InstallMent No</th>
                        <th align="left">Amount</th>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <input type="button" value="Close" id="CloseButton" runat="Server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="nav_header">
                &nbsp;
            </td>
        </tr>
    </table>
    
</asp:Content>
