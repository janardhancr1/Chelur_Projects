<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="Groups.aspx.cs" Inherits="Groups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Groups</div>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:ListBox ID="GroupNames" runat="server" Height="350px" Width="278px"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                <input type="button" value="Close" runat="server" onclick="window.location.href='HomePage.aspx';" />
            </td>
        </tr>
    </table>
</asp:Content>
