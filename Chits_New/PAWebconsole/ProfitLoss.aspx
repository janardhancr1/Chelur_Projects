<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ProfitLoss.aspx.cs" Inherits="ProfitLoss" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Profit & Loss</div>
    <table width="100%">
        <tr>
            <td width="5%">
                View</td>
            <td width="10%">
                <asp:DropDownList ID="ViewType" runat="server" OnSelectedIndexChanged="ViewType_Selected"
                    AutoPostBack="true">
                </asp:DropDownList></td>
            <td id="FinancialCell" runat="server" width="50%" align="center">
                Financial Year : <b>
                    <asp:Label ID="FinYearFrom" runat="server"></asp:Label></b> To <b>
                        <asp:Label ID="FinYearTo" runat="server"></asp:Label></b></td>
            <td id="UptoDateCell" runat="server" visible="false" width="50%" align="center">
                From : <b>
                    <asp:Label ID="UptoYearFrom" runat="server"></asp:Label></b> To <b>
                        <input type="text" id="UptoYearTo" runat="server" onfocus="showCalendarControl(this);"
                            readonly /></b>
            </td>
            <td width="35%">
                <asp:Button ID="ViewButton" Text="View" runat="server" OnClick="View_Click" />
                <asp:Button ID="DetailsButton" Text="Details" runat="server" OnClick="Details_Click" />
                <input type="button" value="Print" onclick="javascript:openReport();"/>
                <input type="hidden" id="SelectedType" runat="server" />
                <input type="hidden" id="ToDate" runat="server" />
            </td>
        </tr>
    </table>
    <hr />
    <br />
    <div style="height: 350px; overflow-y: auto; overflow-x:hidden">
        <table width="98%">
            <tr class="nav_header" align="center">
                <td width="50%">
                    Expenses</td>
                <td width="50%">
                    Incomes</td>
            </tr>
        </table>
        <table width="98%">
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
        <table width="98%">
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td width="50%" valign="top">
                    <table width="100%" id="TotalExpense" runat="server">
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%" id="TotalIncome" runat="server">
                    </table>
                </td>
            </tr>
        </table>
        <table width="98%">
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
    </div>
</asp:Content>
