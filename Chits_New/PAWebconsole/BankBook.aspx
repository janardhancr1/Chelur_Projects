<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="BankBook.aspx.cs" Inherits="BankBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div id="content" class="nav_header" runat="server">
        Bank Book</div>
    <table width="100%">
        <tr>
            <td width="15%">
                Select Bank:</td>
            <td width="15%">
                <asp:DropDownList ID="BankName" runat="server" Width="153px" AutoPostBack="true" OnSelectedIndexChanged="BankName_Selected">
                </asp:DropDownList></td>
            <td width="70%" align="center">
                Financial Year : <b>
                    <asp:Label ID="FromYear" runat="server"></asp:Label></b> To <b>
                        <asp:Label ID="ToYear" runat="server"></asp:Label></b>
            </td>
        </tr>
    </table>
    <table width="100%" id="DetailsTable" runat="server">
        <tr class="nav_header">
            <td>
                Particulars</td>
            <td align="right">
                Debit</td>
            <td align="right">
                Credit</td>
            <td align="right">
                Balance</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                Opening Balance</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td colspan="4">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                April</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                May</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                June</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                July</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                August</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                September</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                October</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                November</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                December</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                January</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                February</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                March</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td colspan="4">
                &nbsp;</td>
        </tr>
        <tr onmouseover="this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';"
            onmouseout="this.style.backgroundColor=this.oldColor;" style="cursor: hand">
            <td>
                Closing Balance</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr class="nav_header">
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="DetailsButton" Text="Details" runat="server" OnClick="Details_Click" />
                <input id="Button1" type="button" value="Close" runat="server" onclick="window.location.href='HomePage.aspx';" />
            </td>
        </tr>
    </table>
</asp:Content>
