<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="HomePage.aspx.cs" Inherits="HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header">
        Home</div>
    <table width="100%" height="400">
        <tr>
            <td align="center" valign="middle" width="100%" colspan="2">
                <table>
                    <tr>
                        <td>
                            <b>
                                <asp:Label ID="CompanyName" runat="server" Font-Size="Medium" Text="Globe"></asp:Label></b></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="50%">
                <table width="100%" visible="false" id="DTable" runat="server">
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%" visible="false" id="PTable" runat="server">
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
