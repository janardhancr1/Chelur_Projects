<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="BalanceSheet.aspx.cs" Inherits="BalanceSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Balance Sheet</div>
    <table width="100%">
        <tr>
            <td align="center">
                Financial Year : <b>
                    <asp:Label ID="FromYear" runat="server"></asp:Label></b> To <b>
                        <asp:Label ID="ToYear" runat="server"></asp:Label></b>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr class="nav_header" align="center">
            <td width="50%">
                ASSETS</td>
            <td width="50%">
                LIABILITIES</td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td width="50%" valign="top">
                <table width="100%" id="ExpenseTable" runat="server">
                    <tr class="nav_header">
                        <td width="50%">
                            Particulars</td>
                        <td width="50%" align="right">
                            Amount</td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%" id="IncomeTable" runat="server">
                    <tr class="nav_header">
                        <td width="50%">
                            Particulars</td>
                        <td width="50%" align="right">
                            Amount</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td width="50%" valign="top">
                <table width="100%" id="Loss" runat="server">
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%" id="Profit" runat="server">
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
    </table>
</asp:Content>
